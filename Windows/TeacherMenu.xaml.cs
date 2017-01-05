using POP_SF7.Helpers;
using POP_SF7.School;
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

    public enum PeopleDecider { User, Teacher, Student}

    public partial class TeacherMenu : Window
    {
        public ICollectionView TeachersView { get; set; }
        public ICollectionView CoursesView { get; set; }
        public ICollectionView LanguagesView { get; set; }

        public TeacherMenu()
        {
            InitializeComponent();
            TeachersView = CollectionViewSource.GetDefaultView(ApplicationA.Instance.Teachers);

            teachersdg.ItemsSource = TeachersView;
            teachersdg.IsSynchronizedWithCurrentItem = true;
        }

        private void addbtn_Click(object sender, RoutedEventArgs e)
        {
            Teacher newTeacher = new Teacher();
            TeacherAddEdit addUser = new TeacherAddEdit(newTeacher, Decider.ADD);
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
                TeacherAddEdit edit = new TeacherAddEdit(selectedTeacher, Decider.EDIT);
                if(edit.ShowDialog() != true)
                {
                    int index = ApplicationA.Instance.Teachers.IndexOf(selectedTeacher);
                    ApplicationA.Instance.Teachers[index] = backup;
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
            else if (selectedTeacher.Deleted == true)
            {
                MessageBox.Show("Selektovani nastavnik je vec obrisan!");
            }
            else
            {
                var result = MessageBox.Show("Da li ste sigurni da hocete da obrisete ovog nastavnika?", "Upozorenje", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    selectedTeacher = TeachersView.CurrentItem as Teacher;
                    Teacher.Delete(selectedTeacher);
                    selectedTeacher.Deleted = true;
                }
            }
        }

        private void sortbtn_Click(object sender, RoutedEventArgs e)
        {
            // problem oko bool? (moze da bude i null) i bool
            bool ascending = sortAscrb.IsChecked ?? false;
            ListSortDirection direction = (ascending == true) ? ListSortDirection.Ascending : ListSortDirection.Descending;

            TeachersView.SortDescriptions.Clear();
            if (idrb.IsChecked ?? false)
            {
                TeachersView.SortDescriptions.Add(new SortDescription("Id", direction));
            }
            else if (lastnamerb.IsChecked ?? false)
            {
                TeachersView.SortDescriptions.Add(new SortDescription("LastName", direction));
            }
            else if (firstnamerb.IsChecked ?? false)
            {
                TeachersView.SortDescriptions.Add(new SortDescription("FirstName", direction));
            }
            TeachersView.Refresh();
        }

        private void searchbtn_Click(object sender, RoutedEventArgs e)
        {
            bool firstName = firstnamechb.IsChecked ?? false;
            bool lastName = lastnamechb.IsChecked ?? false;
            bool jmbg = jmbgchb.IsChecked ?? false;

            Search s = new Search(firstnametb.Text, lastnametb.Text, jmbgtb.Text, PeopleDecider.Teacher);
            Predicate<object> firstNamePredicate = new Predicate<object>(s.firstname);
            Predicate<object> lastNamePredicate = new Predicate<object>(s.lastname);
            Predicate<object> jmbgPredicate = new Predicate<object>(s.jmbg);

            if (firstName && lastName && jmbg)
            {
                TeachersView.Filter = firstNamePredicate + lastNamePredicate + jmbgPredicate;
            }
            else if (firstName && lastName)
            {
                TeachersView.Filter = firstNamePredicate + lastNamePredicate;
            }
            else if (firstName && jmbg)
            {
                TeachersView.Filter = firstNamePredicate + jmbgPredicate;
            }
            else if (lastName && jmbg)
            {
                TeachersView.Filter = lastNamePredicate + jmbgPredicate;
            }
            else if (firstName)
            {
                TeachersView.Filter = firstNamePredicate;
            }
            else if (lastName)
            {
                TeachersView.Filter = lastNamePredicate;
            }
            else if (jmbg)
            {
                TeachersView.Filter = jmbgPredicate;
            }
            else
            {
                MessageBox.Show("Morate da otkacite makar jedan kriterijum kako biste pretrazili ucenike!");
            }
        }

        private void usersdg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Teacher selectedTeacher = TeachersView.CurrentItem as Teacher;
          
            try
            {
                int selectedTeacherId = selectedTeacher.Id;
                Teacher selectedTeacherTwo = ApplicationA.Instance.Teachers[selectedTeacherId - 1];
                if (selectedTeacherTwo.ListOfCourses.Count == 0)
                {
                    
                    foreach (TeacherTeachesCourse ttc in ApplicationA.Instance.TeacherTeachesCourseCollection)
                    {
                        if (ttc.TeacherId == selectedTeacherId)
                        {
                            Course course = ApplicationA.Instance.Courses[ttc.CourseId - 1];
                            selectedTeacherTwo.ListOfCourses.Add(course);
                        }
                    }

                    foreach (TeacherTeachesLanguage ttl in ApplicationA.Instance.TeacherTeachesLanguageCollection)
                    {
                        if (ttl.TeacherId == selectedTeacherId)
                        {
                            Language lang = ApplicationA.Instance.Languages[ttl.LanguageId - 1];
                            selectedTeacherTwo.ListOfLanguages.Add(lang);
                        }
                    }


                }
                else
                {
                    LanguagesView = CollectionViewSource.GetDefaultView(selectedTeacherTwo.ListOfLanguages);
                    languagesdg.ItemsSource = LanguagesView;
                    languagesdg.IsSynchronizedWithCurrentItem = true;

                    CoursesView = CollectionViewSource.GetDefaultView(selectedTeacherTwo.ListOfCourses);
                    coursesdg.ItemsSource = CoursesView;
                    coursesdg.IsSynchronizedWithCurrentItem = true;
                }

            }
            catch (NullReferenceException a) { Console.WriteLine(a.StackTrace); }
        }

        private void cancelSearchbtn_Click(object sender, RoutedEventArgs e)
        {
            TeachersView.Filter = null;
        }

        private void teachersdg_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            LoadColumnsHelper.LoadTeacher(e);
        }

        private void languagesdg_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            LoadColumnsHelper.LoadLanguage(e);
        }

        private void coursesdg_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            LoadColumnsHelper.LoadCourse(e);
        }

        private void coursesdg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Course selectedCourse = CoursesView.CurrentItem as Course;
                if (selectedCourse.Language.Name == null)
                {
                    selectedCourse.Language = ApplicationA.Instance.Languages[selectedCourse.Language.Id - 1];
                    selectedCourse.CourseType = ApplicationA.Instance.CourseTypes[selectedCourse.CourseType.Id - 1];
                }
            }catch(NullReferenceException a) { Console.WriteLine(a.StackTrace); }
        }
    }
}
