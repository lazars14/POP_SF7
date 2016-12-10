using POP_SF7.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
        public ICollectionView view { get; set; }
        public ObservableCollection<User> ListOfUsers { get; set; }

        public UserMenu()
        {
            InitializeComponent();
            // ucitavanje korisnika iz baze
            ListOfUsers = new ObservableCollection<User>();
            view = CollectionViewSource.GetDefaultView(ListOfUsers);

            usersdg.ItemsSource = view;
            usersdg.IsSynchronizedWithCurrentItem = true;
        }

        private void addbtn_Click(object sender, RoutedEventArgs e)
        {
            User newUser = new User();
            UserAddEdit addUser = new UserAddEdit(newUser, Decider.ADD, ListOfUsers);
            addUser.Show();
        }

        private void editbtn_Click(object sender, RoutedEventArgs e)
        {
            User selectedUser = view.CurrentItem as User;
            if(selectedUser == null)
            {
                MessageBox.Show("Morate da selektujete nekog korisnika kako biste ga izmenili!");
            }
            else
            {
                User backup = (User)selectedUser.Clone();
                UserAddEdit edit = new UserAddEdit(selectedUser, Decider.EDIT, ListOfUsers);
                if(edit.ShowDialog() != true)
                {
                    int index = ListOfUsers.IndexOf(selectedUser);
                    ListOfUsers[index] = backup;
                }
            }
        }

        private void deletebtn_Click(object sender, RoutedEventArgs e)
        {
            User selectedUser = view.CurrentItem as User;
            if (selectedUser == null)
            {
                MessageBox.Show("Morate da selektujete nekog korisnika kako biste ga obrisali!");
            }
            else
            {
                var result = MessageBox.Show("Da li ste sigurni da hocete da obrisete ovog korisnika?", "Upozorenje", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    // komanda za brisanje iz baze
                    selectedUser = view.CurrentItem as User;
                    ListOfUsers.Remove(selectedUser);
                }
            }
        }

        private void sortbtn_Click(object sender, RoutedEventArgs e)
        {
            // problem oko bool? (moze da bude i null) i bool
            bool ascending = sortAscrb.IsChecked ?? false;
            ListSortDirection direction = (ascending == true) ? ListSortDirection.Ascending : ListSortDirection.Descending;

            view.SortDescriptions.Clear();
            if (idrb.IsChecked ?? false)
            {
                view.SortDescriptions.Add(new SortDescription("Id", direction));
            }
            else if (lastnamerb.IsChecked ?? false)
            {
                view.SortDescriptions.Add(new SortDescription("LastName", direction));
            }
            else if (usernamerb.IsChecked ?? false)
            {
                view.SortDescriptions.Add(new SortDescription("UserName", direction));
            }
            view.Refresh();
        }

        private void searchbtn_Click(object sender, RoutedEventArgs e)
        {
            bool firstName = firstnamechb.IsChecked ?? false;
            bool lastName = lastnamechb.IsChecked ?? false;
            bool jmbgOrUsername = jmbgchb.IsChecked ?? false;

            string firstNameStr = firstnametb.Text;
            string lastNameStr = firstnametb.Text;
            string userNameStr = firstnametb.Text;

            if (firstName && lastName && jmbgOrUsername)
            {
                // f-ja za pretragu u bazi
            }
            else if (firstName && lastName)
            {
                // f-ja za pretragu u bazi
            }
            else if (firstName && jmbgOrUsername)
            {
                // f-ja za pretragu u bazi
            }
            else if (lastName && jmbgOrUsername)
            {
                // f-ja za pretragu u bazi
            }
            else
            {
                MessageBox.Show("Morate da otkacite jedan ili vise kriterijuma za pretragu!");
            }
        }
    }
}
