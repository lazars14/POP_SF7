using POP_SF7.DB;
using POP_SF7.Helpers;
using POP_SF7.Windows;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace POP_SF7
{
    /// <summary>
    /// Interaction logic for CourseAddEdit.xaml
    /// </summary>
    public partial class CourseAddEdit : Window
    {
        public Course Course { get; set; }
        public Decider Decider { get; set; }

        public ICollectionView StudentsView { get; set; }
        public ICollectionView DeletedStudentsView { get; set; }

        public List<Student> AddedStudents { get; set; }
        public List<Student> EditedStudents { get; set; }
        public List<Student> DeletedStudents { get; set; }

        public ObservableCollection<Teacher> FilteredTeachers { get; set; }

        public string labelCourseAdd = "Dodavanje novog kursa";
        public string labelCourseEdit = "Izmena postojeceg kursa";

        public CourseAddEdit(Course course, Decider decider)
        {
            InitializeComponent();
            Course = course;
            Decider = decider;
            FilteredTeachers = new ObservableCollection<Teacher>();

            AddedStudents = new List<Student>();
            EditedStudents = new List<Student>();
            DeletedStudents = new List<Student>();

            setupWindow();
        }

        private void setupWindow()
        {
            DataContext = Course;

            descriptionlbl.Text = (Decider == Decider.ADD) ? labelCourseAdd : labelCourseEdit;

            languagecb.ItemsSource = ApplicationA.Instance.Languages;
            setupComboBox(languagecb, "Name");

            courseTypecb.ItemsSource = ApplicationA.Instance.CourseTypes;
            setupComboBox(courseTypecb, "Name");

            teachercb.ItemsSource = FilteredTeachers;
            setupComboBox(teachercb, "FullName");

            setupGrid(StudentsView, Course.ListOfStudents, studentsdg);
            
            setupGrid(DeletedStudentsView, Course.ListOfDeletedStudents, deletedStudentsdg);

            if (Decider == Decider.ADD)
            {
                teachercb.IsEnabled = false;
            }
        }

        private void setupGrid(ICollectionView view, ObservableCollection<Student> collection, DataGrid dg)
        {
            view = CollectionViewSource.GetDefaultView(collection);
            dg.ItemsSource = view;
            dg.IsSynchronizedWithCurrentItem = true;
        }

        private void setupComboBox(ComboBox cb, string memberPath)
        {
            cb.DisplayMemberPath = memberPath;
            cb.SelectedValuePath = "Id";
        }

        private void okbtn_Click(object sender, RoutedEventArgs e)
        {
            if(pricetb.Text.Equals("") || languagecb.SelectedItem == null || courseTypecb.SelectedItem == null || teachercb.SelectedItem == null)
            {
                MessageBox.Show(ApplicationA.FILL_ALL_FIELDS_WARNING);
            }
            else
            {
                if (Decider == Decider.ADD)
                {
                    Course.Id = ApplicationA.Instance.Courses.Count() + 1;

                    if (CourseDAO.Add(Course) && saveStudents())
                    {
                        ApplicationA.Instance.Courses.Add(Course);
                    }
                }
                else
                {
                    if (CourseDAO.Edit(Course) && saveStudents())
                    {
                        DialogResult = true;
                    }
                    else
                    {
                        cancelbtn_Click(null, null);
                    }
                }
                Close();
            }
        }

        private bool saveStudents()
        {
            bool valid = true;

            foreach(Student s in AddedStudents)
            {
                valid = StudentAttendsCourseDAO.Add(s.Id, Course.Id);
            }

            foreach (Student s in EditedStudents)
            {
                valid = StudentAttendsCourseDAO.UnDelete(s.Id, Course.Id);
            }

            foreach (Student s in DeletedStudents)
            {
                valid = StudentAttendsCourseDAO.Delete(s.Id, Course.Id);
            }

            return valid;
        }

        private void studentsdg_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            LoadColumnsHelper.LoadStudent(e);
        }

        private void languagecb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FilteredTeachers.Count != 0) FilteredTeachers.Clear();

            if(languagecb.SelectedItem != null)
            {
                foreach (Teacher t in ApplicationA.Instance.Teachers)
                {
                    if (t.ListOfLanguages.Contains(languagecb.SelectedItem))
                    {
                        FilteredTeachers.Add(t);
                    }
                }

                teachercb.IsEnabled = (FilteredTeachers.Count == 0) ? false : true;
            }
        }

        private void cancelbtn_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            Close();
        }

        private void addStudentbtn_Click(object sender, RoutedEventArgs e)
        {
            SelectCourseLangStud add = new SelectCourseLangStud(this);
            add.Show(); 
        }

        private void deleteStudentbtn_Click(object sender, RoutedEventArgs e)
        {
            Student selectedStudent = StudentsView.CurrentItem as Student;
            if (selectedStudent == null)
            {
                MessageBox.Show("Morate da selektujete ucenika da biste ga obrisali!");
            }
            else
            {
                var result = MessageBox.Show("Da li ste sigurni da hocete da obrisete datog ucenika za ovaj kurs?", "Upozorenje", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    checkIfStudentAddedOrDeleted(selectedStudent);
                }
            }
        }

        private void undeleteStudentbtn_Click(object sender, RoutedEventArgs e)
        {
            Student selectedStudent = DeletedStudentsView.CurrentItem as Student;
            if (selectedStudent == null)
            {
                MessageBox.Show("Morate da selektujete ucenika da biste ga povratili!");
            }
            else
            {
                var result = MessageBox.Show("Da li ste sigurni da hocete da povratite datog ucenika za ovog kurs?", "Upozorenje", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    if(DeletedStudents.Contains(selectedStudent))
                    {
                        DeletedStudents.Remove(selectedStudent);
                    }
                    EditedStudents.Add(selectedStudent);

                    Course.ListOfStudents.Add(selectedStudent);
                    Course.ListOfDeletedStudents.Remove(selectedStudent);
                }
            }
        }

        private void checkIfStudentAddedOrDeleted(Student student)
        {
            if(AddedStudents.Contains(student))
            {
                AddedStudents.Remove(student);
            }
            else if(EditedStudents.Contains(student))
            {
                EditedStudents.Remove(student);
            }

            DeletedStudents.Add(student);

            Course.ListOfDeletedStudents.Add(student);
            Course.ListOfStudents.Remove(student);
        }
    }
}
