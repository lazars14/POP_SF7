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

namespace POP_SF7
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Window
    {
        public MainMenu(Role role)
        {
            InitializeComponent();

            if(role == Role.Administrator)
            {
                payments.Visibility = Visibility.Collapsed;
                students.Visibility = Visibility.Collapsed;
                courses.Visibility = Visibility.Collapsed;
            }
            else if(role == Role.Employee)
            {
                editSchoolData.Visibility = Visibility.Collapsed;
                teachers.Visibility = Visibility.Collapsed;
                users.Visibility = Visibility.Collapsed;
                languages.Visibility = Visibility.Collapsed;
                courseTypes.Visibility = Visibility.Collapsed;
            }
        }

        private void menuItem_Click(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = (MenuItem)sender;
            string menuItemName = menuItem.Name;

            switch(menuItemName)
            {
                case "editSchoolData":
                    SchoolEdit schoolEdit = new SchoolEdit();
                    schoolEdit.Show();
                    break;
                case "payments":
                    PaymentMenu payments = new PaymentMenu();
                    payments.Show();
                    break;
                case "languages":
                    LanguagesCourseTypesMenu languages = new LanguagesCourseTypesMenu(DeciderLanguageCourseType.Language);
                    languages.Show();
                    break;
                case "courseTypes":
                    LanguagesCourseTypesMenu courseTypes = new LanguagesCourseTypesMenu(DeciderLanguageCourseType.CourseType);
                    courseTypes.Show();
                    break;
                case "courses":
                    CourseMenu courses = new CourseMenu();
                    courses.Show();
                    break;
                case "users":
                    PeopleMenu users = new PeopleMenu(PeopleDecider.User);
                    users.Show();
                    break;
                case "teachers":
                    PeopleMenu teachers = new PeopleMenu(PeopleDecider.Teacher);
                    teachers.Show();
                    break;
                case "students":
                    PeopleMenu students = new PeopleMenu(PeopleDecider.Student);
                    students.Show();
                    break;
            }
        }

        
    }
}
