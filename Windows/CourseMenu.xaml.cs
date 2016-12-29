using POP_SF7.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public ICollectionView StudentsView { get; set; }

        public CourseMenu()
        {
            InitializeComponent();
            setupWindow();

            CoursesView = CollectionViewSource.GetDefaultView(ApplicationA.Instance.Courses);

            coursesdg.ItemsSource = CoursesView;
            coursesdg.IsSynchronizedWithCurrentItem = true;
        }

        private void setupWindow()
        {
            POP_SF7.Language.Load();
            CourseType.Load();

            languagecb.ItemsSource = ApplicationA.Instance.Languages;
            languagecb.DisplayMemberPath = "Name";
            languagecb.SelectedValuePath = "Id";
            languagecb.SelectedIndex = 0;

            courseTypecb.ItemsSource = ApplicationA.Instance.CourseTypes;
            courseTypecb.DisplayMemberPath = "Name";
            courseTypecb.SelectedValuePath = "Id";
            courseTypecb.SelectedIndex = 0;
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
                if(edit.ShowDialog() != true)
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
                var result = MessageBox.Show("Da li ste sigurni da hocete da obrisete ovog korisnika?", "Upozorenje", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    selectedCourse = CoursesView.CurrentItem as Course;
                    Course.Delete(selectedCourse);
                    selectedCourse.Deleted = true;
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

            if (language && courseType)
            {
                CoursesView.Filter = languagePredicate + courseTypePredicate;
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
            if (selectedCourse == null)
            {
                CoursesView.MoveCurrentToFirst();
                selectedCourse = CoursesView.CurrentItem as Course;
            }
            selectedCourse.Language = ApplicationA.Instance.Languages[selectedCourse.Language.Id - 1];
            selectedCourse.CourseType = ApplicationA.Instance.CourseTypes[selectedCourse.CourseType.Id - 1];
            selectedCourse.Teacher = ApplicationA.Instance.Teachers[selectedCourse.Teacher.Id - 1];
        }

        private void cancelSearchbtn_Click(object sender, RoutedEventArgs e)
        {
            CoursesView.Filter = null;
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
                    e.Cancel = true;
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

        private void showCoursesbtn_Click(object sender, RoutedEventArgs e)
        {
            Predicate<object> finishedCoursesPredicate = new Predicate<object>(finishedCoursesSearchCondition);
            Predicate<object> ongoingCoursesPredicate = new Predicate<object>(ongoingCoursesSearchCondition);

            bool allCourses = allCoursesrb.IsChecked ?? false;
            bool finishedCourses = completedCoursesrb.IsChecked ?? false;
            bool ongoingCourses = ongoingCoursesrb.IsChecked ?? false;

            if(allCourses) CoursesView.Filter = null;
            else if(finishedCourses) CoursesView.Filter = finishedCoursesPredicate;
            else if(ongoingCourses) CoursesView.Filter = ongoingCoursesSearchCondition;
        }
    }
}
