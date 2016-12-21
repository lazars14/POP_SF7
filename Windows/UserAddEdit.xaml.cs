using POP_SF7.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for PersonAddEdit.xaml
    /// </summary>
    public partial class UserAddEdit : Window
    {
        public User UserU { get; set; }
        public Decider Decider { get; set; }
        public string UserName { get; set; }

        public string labelAddUser = "Dodavanje novog korisnika";
        public string labelEditUser = "Izmena postojeceg korisnika";
        
        public UserAddEdit(User userU, Decider decider)
        {
            InitializeComponent();
            UserU = userU;
            Decider = decider;
            DataContext = UserU;

            personInfo.descriptionlbl.Text = (decider == Decider.ADD) ? labelAddUser : labelEditUser;
            UserName = (decider == Decider.EDIT) ? UserU.UserName : "";
            setRadioButton();
        }

        private void okbtn_Click(object sender, RoutedEventArgs e)
        {
            bool valid = true;
            string message = "Postoji korisnik sa unetim korisnickim imenom! Unesite neko drugo.";
            valid = checkUsername();
            if(valid)
            {
                if (Decider == Decider.ADD)
                {
                    User.Add(UserU);
                    UserU.Id = ApplicationA.Instance.Users.Count() + 1;
                    ApplicationA.Instance.Users.Add(UserU);
                    Close();
                }
                else
                {
                    User.Edit(UserU);
                    Close();
                }
            }
            else
            {
                MessageBox.Show(message);
            }

        }

        public bool checkUsername()
        {
            bool valid = true;
            foreach (User u in ApplicationA.Instance.Users)
            {
                if (u.UserName.Equals(UserU.UserName))
                {
                    if(Decider == Decider.EDIT)
                    {
                        if(!UserU.UserName.Equals(UserName) || UserU.Id != u.Id)
                        {
                            valid = false;
                        }
                    }
                    else
                    {
                        valid = false;
                    }
                }
            }
            return valid;
        }
        
        public void setRadioButton()
        {
            if(Decider == Decider.EDIT)
            {
                if (UserU.UserRole == Role.Administrator)
                {
                    administratorrb.IsChecked = true;
                }
                else
                {
                    employeerb.IsChecked = true;
                }
            }
            else
            {
                UserU.UserRole = Role.Administrator;
            }
        }

        private void administratorrb_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            try
            {
                UserU.UserRole = (rb.Name.Equals("administratorrb")) ? Role.Administrator : Role.Employee;
            }
            catch(NullReferenceException a)
            {
                Console.WriteLine(a.StackTrace);
            }     
        }
    }
}
