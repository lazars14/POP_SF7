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
    }
}
