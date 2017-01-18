using POP_SF7.Windows;
using System.Linq;
using System.Windows;
using POP_SF7.Helpers;
using System.ComponentModel;
using System.Windows.Data;
using POP_SF7.DB;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace POP_SF7
{
    /// <summary>
    /// Interaction logic for PersonAddEdit.xaml
    /// </summary>
    public partial class StudentAddEdit : Window
    {
        public ICollectionView CoursesView { get; set; }
        public ICollectionView DeletedCoursesView { get; set; }

        public List<Course> AddedCourses { get; set; }
        public List<Course> EditedCourses { get; set; }
        public List<Course> DeletedCourses { get; set; }

        public Student StudentS { get; set; }
        public Decider Decider { get; set; }

        public string labelAddStudent = "Dodavanje novog ucenika";
        public string labelEditStudent = "Izmena postojeceg ucenika";
        
        public StudentAddEdit(Student student, Decider decider)
        {
            InitializeComponent();

            StudentS = student;
            Decider = decider;

            AddedCourses = new List<Course>();
            EditedCourses = new List<Course>();
            DeletedCourses = new List<Course>();

            setupWindow();
        }

        private void setupWindow()
        {
            DataContext = StudentS;
            personInfo.descriptionlbl.Text = (Decider == Decider.ADD) ? labelAddStudent : labelEditStudent;

            setupGrid(CoursesView, StudentS.ListOfCourses, coursesdg);

            setupGrid(DeletedCoursesView, StudentS.ListOfDeletedCourses, deletedCoursesdg);
        }

        private void setupGrid(ICollectionView view, ObservableCollection<Course> collection, DataGrid dataGrid)
        {
            view = CollectionViewSource.GetDefaultView(collection);
            dataGrid.ItemsSource = view;
            dataGrid.IsSynchronizedWithCurrentItem = true;
        }

        private void okbtn_Click(object sender, RoutedEventArgs e)
        {
            if (personInfo.nametb.Text.Equals("") || personInfo.lastnametb.Text.Equals("") || personInfo.addresstb.Text.Equals("") || personInfo.jmbgtb.Text.Equals(""))
            {
                MessageBox.Show(ApplicationA.FILL_ALL_FIELDS_WARNING);
            }
            else
            {
                if (Decider == Decider.ADD)
                {
                    StudentS.Id = ApplicationA.Instance.Students.Count() + 1;
                    if (StudentDAO.Add(StudentS) && saveCourses())
                    {
                        ApplicationA.Instance.Students.Add(StudentS);
                    }
                }
                else
                {
                    if (StudentDAO.Edit(StudentS) && saveCourses())
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

        private bool saveCourses()
        {
            bool valid = true;

            foreach (Course c in AddedCourses)
            {
                valid = StudentAttendsCourseDAO.Add(StudentS.Id, c.Id);
            }

            foreach (Course c in EditedCourses)
            {
                valid = StudentAttendsCourseDAO.UnDelete(StudentS.Id, c.Id);
            }

            foreach (Course c in DeletedCourses)
            {
                valid = StudentAttendsCourseDAO.Delete(StudentS.Id, c.Id);
            }

            return valid;
        }

        private void coursesdg_AutoGeneratingColumn(object sender, System.Windows.Controls.DataGridAutoGeneratingColumnEventArgs e)
        {
            LoadColumnsHelper.LoadCourse(e);
        }

        private void cancelbtn_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void addCoursebtn_Click(object sender, RoutedEventArgs e)
        {
            SelectCourseLangStud scl = new SelectCourseLangStud(this);
            scl.Show();
        }

        private void undeleteCoursebtn_Click(object sender, RoutedEventArgs e)
        {
            Course selectedCourse = DeletedCoursesView.CurrentItem as Course;
            if (selectedCourse == null)
            {
                MessageBox.Show("Morate da selektujete kurs koji zelite da povratite!");
            }
            else
            {
                var result = MessageBox.Show("Da li ste sigurni da hocete da povratite dati kurs za ovog ucenika?", "Upozorenje", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    if (DeletedCourses.Contains(selectedCourse))
                    {
                        DeletedCourses.Remove(selectedCourse);
                    }

                    EditedCourses.Add(selectedCourse);

                    StudentS.ListOfCourses.Add(selectedCourse);
                    StudentS.ListOfDeletedCourses.Remove(selectedCourse);
                }
            }
        }

        private void deleteCoursebtn_Click(object sender, RoutedEventArgs e)
        {
            Course selectedCourse = CoursesView.CurrentItem as Course;
            if (selectedCourse == null)
            {
                MessageBox.Show("Morate da selektujete kurs koji zelite da obrisete!");
            }
            else
            {
                var result = MessageBox.Show("Da li ste sigurni da hocete da obrisete dati kurs za ovog ucenika?", "Upozorenje", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    checkIfCourseAddedOrDeleted(selectedCourse);
                }
            }
        }

        private void checkIfCourseAddedOrDeleted(Course course)
        {
            if (AddedCourses.Contains(course))
            {
                AddedCourses.Remove(course);
            }
            else if (EditedCourses.Contains(course))
            {
                EditedCourses.Remove(course);
            }

            DeletedCourses.Add(course);

            StudentS.ListOfDeletedCourses.Add(course);
            StudentS.ListOfCourses.Remove(course);
        }
    }
}
