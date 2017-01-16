using POP_SF7.DB;
using POP_SF7.Helpers;
using POP_SF7.School;
using POP_SF7.Windows;
using System;
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

        public ObservableCollection<Student> ListOfStudents { get; set; }
        public ICollectionView StudentsView { get; set; }

        public ObservableCollection<Teacher> FilteredTeachers { get; set; }

        public string labelCourseAdd = "Dodavanje novog kursa";
        public string labelCourseEdit = "Izmena postojeceg kursa";

        public CourseAddEdit(Course course, Decider decider, ObservableCollection<Student> listOfStudents)
        {
            InitializeComponent();
            Course = course;
            Decider = decider;
            ListOfStudents = listOfStudents;
            FilteredTeachers = new ObservableCollection<Teacher>();

            setupWindow();
        }

        private void setupWindow()
        {
            DataContext = Course;

            descriptionlbl.Text = (Decider == Decider.ADD) ? labelCourseAdd : labelCourseEdit;

            languagecb.ItemsSource = ApplicationA.Instance.Languages;
            languagecb.DisplayMemberPath = "Name";
            languagecb.SelectedValuePath = "Id";
            languagecb.SelectedIndex = 0;

            courseTypecb.ItemsSource = ApplicationA.Instance.CourseTypes;
            courseTypecb.DisplayMemberPath = "Name";
            courseTypecb.SelectedValuePath = "Id";
            courseTypecb.SelectedIndex = 0;

            teachercb.ItemsSource = FilteredTeachers;
            teachercb.DisplayMemberPath = "LastName";
            teachercb.SelectedValuePath = "Id";
            teachercb.SelectedIndex = 0;

            if(Decider == Decider.ADD && ListOfStudents.Count != 0)
            {
                ListOfStudents.Clear();
            }
            StudentsView = CollectionViewSource.GetDefaultView(ListOfStudents);
            studentsdg.ItemsSource = StudentsView;
            studentsdg.IsSynchronizedWithCurrentItem = true;

            if(Decider == Decider.ADD)
            {
                teachercb.IsEnabled = false;
            }
        }

        private int setTeacherComboBoxEDIT()
        {
            int index = FilteredTeachers.IndexOf(Course.Teacher);

            return index;
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
                    if (CourseDAO.Add(Course))
                    {
                        Course.Id = ApplicationA.Instance.Courses.Count() + 1;
                        ApplicationA.Instance.Courses.Add(Course);
                    }
                }
                else
                {
                    if (!CourseDAO.Edit(Course))
                    {
                        cancelbtn_Click(null, null);
                    }
                }
                Close();
            }
        }

        private void studentsdg_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            LoadColumnsHelper.LoadStudent(e);
        }

        private void languagecb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int languageId = 0;

            if (FilteredTeachers.Count != 0) FilteredTeachers.Clear();

            if(languagecb.SelectedItem != null)
            {
                languageId = (int)languagecb.SelectedValue;

                foreach (TeacherTeachesLanguage ttl in ApplicationA.Instance.TeacherTeachesLanguageCollection)
                {
                    if (ttl.LanguageId == languageId && ttl.Deleted == false)
                    {
                        Teacher teach = ApplicationA.Instance.Teachers[ttl.TeacherId - 1];
                        FilteredTeachers.Add(teach);
                    }
                }

                teachercb.IsEnabled = (FilteredTeachers.Count == 0) ? false : true;
            }
        }

        private void cancelbtn_Click(object sender, RoutedEventArgs e)
        {
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
                if (selectedStudent.Deleted == true) // ovde ide provera na osnovu boje
                {
                    MessageBox.Show("Selektovani ucenik je obrisan!");
                }
                else
                {
                    var result = MessageBox.Show("Da li ste sigurni da hocete da obrisete datog ucenika za ovaj kurs?", "Upozorenje", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    if (result == MessageBoxResult.Yes)
                    {
                        foreach (StudentAttendsCourse sac in ApplicationA.Instance.StudentAttendsCourseCollection)
                        {
                            if (sac.CourseId == Course.Id && sac.StudentId == selectedStudent.Id)
                            {
                                if (StudentAttendsCourseDAO.UnDelete(sac))
                                {
                                    sac.Deleted = true;
                                    // boja - crvena
                                }
                            }
                        }
                    }
                }
            }
        }

        private void undeleteStudentbtn_Click(object sender, RoutedEventArgs e)
        {
            Student selectedStudent = StudentsView.CurrentItem as Student;
            if (selectedStudent == null)
            {
                MessageBox.Show("Morate da selektujete ucenika da biste ga povratili!");
            }
            else
            {
                if (selectedStudent.Deleted == false) // ovde ide provera na osnovu boje
                {
                    MessageBox.Show("Selektovani ucenik nije obrisan!");
                }
                else
                {
                    var result = MessageBox.Show("Da li ste sigurni da hocete da povratite datog ucenika za ovog kurs?", "Upozorenje", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    if (result == MessageBoxResult.Yes)
                    {
                        foreach (StudentAttendsCourse sac in ApplicationA.Instance.StudentAttendsCourseCollection)
                        {
                            if (sac.CourseId == Course.Id && sac.StudentId == selectedStudent.Id)
                            {
                                if (StudentAttendsCourseDAO.UnDelete(sac))
                                {
                                    sac.Deleted = false;
                                    // boja - default
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
