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
            else
            {
                var result = MessageBox.Show("Da li ste sigurni da hocete da obrisete ovog nastavnika?", "Upozorenje", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    // komanda za brisanje iz baze
                    selectedTeacher = TeachersView.CurrentItem as Teacher;
                    ListOfTeachers.Remove(selectedTeacher);
                }
            }
        }

        private void sortbtn_Click(object sender, RoutedEventArgs e)
        {
            // problem oko bool? (moze da bude i null) i bool
            bool ascending = sortAscrb.IsChecked ?? false;

            if (idrb.IsChecked ?? false) ; //funkcija za sortiranje
            else if (lastnamerb.IsChecked ?? false) ; //funkcija za sortiranje
            else if (firstnamerb.IsChecked ?? false) ; //funkcija za sortiranje
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
                // pokupi podatke iz textboxa za ime i prezime
                // funkcija
            }
            else if (firstName && jmbg)
            {
                
            }
            else if (lastName && jmbg)
            {
                
            }
            else
            {
                MessageBox.Show("Morate da otkacite makar jedan kriterijum pretrage!");
            }
        }

        private void usersdg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }
    }
}
