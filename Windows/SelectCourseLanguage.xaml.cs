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
                StudentWindow.StudentS.ListOfCourses.Add(selectedCourse);
            }
            else
            {
                Language selectedLanguage = dataGrid.SelectedItem as Language;
                TeacherWindow.TeacherT.ListOfLanguages.Add(selectedLanguage);
            }
        }
    }
}
