using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;

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
            if (ApplicationA.Instance.Users.Count == 0) User.Load();
        }

        private void cancelbtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void okbtn_Click(object sender, RoutedEventArgs e)
        {
            bool valid = false;
            bool userDeleted = false;

            string incorrectInput = "Korisnicko ime ili lozinka nisu ispravni!";
            string deletedUser = "Obrisani ste iz sistema! Obratite se administratoru.";

            if (String.IsNullOrEmpty(usernametb.Text) || String.IsNullOrEmpty(passwordpb.Password))
            {
                MessageBox.Show("Morate da unesete korisnicko ime i lozinku kako biste zapoceli sa radom!");
            }
            else
            {
                foreach(User u in ApplicationA.Instance.Users)
                {
                    if (u.UserName.Equals(usernametb.Text) && u.Password.Equals(passwordpb.Password) && u.Deleted == true)
                    {
                        userDeleted = true;
                    }
                    else if (u.UserName.Equals(usernametb.Text) && u.Password.Equals(passwordpb.Password) && u.Deleted == false)
                    {
                        valid = true;
                        MainMenu mainMenu = new MainMenu(u.UserRole, u.Id);
                        mainMenu.Show();
                        Close();
                    }
                }
                if(userDeleted)
                {
                    resetTextBoxes(deletedUser);
                }
                else if (!valid)
                {
                    resetTextBoxes(incorrectInput);
                }
            }
        }

        public void resetTextBoxes(string message)
        {
            usernametb.Text = "";
            passwordpb.Password = "";
            usernametb.Focus();
            MessageBox.Show(message);
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
