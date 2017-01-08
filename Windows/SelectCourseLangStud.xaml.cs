using POP_SF7.Helpers;
using POP_SF7.School;
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
                if(!CourseWindow.ListOfStudents.Contains(student))
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
                if(!TeacherWindow.TeacherT.ListOfLanguages.Contains(language))
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
                if(!StudentWindow.StudentS.ListOfCourses.Contains(course))
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
                Course selectedCourse = dataGrid.SelectedItem as Course;
                if(selectedCourse == null)
                {
                    MessageBox.Show("Morate da selektujete kurs kako biste ga dodali na ucenika!");
                }
                else
                {
                    int nextId = ApplicationA.Instance.StudentAttendsCourseCollection.Count() + 1;
                    StudentAttendsCourse toAdd = new StudentAttendsCourse(nextId, selectedCourse.Id, StudentWindow.StudentS.Id, false);

                    if (StudentAttendsCourse.Add(toAdd))
                    {
                        ApplicationA.Instance.StudentAttendsCourseCollection.Add(toAdd);
                        StudentWindow.StudentS.ListOfCourses.Add(selectedCourse);
                    }
                    Close();
                }
            }
            else if(CourseWindow == null && StudentWindow == null)
            {
                Language selectedLanguage = dataGrid.SelectedItem as Language;
                if(selectedLanguage == null)
                {
                    MessageBox.Show("Morate da selektujete jezik kako biste ga dodali na nastavnika!");
                }
                else
                {
                    int nextId = ApplicationA.Instance.Languages.Count() + 1;
                    TeacherTeachesLanguage toAdd = new TeacherTeachesLanguage(nextId, TeacherWindow.TeacherT.Id, selectedLanguage.Id, false);
                    if(TeacherTeachesLanguage.Add(toAdd))
                    {
                        ApplicationA.Instance.TeacherTeachesLanguageCollection.Add(toAdd);
                        TeacherWindow.TeacherT.ListOfLanguages.Add(selectedLanguage);
                    }
                    Close();
                }
            }
            else
            {
                Student selectedStudent = dataGrid.SelectedItem as Student;
                if(selectedStudent == null)
                {
                    MessageBox.Show("Morate da selektujete ucenika kako biste ga preuzeli!");
                }
                else
                {
                    int nextId = ApplicationA.Instance.Students.Count() + 1;
                    StudentAttendsCourse toAdd = new StudentAttendsCourse(nextId, CourseWindow.Course.Id, selectedStudent.Id, false);
                    if(StudentAttendsCourse.Add(toAdd))
                    {
                        ApplicationA.Instance.StudentAttendsCourseCollection.Add(toAdd);
                        CourseWindow.ListOfStudents.Add(selectedStudent);
                    }
                    Close();
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
