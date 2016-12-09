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
    /// Interaction logic for CourseMenu.xaml
    /// </summary>
    public partial class CourseMenu : Window
    {
        public ICollectionView CoursesView { get; set; }
        public ICollectionView StudentsView { get; set; }
        public ObservableCollection<Course> ListOfCourses { get; set; }
        public ObservableCollection<Student> ListOfStudents { get; set; }

        public List<Language> ListOfLanguages { get; set; }
        public List<CourseType> ListOfCourseTypes { get; set; }

        public CourseMenu()
        {
            InitializeComponent();
            // ucitavanje svih podataka iz baze
            ListOfCourses = new ObservableCollection<Course>();
            ListOfStudents = new ObservableCollection<Student>();
            ListOfLanguages = new List<Language>();
            ListOfCourseTypes = new List<CourseType>();

            CoursesView = CollectionViewSource.GetDefaultView(ListOfCourses);

            coursesdg.ItemsSource = CoursesView;
            coursesdg.IsSynchronizedWithCurrentItem = true;
        }

        private void addbtn_Click(object sender, RoutedEventArgs e)
        {
            Course newCourse = new Course();
            CourseAddEdit add = new CourseAddEdit(newCourse, Decider.ADD, ListOfCourses);
            add.Show();
        }

        private void editbtn_Click(object sender, RoutedEventArgs e)
        {
            Course selectedCourse = CoursesView.CurrentItem as Course;
            if(selectedCourse == null)
            {
                MessageBox.Show("Morate da selektujete neki kurs da biste ga izmenili!");
            }
            else
            {
                Course backup = (Course)selectedCourse.Clone();
                CourseAddEdit edit = new CourseAddEdit(selectedCourse, Decider.EDIT, ListOfCourses);
                if(edit.ShowDialog() != true)
                {
                    int index = ListOfCourses.IndexOf(selectedCourse);
                    ListOfCourses[index] = backup;
                }
            }
        }

        private void deletebtn_Click(object sender, RoutedEventArgs e)
        {
            Course selectedCourse = CoursesView.CurrentItem as Course;
            if(selectedCourse == null)
            {
                MessageBox.Show("Morate da selektujete neki kurs da biste ga obrisali!");
            }
            else
            {
                var result = MessageBox.Show("Da li ste sigurni da hocete da obrisete ovog korisnika?", "Upozorenje", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    // komanda za brisanje iz baze
                    selectedCourse = CoursesView.CurrentItem as Course;
                    ListOfCourses.Remove(selectedCourse);
                }
            }
        }

        private void sortbtn_Click(object sender, RoutedEventArgs e)
        {
            // problem oko bool? (moze da bude i null) i bool
            bool ascending = sortAscrb.IsChecked ?? false;

            if (idrb.IsChecked ?? false) ; //funkcija za sortiranje
            else if (pricerb.IsChecked ?? false) ; //funkcija za sortiranje
            else if (numberOfStudentsrb.IsChecked ?? false) ; //funkcija za sortiranje
            else if (startDaterb.IsChecked ?? false) ; //funkcija za sortiranje
            else if (endDaterb.IsChecked ?? false) ; //funkcija za sortiranje 
        }

        private void searchbtn_Click(object sender, RoutedEventArgs e)
        {
            bool language = languagechb.IsChecked ?? false;
            bool courseType = courseTypechb.IsChecked ?? false;


            if (language && courseType)
            {
                // pokupi podatke iz oba comboboxa
                // funkcija
            }
            else if(language)
            {
                // pokupi podatke iz comboboxa za jezik
                // funkcija
            }
            else if(courseType)
            {
                // pokupi podatke iz comboboxa za tip kursa
                // funkcija
            }
            else
            {
                MessageBox.Show("Morate da otkacite makar jedan kriterijum da biste pretrazili kurs!");
            }
        }

        private void coursesdg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }
    }
}
