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
    
    public partial class StudentMenu : Window
    {
        public ICollectionView StudentsView { get; set; }
        public ICollectionView CoursesView { get; set; }
        public ICollectionView PaymentsView { get; set; }

        public ObservableCollection<Student> ListOfStudents { get; set; }
        public ObservableCollection<Course> ListOfCourses { get; set; }
        public ObservableCollection<Payment> ListOfPayments { get; set; }


        public StudentMenu()
        {
            InitializeComponent();
            // ucitavanje iz baze
            ListOfStudents = new ObservableCollection<Student>();
            ListOfCourses = new ObservableCollection<Course>();
            ListOfPayments = new ObservableCollection<Payment>();

            StudentsView = CollectionViewSource.GetDefaultView(ListOfStudents);

            studentsdg.ItemsSource = StudentsView;
            studentsdg.IsSynchronizedWithCurrentItem = true;
        }

        private void addbtn_Click(object sender, RoutedEventArgs e)
        {
            Student newStudent = new Student();
            StudentAddEdit addUser = new StudentAddEdit(newStudent, Decider.ADD, ListOfStudents);
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
                StudentAddEdit edit = new StudentAddEdit(selectedStudent, Decider.EDIT, ListOfStudents);
                if (edit.ShowDialog() != true)
                {
                    int index = ListOfStudents.IndexOf(selectedStudent);
                    ListOfStudents[index] = backup;
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
            }
        }

        private void coursesdg_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            switch ((string)e.Column.Header)
            {
                case "Language":
                    e.Column.Header = "Obrisan";
                    break;
                case "CourseType":
                    e.Cancel = true;
                    break;
                case "Teacher":
                    e.Cancel = true;
                    break;
                case "ListOfStudents":
                    e.Cancel = true;
                    break;
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

        private void paymentsdg_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            switch ((string)e.Column.Header)
            {
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
            }
        }
    }
}
