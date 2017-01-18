using POP_SF7.DB;
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
    /// Interaction logic for CourseMenu.xaml
    /// </summary>
    public partial class CourseMenu : Window
    {
        public ICollectionView CoursesView { get; set; }

        public CourseMenu()
        {
            InitializeComponent();
            setupWindow();
        }

        private void setupWindow()
        {
            CoursesView = CollectionViewSource.GetDefaultView(ApplicationA.Instance.Courses);
            coursesdg.ItemsSource = CoursesView;
            coursesdg.IsSynchronizedWithCurrentItem = true;

            languagecb.ItemsSource = ApplicationA.Instance.Languages;
            setupComboBox(languagecb);

            courseTypecb.ItemsSource = ApplicationA.Instance.CourseTypes;
            setupComboBox(courseTypecb);
        }

        private void setupComboBox(ComboBox cb)
        {
            cb.DisplayMemberPath = "Name";
            cb.SelectedValuePath = "Id";
            cb.SelectedIndex = 0;
        }

        private void addbtn_Click(object sender, RoutedEventArgs e)
        {
            Course newCourse = new Course();
            CourseAddEdit add = new CourseAddEdit(newCourse, Decider.ADD);
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
                CourseAddEdit edit = new CourseAddEdit(selectedCourse, Decider.EDIT);
                if (edit.ShowDialog() != true)
                {
                    int index = ApplicationA.Instance.Courses.IndexOf(selectedCourse);
                    ApplicationA.Instance.Courses[index] = backup;
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
            else if (selectedCourse.Deleted == true)
            {
                MessageBox.Show("Selektovan kurs je vec obrisan!");
            }
            else
            {
                var result = MessageBox.Show("Da li ste sigurni da hocete da obrisete ovaj kurs?", "Upozorenje", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    selectedCourse = CoursesView.CurrentItem as Course;
                    if(CourseDAO.Delete(selectedCourse))
                    {
                        selectedCourse.Deleted = true;
                    }
                    
                }
            }
        }

        private void sortbtn_Click(object sender, RoutedEventArgs e)
        {
            // problem oko bool? (moze da bude i null) i bool
            bool ascending = sortAscrb.IsChecked ?? false;
            ListSortDirection direction = (ascending == true) ? ListSortDirection.Ascending : ListSortDirection.Descending;

            CoursesView.SortDescriptions.Clear();
            if (idrb.IsChecked ?? false)
            {
                CoursesView.SortDescriptions.Add(new SortDescription(("Id"), direction));
            }
            else if (pricerb.IsChecked ?? false)
            {
                CoursesView.SortDescriptions.Add(new SortDescription(("Price"), direction));
            }
            else if (startDaterb.IsChecked ?? false)
            {
                CoursesView.SortDescriptions.Add(new SortDescription(("StartDate"), direction));
            }
            else if (endDaterb.IsChecked ?? false)
            {
                CoursesView.SortDescriptions.Add(new SortDescription(("EndDate"), direction));
            }
            CoursesView.Refresh();
        }

        private bool languageSearchCondition(object s)
        {
            Course c = s as Course;
            return c.Language.Id == (int)languagecb.SelectedValue;
        }

        private bool courseTypeSearchCondition(object s)
        {
            Course c = s as Course;
            return c.CourseType.Id == (int)courseTypecb.SelectedValue;
        }

        private bool courseTypeAndLanguageSearchCondition(object s)
        {
            Course c = s as Course;
            return c.CourseType.Id == (int)courseTypecb.SelectedValue && c.Language.Id == (int)languagecb.SelectedValue; 
        }

        private bool finishedCoursesSearchCondition(object s)
        {
            Course c = s as Course;
            return c.EndDate <= DateTime.Today;
        }

        private bool ongoingCoursesSearchCondition(object s)
        {
            Course c = s as Course;
            return c.EndDate >= DateTime.Today;
        }

        private void searchbtn_Click(object sender, RoutedEventArgs e)
        {
            bool language = languagechb.IsChecked ?? false;
            bool courseType = courseTypechb.IsChecked ?? false;

            Predicate<object> languagePredicate = new Predicate<object>(languageSearchCondition);
            Predicate<object> courseTypePredicate = new Predicate<object>(courseTypeSearchCondition);
            Predicate<object> languageAndCourseType = new Predicate<object>(courseTypeAndLanguageSearchCondition);

            if (language && courseType)
            {
                CoursesView.Filter = languageAndCourseType;
            }
            else if(language)
            {
                CoursesView.Filter = languagePredicate;
            }
            else if(courseType)
            {
                CoursesView.Filter = courseTypePredicate;
            }
            else
            {
                MessageBox.Show("Morate da otkacite makar jedan kriterijum da biste pretrazili kurs!");
            }
        }

        private void coursesdg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Course selectedCourse = CoursesView.CurrentItem as Course;
            if(selectedCourse != null)
            {
                int selectedCourseId = selectedCourse.Id;
                if (selectedCourse.Language.Name == "")
                {
                    selectedCourse.Language = ApplicationA.Instance.Languages[selectedCourse.Language.Id - 1];
                    selectedCourse.CourseType = ApplicationA.Instance.CourseTypes[selectedCourse.CourseType.Id - 1];
                    selectedCourse.Teacher = ApplicationA.Instance.Teachers[selectedCourse.Teacher.Id - 1];
                }

                studentsdg.ItemsSource = selectedCourse.ListOfStudents;
            }
        }

        private void cancelSearchbtn_Click(object sender, RoutedEventArgs e)
        {
            CoursesView.Filter = null;
        }

        private void studentsdg_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            LoadColumnsHelper.LoadStudent(e);
        }

        private void coursesdg_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            LoadColumnsHelper.LoadCourse(e);
        }

        private void completedCoursesrb_Checked(object sender, RoutedEventArgs e)
        {
            Predicate<object> finishedCoursesPredicate = new Predicate<object>(finishedCoursesSearchCondition);
            Predicate<object> ongoingCoursesPredicate = new Predicate<object>(ongoingCoursesSearchCondition);

            bool allCourses = allCoursesrb.IsChecked ?? false;
            bool finishedCourses = completedCoursesrb.IsChecked ?? false;
            bool ongoingCourses = ongoingCoursesrb.IsChecked ?? false;

            RadioButton rb = sender as RadioButton;
            switch(rb.Name)
            {
                case "allCoursesrb":
                    CoursesView.Filter = null;
                    break;
                case "completedCoursesrb":
                    CoursesView.Filter = finishedCoursesPredicate;
                    break;
                case "ongoingCoursesrb":
                    CoursesView.Filter = ongoingCoursesSearchCondition;
                    break;
            }
        }
    }
}
