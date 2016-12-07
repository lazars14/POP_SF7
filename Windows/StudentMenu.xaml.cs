using System;
using System.Collections.Generic;
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
        public string labelUsers = "Korisnici";
        public string labelTeachers = "Nastavnici";
        public string labelStudents = "Ucenici";

        public StudentMenu()
        {
            InitializeComponent();
            setupWindow();
        }

        public void setupWindow()
        {
            switch (Decider)
            {
                case PeopleDecider.User:
                    userstb.Text = labelUsers;
                    jmbgchb.Content = "Korisnicko ime";
                    coursesgb.Visibility = Visibility.Collapsed;
                    dynamicgp.Visibility = Visibility.Collapsed;
                    break;
                case PeopleDecider.Teacher:
                    userstb.Text = labelTeachers;
                    dynamicgp.Header = "Jezici";
                    break;
                case PeopleDecider.Student:
                    userstb.Text = labelStudents;
                    dynamicgp.Header = "Uplate";
                    break;
            }
        }

        private void addbtn_Click(object sender, RoutedEventArgs e)
        {
            PersonAddEdit addUser = new PersonAddEdit(Decider);
            addUser.Show();
        }

        private void editbtn_Click(object sender, RoutedEventArgs e)
        {
            // provera da li je selektovana osoba, ako nije message box sa upozorenjem
            switch (Decider)
            {
                case PeopleDecider.User:
                    User selectedUser = (User)usersdg.SelectedItem;
                    PersonAddEdit addUser = new PersonAddEdit(selectedUser, Decider);
                    addUser.Show();
                    break;
                case PeopleDecider.Teacher:
                    Teacher selectedTeacher = (Teacher)usersdg.SelectedItem;
                    PersonAddEdit addTeacher = new PersonAddEdit(selectedTeacher, Decider);
                    addTeacher.Show();
                    break;
                case PeopleDecider.Student:
                    Student selectedStudent = (Student)usersdg.SelectedItem;
                    PersonAddEdit addStudent = new PersonAddEdit(selectedStudent, Decider);
                    addStudent.Show();
                    break;
            }
        }

        private void deletebtn_Click(object sender, RoutedEventArgs e)
        {
            // provera da li je selektovana osoba, ako nije message box sa upozorenjem
            switch (Decider)
            {
                case PeopleDecider.User:
                    User selectedUser = (User)usersdg.SelectedItem;
                    // message dialog da li ste sigurni da hocete da obrisete ovu osobu?
                    // funkcija za brisanje
                    break;
                case PeopleDecider.Teacher:
                    Teacher selectedTeacher = (Teacher)usersdg.SelectedItem;
                    // message dialog da li ste sigurni da hocete da obrisete ovu osobu?
                    // funkcija za brisanje
                    break;
                case PeopleDecider.Student:
                    Student selectedStudent = (Student)usersdg.SelectedItem;
                    // message dialog da li ste sigurni da hocete da obrisete ovu osobu?
                    // funkcija za brisanje
                    break;
            }
            
        }

        private void sortbtn_Click(object sender, RoutedEventArgs e)
        {
            // problem oko bool? (moze da bude i null) i bool
            bool ascending = sortAscrb.IsChecked ?? false;

            if (idrb.IsChecked ?? false) ; //funkcija za sortiranje
            else if (lastnamerb.IsChecked ?? false) ; //funkcija za sortiranje
            else if (usernamerb.IsChecked ?? false) ; //funkcija za sortiranje
        }

        private void searchbtn_Click(object sender, RoutedEventArgs e)
        {
            bool firstName = firstnamechb.IsChecked ?? false;
            bool lastName = lastnamechb.IsChecked ?? false;
            bool jmbgOrUsername = jmbgchb.IsChecked ?? false;
            if (firstName && lastName && jmbgOrUsername)
            {
                if (jmbgchb.Content.Equals("Korisnicko ime"))
                {
                    // pretrazi po username-u, ne po jmbg-u
                }
                else
                {
                    // pretrazi po jmbg-u
                }
                // pokupi podatke iz sva tri textboxa
                // funkcija
            }
            else if (firstName && lastName)
            {
                // pokupi podatke iz textboxa za ime i prezime
                // funkcija
            }
            else if (firstName && jmbgOrUsername)
            {
                // provera da li je u pitanju user ili jedan od ova dva
                if(jmbgchb.Content.Equals("Korisnicko ime"))
                {
                    // pretrazi po username-u, ne po jmbg-u
                }
                else
                {
                    // pretrazi po jmbg-u
                }
                // pokupi podatke iz textboxa za ime i jmbg(username)
                // funkcija
            }
            else if (lastName && jmbgOrUsername)
            {
                if (jmbgchb.Content.Equals("Korisnicko ime"))
                {
                    // pretrazi po username-u, ne po jmbg-u
                }
                else
                {
                    // pretrazi po jmbg-u
                }
                // pokupi podatke iz textboxa za prezime i jmbg(username)
                // funkcija
            }
            else
            {
                // MessageBox koji kaze da mora da se selektuje nesto od ta tri ili sve
            }
        }

        private void usersdg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(Decider == PeopleDecider.Teacher)
            {
                Teacher selectedTeacher = (Teacher)usersdg.SelectedItem;
                coursesdg.ItemsSource = selectedTeacher.ListOfCourses;
                dynamicdg.ItemsSource = selectedTeacher.ListOfLanguages;
            }
            else if (Decider == PeopleDecider.Student)
            {
                Student selectedTeacher = (Student)usersdg.SelectedItem;
                coursesdg.ItemsSource = selectedTeacher.ListOfCourses;
                dynamicdg.ItemsSource = selectedTeacher.ListOfPayments;
            }
        }
    }
}
