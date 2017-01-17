using POP_SF7.Windows;
using System.Windows;
using System.Windows.Controls;
using System;

namespace POP_SF7
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Window
    {
        public MainMenu(Role role, int userId)
        {
            InitializeComponent();
            DataContext = ApplicationA.Instance.SchoolS;
            ApplicationA.Instance.UserId = userId;

            setVisibilityRole(role);
        }

        private void setVisibilityRole(Role role)
        {
            if (role == Role.ADMINISTRATOR)
            {
                payments.Visibility = Visibility.Collapsed;
                students.Visibility = Visibility.Collapsed;
                courses.Visibility = Visibility.Collapsed;

                if(ApplicationA.AdminDataLoaded == false && ApplicationA.EmployeeDataLoaded == false)
                {
                    if (!ApplicationA.LoadAllDataAdministrator())
                    {
                        Close();
                    }
                }
            }
            else if (role == Role.EMPLOYEE)
            {
                editSchoolData.Visibility = Visibility.Collapsed;
                teachers.Visibility = Visibility.Collapsed;
                users.Visibility = Visibility.Collapsed;
                languages.Visibility = Visibility.Collapsed;
                courseTypes.Visibility = Visibility.Collapsed;

                if (ApplicationA.AdminDataLoaded == true && ApplicationA.EmployeeDataLoaded == false)
                {
                    if(!ApplicationA.LoadEmployeeAdminDifference())
                    {
                        Close();
                    }
                }
                else if(ApplicationA.AdminDataLoaded == false && ApplicationA.EmployeeDataLoaded == false)
                {
                    if(!ApplicationA.LoadAllDataEmployee())
                    {
                        Close();
                    }
                }
            }
        }

        private void editSchool()
        {
            SchoolS backup = (SchoolS) ApplicationA.Instance.SchoolS.Clone();
            SchoolEdit schoolEdit = new SchoolEdit(ApplicationA.Instance.SchoolS);
            if (schoolEdit.ShowDialog() != true)
            {
                ApplicationA.Instance.SchoolS = backup;
                // ispravljeno
                DataContext = ApplicationA.Instance.SchoolS;
            }
        }

        private void menuItem_Click(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = (MenuItem)sender;
            string menuItemName = menuItem.Name;

            switch(menuItemName)
            {
                case "editSchoolData":
                    editSchool();
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
