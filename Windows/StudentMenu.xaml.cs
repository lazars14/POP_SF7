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
            else
            {
                var result = MessageBox.Show("Da li ste sigurni da hocete da obrisete ovog ucenika?", "Upozorenje", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    // komanda za brisanje iz baze
                    selectedStudent = StudentsView.CurrentItem as Student;
                    ListOfStudents.Remove(selectedStudent);
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

            if (firstName && lastName && jmbg)
            {

            }
            else if (firstName && lastName)
            {
                
            }
            else if (firstName && jmbg)
            {
                
            }
            else if (lastName && jmbg)
            {
                
            }
            else if (firstName)
            {

            }
            else if (lastName)
            {

            }
            else if (jmbg)
            {

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
    }
}
