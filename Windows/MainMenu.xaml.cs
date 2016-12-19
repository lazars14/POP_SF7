using POP_SF7.Windows;
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
        public School School { get; set; }

        public MainMenu(Role role)
        {
            InitializeComponent();
            School = ApplicationA.Instance.School;
            DataContext = School;

            setVisibilityRole(role);
        }

        public void setVisibilityRole(Role role)
        {
            if (role == Role.Administrator)
            {
                payments.Visibility = Visibility.Collapsed;
                students.Visibility = Visibility.Collapsed;
                courses.Visibility = Visibility.Collapsed;
            }
            else if (role == Role.Employee)
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
                    School backup = (School) School.Clone();
                    SchoolEdit schoolEdit = new SchoolEdit(School);
                    if(schoolEdit.ShowDialog() != true)
                    {
                        School = backup;
                    }
                    break;
                case "payments":
                    PaymentMenu payments = new PaymentMenu();
                    payments.Show();
                    break;
                case "languages":
                    LanguageMenu languages = new LanguageMenu();
                    languages.Show();
                    break;
                case "courseTypes":
                    CourseTypeMenu courseTypes = new CourseTypeMenu();
                    courseTypes.Show();
                    break;
                case "courses":
                    CourseMenu courses = new CourseMenu();
                    courses.Show();
                    break;
                case "users":
                    UserMenu users = new UserMenu();
                    users.Show();
                    break;
                case "teachers":
                    TeacherMenu teachers = new TeacherMenu();
                    teachers.Show();
                    break;
                case "students":
                    StudentMenu students = new StudentMenu();
                    students.Show();
                    break;
                case "logout":
                    LoginWindow login = new LoginWindow();
                    login.Show();
                    Close();
                    break;
            }
        }

        
    }
}
