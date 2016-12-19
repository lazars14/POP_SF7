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
        public LoginWindow()
        {
            InitializeComponent();
            loadData();

            usernametb.Focus();
        }

        public void loadData()
        {
            if (ApplicationA.Instance.Users.Count() == 0) User.Load();
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
                foreach(User u in ApplicationA.Instance.Users)
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

        private void passwordpb_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                okbtn_Click(null, null);
            }
        }
    }
}
