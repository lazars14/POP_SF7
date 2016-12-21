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

        public string labelAddUser = "Dodavanje novog korisnika";
        public string labelEditUser = "Izmena postojeceg korisnika";
        
        public UserAddEdit(User userU, Decider decider)
        {
            InitializeComponent();
            UserU = userU;
            Decider = decider;
            DataContext = UserU;

            personInfo.descriptionlbl.Text = (decider == Decider.ADD) ? labelAddUser : labelEditUser;
            setRadioButton();
        }

        private void okbtn_Click(object sender, RoutedEventArgs e)
        {
            if(Decider == Decider.ADD)
            {
                // provera da li postoji korisnik sa tim koriscnickim imenom
                User.Add(UserU);
                ApplicationA.Instance.Users.Add(UserU);
            }
            else
            {
                // provera da li postoji korisnik sa tim koriscnickim imenom
                User.Edit(UserU);
            }
            Close();
        }

        public void setRadioButton()
        {
            if(personInfo.descriptionlbl.Text.Equals(labelEditUser))
            {
                if (UserU.UserRole == Role.Administrator) administratorrb.IsChecked = true;
                else employeerb.IsChecked = true;
            }
            else
            {
                UserU.UserRole = Role.Administrator;
            }
        }

        private void administratorrb_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if (rb.Name.Equals("administratorrb")) UserU.UserRole = Role.Administrator;
            else UserU.UserRole = Role.Employee;
        }
    }
}
