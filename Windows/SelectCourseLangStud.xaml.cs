using POP_SF7.DB;
using POP_SF7.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace POP_SF7.Windows
{
    /// <summary>
    /// Interaction logic for SelectCourseLanguage.xaml
    /// </summary>

    public partial class SelectCourseLangStud : Window
    {
        public StudentAddEdit StudentWindow { get; set; }
        public TeacherAddEdit TeacherWindow { get; set; }
        public CourseAddEdit CourseWindow { get; set; }

        public string addLanguage = "Dodavanje jezika";
        public string addCourse = "Dodavanje kursa";
        public string addStudent = "Dodavanje ucenika";

        public SelectCourseLangStud(TeacherAddEdit teacherAddEdit)
        {
            InitializeComponent();

            TeacherWindow = teacherAddEdit;

            setupWindow();
        }

        public SelectCourseLangStud(StudentAddEdit studentAddEdit)
        {
            InitializeComponent();

            StudentWindow = studentAddEdit;

            setupWindow();
        }

        public SelectCourseLangStud(CourseAddEdit courseAddEdit)
        {
            InitializeComponent();

            CourseWindow = courseAddEdit;

            setupWindow();
        }

        private void setupWindow()
        {
            if(TeacherWindow == null && CourseWindow == null)
            {
                description.Text = addCourse;
                setupCourseGrid();
            }
            else if(CourseWindow == null && StudentWindow == null)
            {
                description.Text = addLanguage;
                setupLanguageGrid();
            }
            else
            {
                description.Text = addStudent;
                setupStudentsGrid();
            }
        }

        private void setupStudentsGrid()
        {
            List<Student> FilteredStudents = new List<Student>();
            foreach(Student student in ApplicationA.Instance.Students)
            {
                if(!CourseWindow.Course.ListOfStudents.Contains(student) && !CourseWindow.Course.ListOfDeletedStudents.Contains(student))
                {
                    FilteredStudents.Add(student);
                }
            }

            if (FilteredStudents.Count == 0)
            {
                MessageBox.Show("Ne postoje ucenici koji mogu da se dodaju na dati kurs!");
                Close();
            }
            else
            {
                dataGrid.ItemsSource = FilteredStudents;
            }
        }

        private void setupLanguageGrid()
        {
            List<Language> FilteredLanguages = new List<Language>();
            foreach(Language language in ApplicationA.Instance.Languages)
            {
                if(!TeacherWindow.TeacherT.ListOfLanguages.Contains(language) && !TeacherWindow.TeacherT.ListOfDeletedLanguages.Contains(language))
                {
                    FilteredLanguages.Add(language);
                }
            }

            if(FilteredLanguages.Count == 0)
            {
                MessageBox.Show("Ne postoje jezici koji mogu da se dodaju za datog nastavnika!");
                Close();
            }
            else
            {
                dataGrid.ItemsSource = FilteredLanguages;
            }
        }

        private void setupCourseGrid()
        {
            List<Course> FilteredCourses = new List<Course>();
            foreach(Course course in ApplicationA.Instance.Courses)
            {
                if(!StudentWindow.StudentS.ListOfCourses.Contains(course) && !StudentWindow.StudentS.ListOfDeletedCourses.Contains(course))
                {
                    FilteredCourses.Add(course);
                }
            }

            if(FilteredCourses.Count == 0)
            {
                MessageBox.Show("Ne postoje kursevi koji mogu da se dodaju za datog ucenika!");
                Close();
            }
            else
            {
                dataGrid.ItemsSource = FilteredCourses;
            }
        }

        private void closebtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void okbtn_Click(object sender, RoutedEventArgs e)
        {
            if(TeacherWindow == null && CourseWindow == null)
            {
                int a = dataGrid.SelectedItems.Count;
                if (a > 0)
                {
                    foreach (Course c in dataGrid.SelectedItems)
                    {
                        StudentWindow.StudentS.ListOfCourses.Add(c);
                        StudentWindow.AddedCourses.Add(c);
                    }
                    Close();
                }
                else
                {
                    MessageBox.Show("Morate da selektujete kurs kako biste ga dodali na ucenika!");
                }
            }
            else if(CourseWindow == null && StudentWindow == null)
            {
                int a = dataGrid.SelectedItems.Count;
                if (a > 0)
                {
                    foreach (Language lang in dataGrid.SelectedItems)
                    {
                        TeacherWindow.TeacherT.ListOfLanguages.Add(lang);
                        TeacherWindow.AddedLanguages.Add(lang);
                    }
                    Close();
                }
                else
                {
                    MessageBox.Show("Morate da selektujete jezik kako biste ga dodali na nastavnika!");
                }
            }
            else
            {
                int a = dataGrid.SelectedItems.Count;
                if(a > 0)
                {
                    foreach (Student selectedStudent in dataGrid.SelectedItems)
                    {
                        CourseWindow.Course.ListOfStudents.Add(selectedStudent);
                        CourseWindow.AddedStudents.Add(selectedStudent);
                    }
                    Close();
                }
                else
                {
                    MessageBox.Show("Morate da selektujete ucenika kako biste ga preuzeli!");
                }
            }
        }

        private void dataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if(TeacherWindow == null && CourseWindow == null)
            {
                LoadColumnsHelper.LoadCourse(e);
            }
            else if(CourseWindow == null && StudentWindow == null)
            {
                LoadColumnsHelper.LoadLanguage(e);
            }
            else
            {
                LoadColumnsHelper.LoadStudent(e);
            }
        }
    }
}
