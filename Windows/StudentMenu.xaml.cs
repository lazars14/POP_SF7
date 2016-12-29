using POP_SF7.Helpers;
using POP_SF7.Windows;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace POP_SF7
{
    /// <summary>
    /// Interaction logic for UserMenu.xaml
    /// </summary>

    public partial class StudentMenu : Window
    {
        public ICollectionView StudentsView { get; set; }
        public ICollectionView CoursesView { get; set; }
        public ICollectionView PaymentsView { get; set; }

        public StudentMenu()
        {
            InitializeComponent();

            StudentsView = CollectionViewSource.GetDefaultView(ApplicationA.Instance.Students);

            studentsdg.ItemsSource = StudentsView;
            studentsdg.IsSynchronizedWithCurrentItem = true;
        }

        private void addbtn_Click(object sender, RoutedEventArgs e)
        {
            Student newStudent = new Student();
            StudentAddEdit addUser = new StudentAddEdit(newStudent, Decider.ADD);
            addUser.Show();
        }

        private void editbtn_Click(object sender, RoutedEventArgs e)
        {
            Student selectedStudent = StudentsView.CurrentItem as Student;
            if (selectedStudent == null)
            {
                MessageBox.Show("Morate da selektujete nekog ucenika kako biste ga izmenili!");
            }
            else
            {
                Student backup = (Student)selectedStudent.Clone();
                StudentAddEdit edit = new StudentAddEdit(selectedStudent, Decider.EDIT);
                if (edit.ShowDialog() != true)
                {
                    int index = ApplicationA.Instance.Students.IndexOf(selectedStudent);
                    ApplicationA.Instance.Students[index] = backup;
                }
            }
        }

        private void deletebtn_Click(object sender, RoutedEventArgs e)
        {
            Student selectedStudent = StudentsView.CurrentItem as Student;
            if (selectedStudent == null)
            {
                MessageBox.Show("Morate da selektujete nekog ucenika kako biste ga obrisali!");
            }
            else if (selectedStudent.Deleted == true)
            {
                MessageBox.Show("Selektovani ucenik je vec obrisan!");
            }
            else
            {
                var result = MessageBox.Show("Da li ste sigurni da hocete da obrisete ovog ucenika?", "Upozorenje", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    selectedStudent = StudentsView.CurrentItem as Student;
                    Student.Delete(selectedStudent);
                    selectedStudent.Deleted = true;
                }
            }
        }

        private void sortbtn_Click(object sender, RoutedEventArgs e)
        {
            // problem oko bool? (moze da bude i null) i bool
            bool ascending = sortAscrb.IsChecked ?? false;
            ListSortDirection direction = (ascending == true) ? ListSortDirection.Ascending : ListSortDirection.Descending;

            StudentsView.SortDescriptions.Clear();
            if (idrb.IsChecked ?? false)
            {
                StudentsView.SortDescriptions.Add(new SortDescription("Id", direction));
            }
            else if (lastnamerb.IsChecked ?? false)
            {
                StudentsView.SortDescriptions.Add(new SortDescription("LastName", direction));
            }
            else if (firstnamerb.IsChecked ?? false)
            {
                StudentsView.SortDescriptions.Add(new SortDescription("FirstName", direction));
            }
            StudentsView.Refresh();
        }

        private void searchbtn_Click(object sender, RoutedEventArgs e)
        {
            bool firstName = firstnamechb.IsChecked ?? false;
            bool lastName = lastnamechb.IsChecked ?? false;
            bool jmbg = jmbgchb.IsChecked ?? false;

            Search s = new Search(firstnametb.Text, lastnametb.Text, jmbgtb.Text, PeopleDecider.Student);
            Predicate<object> firstNamePredicate = new Predicate<object>(s.firstname);
            Predicate<object> lastNamePredicate = new Predicate<object>(s.lastname);
            Predicate<object> jmbgPredicate = new Predicate<object>(s.jmbg);

            if (firstName && lastName && jmbg)
            {
                StudentsView.Filter = firstNamePredicate + lastNamePredicate + jmbgPredicate;
            }
            else if (firstName && lastName)
            {
                StudentsView.Filter = firstNamePredicate + lastNamePredicate;
            }
            else if (firstName && jmbg)
            {
                StudentsView.Filter = firstNamePredicate + jmbgPredicate;
            }
            else if (lastName && jmbg)
            {
                StudentsView.Filter = lastNamePredicate + jmbgPredicate;
            }
            else if (firstName)
            {
                StudentsView.Filter = firstNamePredicate;
            }
            else if (lastName)
            {
                StudentsView.Filter = lastNamePredicate;
            }
            else if (jmbg)
            {
                StudentsView.Filter = jmbgPredicate;
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
            StudentsView.Filter = null;
        }

        private void studentsdg_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            switch ((string)e.Column.Header)
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
                case "ListOfPayments":
                    e.Cancel = true;
                    break;
                case "ListOfCourses":
                    e.Cancel = true;
                    break;
                case "Error":
                    e.Cancel = true;
                    break;
            }
        }

        private void coursesdg_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            switch ((string)e.Column.Header)
            {
                case "Id":
                    e.Cancel = true;
                    break;
                case "Language":
                    e.Cancel = true;
                    break;
                case "CourseType":
                    e.Cancel = true;
                    break;
                case "Price":
                    e.Column.Header = "Cena";
                    break;
                case "ListOfStudents":
                    e.Cancel = true;
                    break;
                case "Teacher":
                    e.Cancel = true;
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
                case "Error":
                    e.Cancel = true;
                    break;
            }
        }

        private void paymentsdg_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            switch ((string)e.Column.Header)
            {
                case "Id":
                    e.Cancel = true;
                    break;
                case "Course":
                    e.Cancel = true;
                    break;
                case "Student":
                    e.Cancel = true;
                    break;
                case "Amount":
                    e.Column.Header = "Iznos";
                    break;
                case "Date":
                    e.Column.Header = "Datum";
                    break;
                case "Deleted":
                    e.Column.Header = "Obrisano";
                    break;
                case "Error":
                    e.Cancel = true;
                    break;
            }
        }
    }
}
