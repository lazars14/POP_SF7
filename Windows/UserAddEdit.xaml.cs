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
        public ObservableCollection<User> ListOfUsers { get; set; }

        public string labelAddUser = "Dodavanje novog korisnika";
        public string labelEditUser = "Izmena postojeceg korisnika";
        
        public UserAddEdit(User userU, Decider decider, ObservableCollection<User> listOfUsers)
        {
            InitializeComponent();
            UserU = userU;
            Decider = decider;
            ListOfUsers = listOfUsers;
            DataContext = UserU;

            descriptionlbl.Text = (decider == Decider.ADD) ? labelAddUser : labelEditUser;
        }

        private void okbtn_Click(object sender, RoutedEventArgs e)
        {
            if(Decider == Decider.ADD)
            {
                // dodavanje u bazi
                ListOfUsers.Add(UserU);
            }
            else
            {
                // izmena u bazi
            }
            Close();
        }
    }
}
