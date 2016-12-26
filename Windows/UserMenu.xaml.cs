using POP_SF7.Helpers;
using POP_SF7.Windows;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace POP_SF7
{
    /// <summary>
    /// Interaction logic for UserMenu.xaml
    /// </summary>

    public partial class UserMenu : Window
    {
        public ICollectionView view { get; set; }

        public UserMenu()
        {
            InitializeComponent();

            view = CollectionViewSource.GetDefaultView(ApplicationA.Instance.Users);
            usersdg.ItemsSource = view;
            usersdg.IsSynchronizedWithCurrentItem = true;
        }

        private void addbtn_Click(object sender, RoutedEventArgs e)
        {
            User newUser = new User();
            UserAddEdit addUser = new UserAddEdit(newUser, Decider.ADD);
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
                UserAddEdit edit = new UserAddEdit(selectedUser, Decider.EDIT);
                if(edit.ShowDialog() != true)
                {
                    int index = ApplicationA.Instance.Users.IndexOf(selectedUser);
                    ApplicationA.Instance.Users[index] = backup;
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
            else if (selectedUser.Deleted == true)
            {
                MessageBox.Show("Selektovani korisnik je vec obrisan!");
            }
            else
            {
                if(selectedUser.Id == ApplicationA.Instance.UserId)
                {
                    MessageBox.Show("Ne mozete da obrisete sebe!");
                }
                else
                {
                    var result = MessageBox.Show("Da li ste sigurni da hocete da obrisete ovog korisnika?", "Upozorenje", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    if (result == MessageBoxResult.Yes)
                    {
                        selectedUser = view.CurrentItem as User;
                        User.Delete(selectedUser);
                        selectedUser.Deleted = true;
                    }
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
            bool userName = usernamechb.IsChecked ?? false;

            Search s = new Search(firstnametb.Text, lastnametb.Text, usernametb.Text, PeopleDecider.User);
            Predicate<object> firstNamePredicate = new Predicate<object>(s.firstname);
            Predicate<object> lastNamePredicate = new Predicate<object>(s.lastname);
            Predicate<object> userNamePredicate = new Predicate<object>(s.username);

            if (firstName && lastName && userName)
            {
                view.Filter = firstNamePredicate + lastNamePredicate + userNamePredicate;
            }
            else if (firstName && lastName)
            {
                view.Filter = firstNamePredicate + lastNamePredicate;
            }
            else if (firstName && userName)
            {
                view.Filter = firstNamePredicate + userNamePredicate;
            }
            else if (lastName && userName)
            {
                view.Filter = lastNamePredicate + userNamePredicate;
            }
            else if (firstName)
            {
                view.Filter = firstNamePredicate;
            }
            else if (lastName)
            {
                view.Filter = lastNamePredicate;
            }
            else if (userName)
            {
                view.Filter = userNamePredicate;
            }
            else
            {
                MessageBox.Show("Morate da otkacite makar jedan kriterijum kako biste pretrazili ucenike!");
            }
        }

        private void cancelSearchbtn_Click(object sender, RoutedEventArgs e)
        {
            view.Filter = null;
        }

        private void usersdg_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            switch((string)e.Column.Header)
            {
                case "Id":
                    e.Cancel = true;
                    break;
                case "FirstName":
                    e.Column.Header = "Ime";
                    break;
                case "LastName":
                    e.Column.Header = "Prezime";
                    break;
                case "Address":
                    e.Column.Header = "Adresa";
                    break;
                case "Jmbg":
                    e.Column.Header = "JMBG";
                    break;
                case "Deleted":
                    e.Column.Header = "Obrisan";
                    break;
                case "UserName":
                    e.Column.Header = "Korisnicko ime";
                    break;
                case "Password":
                    e.Column.Header = "Lozinka";
                    break;
                case "UserRole":
                    e.Column.Header = "Uloga";
                    break;
                case "Error":
                    e.Cancel = true;
                    break;
            }
        }
    }
}
