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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace POP_SF7
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public List<User> ListOfUsers { get; set; }

        public LoginWindow()
        {
            InitializeComponent();
            // ucitavanje svih korisnika iz baze
            ListOfUsers = new List<User>();
            ListOfUsers.Add(new User(1, "ime", "prezime", "jmbg", "adresa", "admin", "admin", Role.Administrator, false));
            ListOfUsers.Add(new User(1, "ime", "prezime", "jmbg", "adresa", "user", "user", Role.Employee, false));
        }

        private void cancelbtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void okbtn_Click(object sender, RoutedEventArgs e)
        {
            bool valid = false;

            if (String.IsNullOrEmpty(usernametb.Text) || String.IsNullOrEmpty(passwordpb.Password))
            {
                MessageBox.Show("Morate da unesete korisnicko ime i lozinku kako biste zapoceli sa radom!");
            }
            else
            {
                foreach(User u in ListOfUsers)
                {
                    if (u.UserName.Equals(usernametb.Text) && u.Password.Equals(passwordpb.Password) && u.Deleted == false)
                    {
                        valid = true;
                        MainMenu mainMenu = new MainMenu(u.UserRole);
                        mainMenu.Show();
                        Close();
                    }
                }
                if(!valid)
                {
                    usernametb.Text = "";
                    passwordpb.Password = "";
                    MessageBox.Show("Korisnicko ime ili lozinka nisu ispravni!");
                }
            }
        }
    }
}
