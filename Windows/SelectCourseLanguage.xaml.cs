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
    
    public enum LanguageCourseDecider { LANGUAGE, COURSE }

    public partial class SelectCourseLanguage : Window
    {
        public StudentAddEdit StudentWindow { get; set; }
        public TeacherAddEdit TeacherWindow { get; set; }

        public LanguageCourseDecider Decider { get; set; }

        public string addLanguage = "Dodavanje jezika";
        public string addCourse = "Dodavanje kursa";

        public SelectCourseLanguage(TeacherAddEdit teacherAddEdit, LanguageCourseDecider languageCourseDecider)
        {
            InitializeComponent();

            TeacherWindow = teacherAddEdit;
            Decider = languageCourseDecider;

            setupWindow();
        }

        public SelectCourseLanguage(StudentAddEdit studentAddEdit)
        {
            InitializeComponent();

            StudentWindow = studentAddEdit;
            Decider = LanguageCourseDecider.COURSE;

            setupWindow();
        }

        private void setupWindow()
        {
            if(Decider == LanguageCourseDecider.COURSE)
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
            // od svih jezika moram da oduzmem ove koje on vec zna, i onda da vidim za obrisane kako sta... mozda samo da obrisem fizicki jezik, ili mora logicki...
        }

        private void setupCourseGrid()
        {
            throw new NotImplementedException();
        }

        private void closebtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void okbtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
