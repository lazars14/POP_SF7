using POP_SF7.Helpers;
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
    
    public enum PeopleDecider { User, Teacher, Student}

    public partial class TeacherMenu : Window
    {
        public ICollectionView TeachersView { get; set; }
        public ICollectionView CoursesView { get; set; }
        public ICollectionView LanguagesView { get; set; }

        public ObservableCollection<Teacher> ListOfTeachers { get; set; }
        public ObservableCollection<Course> ListOfCourses { get; set; }
        public ObservableCollection<Language> ListOfLanguages { get; set; }

        public TeacherMenu()
        {
            InitializeComponent();
            // ucitavanje iz baze
            ListOfTeachers = new ObservableCollection<Teacher>();
            TeachersView = CollectionViewSource.GetDefaultView(ListOfTeachers);

            teachersdg.ItemsSource = TeachersView;
            teachersdg.IsSynchronizedWithCurrentItem = true;
        }

        private void addbtn_Click(object sender, RoutedEventArgs e)
        {
            Teacher newTeacher = new Teacher();
            TeacherAddEdit addUser = new TeacherAddEdit(newTeacher, Decider.ADD, ListOfTeachers);
            addUser.Show();
        }

        private void editbtn_Click(object sender, RoutedEventArgs e)
        {
            Teacher selectedTeacher = TeachersView.CurrentItem as Teacher;
            if(selectedTeacher == null)
            {
                MessageBox.Show("Morate da selektujete nastavnika u tabeli kako biste ga izmenili!");
            }
            else
            {
                Teacher backup = (Teacher)selectedTeacher.Clone();
                TeacherAddEdit edit = new TeacherAddEdit(selectedTeacher, Decider.EDIT, ListOfTeachers);
                if(edit.ShowDialog() != true)
                {
                    int index = ListOfTeachers.IndexOf(selectedTeacher);
                    ListOfTeachers[index] = backup;
                }
            }
        }

        private void deletebtn_Click(object sender, RoutedEventArgs e)
        {
            Teacher selectedTeacher = TeachersView.CurrentItem as Teacher;
            if (selectedTeacher == null)
            {
                MessageBox.Show("Morate da selektujete nastavnika u tabeli kako biste ga obrisali!");
            }
            else if (selectedTeacher.Deleted == true)
            {
                MessageBox.Show("Selektovani nastavnik je vec obrisan!");
            }
            else
            {
                var result = MessageBox.Show("Da li ste sigurni da hocete da obrisete ovog nastavnika?", "Upozorenje", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    selectedTeacher = TeachersView.CurrentItem as Teacher;
                    Teacher.Delete(selectedTeacher);
                    selectedTeacher.Deleted = true;
                }
            }
        }

        private void sortbtn_Click(object sender, RoutedEventArgs e)
        {
            // problem oko bool? (moze da bude i null) i bool
            bool ascending = sortAscrb.IsChecked ?? false;
            ListSortDirection direction = (ascending == true) ? ListSortDirection.Ascending : ListSortDirection.Descending;

            TeachersView.SortDescriptions.Clear();
            if (idrb.IsChecked ?? false)
            {
                TeachersView.SortDescriptions.Add(new SortDescription("Id", direction));
            }
            else if (lastnamerb.IsChecked ?? false)
            {
                TeachersView.SortDescriptions.Add(new SortDescription("LastName", direction));
            }
            else if (firstnamerb.IsChecked ?? false)
            {
                TeachersView.SortDescriptions.Add(new SortDescription("FirstName", direction));
            }
            TeachersView.Refresh();
        }

        private void searchbtn_Click(object sender, RoutedEventArgs e)
        {
            bool firstName = firstnamechb.IsChecked ?? false;
            bool lastName = lastnamechb.IsChecked ?? false;
            bool jmbg = jmbgchb.IsChecked ?? false;

            Search s = new Search(firstnametb.Text, lastnametb.Text, jmbgtb.Text, PeopleDecider.Teacher);
            Predicate<object> firstNamePredicate = new Predicate<object>(s.firstname);
            Predicate<object> lastNamePredicate = new Predicate<object>(s.lastname);
            Predicate<object> jmbgPredicate = new Predicate<object>(s.jmbg);

            if (firstName && lastName && jmbg)
            {
                TeachersView.Filter = firstNamePredicate + lastNamePredicate + jmbgPredicate;
            }
            else if (firstName && lastName)
            {
                TeachersView.Filter = firstNamePredicate + lastNamePredicate;
            }
            else if (firstName && jmbg)
            {
                TeachersView.Filter = firstNamePredicate + jmbgPredicate;
            }
            else if (lastName && jmbg)
            {
                TeachersView.Filter = lastNamePredicate + jmbgPredicate;
            }
            else if (firstName)
            {
                TeachersView.Filter = firstNamePredicate;
            }
            else if (lastName)
            {
                TeachersView.Filter = lastNamePredicate;
            }
            else if (jmbg)
            {
                TeachersView.Filter = jmbgPredicate;
            }
            else
            {
                MessageBox.Show("Morate da otkacite makar jedan kriterijum kako biste pretrazili ucenike!");
            }
        }

        private void usersdg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void cancelSearchbtn_Click(object sender, RoutedEventArgs e)
        {
            TeachersView.Filter = null;
        }

        private void teachersdg_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            switch ((string)e.Column.Header)
            {
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
                case "ListOfLanguages":
                    e.Cancel = true;
                    break;
                case "ListOfCourses":
                    e.Cancel = true;
                    break;
            }
        }

        private void languagesdg_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            switch ((string)e.Column.Header)
            {
                case "Name":
                    e.Column.Header = "Ime";
                    break;
                case "Deleted":
                    e.Column.Header = "Obrisano";
                    break;
            }
        }

        private void coursesdg_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            switch ((string)e.Column.Header)
            {
                case "Price":
                    e.Column.Header = "Cena";
                    break;
                case "StartDate":
                    e.Column.Header = "Datum pocetka";
                    break;
                case "EndDate":
                    e.Column.Header = "Datum kraja";
                    break;
                case "Deleted":
                    e.Column.Header = "Obrisan";
                    break;
            }
        }
    }
}
