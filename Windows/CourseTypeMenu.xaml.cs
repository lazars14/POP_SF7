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
    /// Interaction logic for LanguagesCourseTypesMenu.xaml
    /// </summary>
    /// 

    public partial class CourseTypeMenu : Window
    {
        public ICollectionView view{ get; set; }
        public ObservableCollection<CourseType> ListOfCourseTypes { get; set; }

        public CourseTypeMenu()
        {
            InitializeComponent();
            // ucitavanje tipova kurseva iz baze u listu
            ListOfCourseTypes = new ObservableCollection<CourseType>();
            view = CollectionViewSource.GetDefaultView(ListOfCourseTypes);

            dynamicdg.ItemsSource = view;
            dynamicdg.IsSynchronizedWithCurrentItem = true;
        }

        private void addbtn_Click(object sender, RoutedEventArgs e)
        {
            CourseType newCourseType = new CourseType();
            CourseTypeAddEdit add = new CourseTypeAddEdit(newCourseType, Decider.ADD, ListOfCourseTypes);
            add.Show();
        }

        private void editbtn_Click(object sender, RoutedEventArgs e)
        {
            CourseType selectedCourseType = (CourseType)dynamicdg.SelectedItem;
            if(selectedCourseType == null)
            {
                MessageBox.Show("Morate da selektujete red u tabeli kako bi izmenili tip kursa!");
            }
            else
            {
                CourseType backup = (CourseType)selectedCourseType.Clone();
           
                CourseTypeAddEdit edit = new CourseTypeAddEdit(selectedCourseType, Decider.EDIT, ListOfCourseTypes);
                if(edit.ShowDialog() != true)
                {
                    int index = ListOfCourseTypes.IndexOf(selectedCourseType);
                    ListOfCourseTypes[index] = backup;
                }
            }
        }

        private void deletebtn_Click(object sender, RoutedEventArgs e)
        {
            CourseType selectedCourseType = (CourseType)dynamicdg.SelectedItem;
            if (selectedCourseType == null)
            {
                MessageBox.Show("Morate da selektujete red u tabeli kako bi izmenili tip kursa!");
            }
            else
            {
                var result = MessageBox.Show("Da li ste sigurni da hocete da obrisete ovaj tip kursa?", "Upozorenje", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if(result == MessageBoxResult.Yes)
                {
                    // komanda za brisanje iz baze
                    selectedCourseType = view.CurrentItem as CourseType;
                    ListOfCourseTypes.Remove(selectedCourseType);
                }
            }
        }

        private void closebtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
