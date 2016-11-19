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
    /// Interaction logic for UserMenu.xaml
    /// </summary>
    public partial class UserMenu : Window
    {
        public string labelUsers = "Korisnici";
        public string labelTeachers = "Nastavnici";
        public string labelStudents = "Ucenici";

        public UserMenu(string role)
        {
            InitializeComponent();
            switch(role)
            {
                case "Korisnik":
                    userstb.Text = labelUsers;
                    jmbgchb.Content = "Korisnicko ime";
                    coursesgb.Visibility = Visibility.Collapsed;
                    dynamicgp.Visibility = Visibility.Collapsed;
                    break;
                case "Nastavnik":
                    userstb.Text = labelTeachers;
                    dynamicgp.Header = "Jezici";
                    break;
                case "Ucenik":
                    userstb.Text = labelStudents;
                    dynamicgp.Header = "Uplate";
                    break;
            }
        }
    }
}
