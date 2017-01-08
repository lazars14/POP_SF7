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

    public partial class SelectCourseLanguage : Window
    {
        public StudentAddEdit StudentWindow { get; set; }
        public TeacherAddEdit TeacherWindow { get; set; }

        public string addLanguage = "Dodavanje jezika";
        public string addCourse = "Dodavanje kursa";

        public SelectCourseLanguage(TeacherAddEdit teacherAddEdit)
        {
            InitializeComponent();

            TeacherWindow = teacherAddEdit;

            setupWindow();
        }

        public SelectCourseLanguage(StudentAddEdit studentAddEdit)
        {
            InitializeComponent();

            StudentWindow = studentAddEdit;

            setupWindow();
        }

        private void setupWindow()
        {
            if(TeacherWindow == null)
            {
                description.Text = addCourse;
                setupCourseGrid();
            }
            else
            {
                description.Text = addLanguage;
                setupLanguageGrid();
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
            if(TeacherWindow == null)
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
            else
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
        }

        private void dataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if(TeacherWindow == null)
            {
                LoadColumnsHelper.LoadCourse(e);
            }
            else
            {
                LoadColumnsHelper.LoadLanguage(e);
            }
        }
    }
}
