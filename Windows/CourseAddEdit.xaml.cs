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

        public string labelCourseAdd = "Dodavanje novog kursa";
        public string labelCourseEdit = "Izmena postojeceg kursa";

        public CourseAddEdit(Course course, Decider decider, ObservableCollection<Student> listOfStudents)
        {
            InitializeComponent();
            Course = course;
            Decider = decider;
            ListOfStudents = listOfStudents;

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

            teachercb.ItemsSource = ApplicationA.Instance.Teachers;
            teachercb.DisplayMemberPath = "FullName";
            teachercb.SelectedValuePath = "Id";
            teachercb.SelectedIndex = 0;

            StudentsView = CollectionViewSource.GetDefaultView(ListOfStudents);
            studentsdg.ItemsSource = StudentsView;
            studentsdg.IsSynchronizedWithCurrentItem = true;

            if(Decider == Decider.ADD)
            {
                teachercb.IsEnabled = false;
            }
            else
            {
                languagecb.SelectedIndex = Course.Language.Id - 1;
                courseTypecb.SelectedIndex = Course.CourseType.Id - 1;
                teachercb.SelectedIndex = Course.Teacher.Id - 1;
            }
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
                    if (Course.Add(Course))
                    {
                        Course.Id = ApplicationA.Instance.Courses.Count() + 1;
                        ApplicationA.Instance.Courses.Add(Course);
                    }
                }
                else
                {
                    if (!Course.Edit(Course))
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
            List<Teacher> filteredTeachers = new List<Teacher>();
            int languageId = 0;
            try
            {
                languageId = (int)languagecb.SelectedValue;
            }
            catch(NullReferenceException a) { Console.WriteLine(a.StackTrace); }

            foreach(TeacherTeachesLanguage ttl in ApplicationA.Instance.TeacherTeachesLanguageCollection)
            {
                if(ttl.LanguageId == languageId)
                {
                    Teacher teach = ApplicationA.Instance.Teachers[ttl.TeacherId - 1];
                    filteredTeachers.Add(teach);
                }
            }
            if(filteredTeachers.Count != 0)
            {
                teachercb.ItemsSource = filteredTeachers;
                teachercb.IsEnabled = true;
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
                if (selectedStudent.Deleted == true)
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
                                if (StudentAttendsCourse.UnDelete(sac))
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
                if (selectedStudent.Deleted == false)
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
                                if (StudentAttendsCourse.UnDelete(sac))
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
