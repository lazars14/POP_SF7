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
    /// Interaction logic for CourseMenu.xaml
    /// </summary>
    public partial class CourseMenu : Window
    {
        public CourseMenu()
        {
            InitializeComponent();
        }

        private void addbtn_Click(object sender, RoutedEventArgs e)
        {
            CourseAddEdit addCourse = new CourseAddEdit(null);
            addCourse.Show();
        }

        private void editbtn_Click(object sender, RoutedEventArgs e)
        {
            // provera da li je selektovan kurs, ako nije message box sa upozorenjem
            Course selectedCourse = (Course) coursesdg.SelectedItem;
            CourseAddEdit editCourse = new CourseAddEdit(selectedCourse);
            editCourse.Show();
        }

        private void deletebtn_Click(object sender, RoutedEventArgs e)
        {
            // provera da li je selektovan kurs, ako nije message box sa upozorenjem
            Course selectedCourse = (Course)coursesdg.SelectedItem;
            // message dialog da li ste sigurni da hocete da obrisete ovaj kurs?
            // funkcija za brisanje
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
                // MessageBox koji kaze da mora da se selektuje nesto od ta dva ili oba
            }
        }

        private void coursesdg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Course selectedCourse = (Course) coursesdg.SelectedItem;
            studentsdg.ItemsSource = selectedCourse.ListOfStudents;
        }
    }
}
