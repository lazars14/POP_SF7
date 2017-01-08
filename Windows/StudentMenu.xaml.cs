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

    public partial class StudentMenu : Window
    {
        public ICollectionView StudentsView { get; set; }
        public ICollectionView CoursesView { get; set; }
        public ICollectionView PaymentsView { get; set; }

        public Student SelectedStudent { get; set; }

        public StudentMenu()
        {
            InitializeComponent();

            setupViews();
        }

        private void setupViews()
        {
            // student view load
            StudentsView = CollectionViewSource.GetDefaultView(ApplicationA.Instance.Students);

            studentsdg.ItemsSource = StudentsView;
            studentsdg.IsSynchronizedWithCurrentItem = true;

            // payment view load
            PaymentsView = CollectionViewSource.GetDefaultView(ApplicationA.Instance.Payments);
            Predicate<object> studentIdPredicate = new Predicate<object>(selectedStudentIdSearchCondition);
            PaymentsView.Filter = studentIdPredicate;

            paymentsdg.ItemsSource = PaymentsView;
            paymentsdg.IsSynchronizedWithCurrentItem = true;
        }

        private bool selectedStudentIdSearchCondition(object s)
        {
            Payment c = s as Payment;
            Student ss = StudentsView.CurrentItem as Student;
            return c.Student.Id == ss.Id;
        }

        private bool showNoneSearchCondition(object s)
        {
            Payment c = s as Payment;
            return c.Student.Id == 123456789;
        }

        private void addbtn_Click(object sender, RoutedEventArgs e)
        {
            Student newStudent = new Student();
            StudentAddEdit addUser = new StudentAddEdit(newStudent, Decider.ADD);
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
                StudentAddEdit edit = new StudentAddEdit(selectedStudent, Decider.EDIT);
                if (edit.ShowDialog() != true)
                {
                    int index = ApplicationA.Instance.Students.IndexOf(selectedStudent);
                    ApplicationA.Instance.Students[index] = backup;
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
                    if(Student.Delete(selectedStudent))
                    {
                        selectedStudent.Deleted = true;
                    }
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

            SearchHelper s = new SearchHelper(firstnametb.Text, lastnametb.Text, jmbgtb.Text, PeopleDecider.Student);
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

        private void setCoursesPaymentsEmpty()
        {
            CoursesView = CollectionViewSource.GetDefaultView(new ObservableCollection<Course>());
            coursesdg.ItemsSource = CoursesView;
            coursesdg.IsSynchronizedWithCurrentItem = true;

            PaymentsView.Filter = new Predicate<object>(showNoneSearchCondition);
        }

        private void usersdg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Student selectedStudent = StudentsView.CurrentItem as Student;
            if(selectedStudent != null)
            {
                int selectedStudentId = selectedStudent.Id;

                try
                {
                    Predicate<object> studentIdPredicate = new Predicate<object>(selectedStudentIdSearchCondition);
                    PaymentsView.Filter = studentIdPredicate;
                }
                catch(NullReferenceException a)
                {
                    Console.WriteLine(a.StackTrace);
                }

                Student selectedStudentTwo = ApplicationA.Instance.Students[selectedStudentId - 1];
                if (selectedStudentTwo.ListOfCourses.Count == 0) // ovde ako student ne slusa nijedan kurs proverice svaki put...
                {
                    foreach (StudentAttendsCourse sac in ApplicationA.Instance.StudentAttendsCourseCollection)
                    {
                        if (sac.StudentId == selectedStudentId)
                        {
                            Course course = ApplicationA.Instance.Courses[sac.StudentId - 1];
                            selectedStudentTwo.ListOfCourses.Add(course);
                        }
                    }
                }
                CoursesView = CollectionViewSource.GetDefaultView(selectedStudentTwo.ListOfCourses);
                coursesdg.ItemsSource = CoursesView;
                coursesdg.IsSynchronizedWithCurrentItem = true;
            }
            else
            {
                CoursesView = CollectionViewSource.GetDefaultView(new ObservableCollection<Course>());
                coursesdg.ItemsSource = CoursesView;
                coursesdg.IsSynchronizedWithCurrentItem = true;
            }

            // verzija gde uzima prvog iz tabele ako nijedan nije selektovan

            /*Student selectedStudent = StudentsView.CurrentItem as Student;
            if(selectedStudent == null)
            {
                StudentsView.MoveCurrentToFirst();
                selectedStudent = StudentsView.CurrentItem as Student;
            }
            else
            {
                selectedStudentId = selectedStudent.Id;
            }

            try
            {
                PaymentsView.Refresh();
            }
            catch(NullReferenceException a)
            {
                Console.WriteLine(a.StackTrace);
            }

            Student selectedStudentTwo = ApplicationA.Instance.Students[selectedStudentId - 1];
            if(selectedStudentTwo.ListOfCourses.Count == 0)
            {
                foreach (StudentAttendsCourse sac in ApplicationA.Instance.StudentAttendsCourseCollection)
                {
                    if (sac.StudentId == selectedStudentId)
                    {
                        Course course = ApplicationA.Instance.Courses[sac.StudentId - 1];
                        selectedStudentTwo.ListOfCourses.Add(course);
                    }
                }
            }
            CoursesView = CollectionViewSource.GetDefaultView(selectedStudentTwo.ListOfCourses);
            coursesdg.ItemsSource = CoursesView;
            coursesdg.IsSynchronizedWithCurrentItem = true;*/
        }

        private void cancelSearchbtn_Click(object sender, RoutedEventArgs e)
        {
            StudentsView.Filter = null;
        }

        private void studentsdg_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            LoadColumnsHelper.LoadStudent(e);
        }

        private void coursesdg_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            LoadColumnsHelper.LoadCourse(e);
        }

        private void paymentsdg_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            LoadColumnsHelper.LoadPayment(e);
        }

        private void paymentsdg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Payment selectedPayment = PaymentsView.CurrentItem as Payment;
            if(selectedPayment != null)
            {
                if(selectedPayment.Course.Language == null)
                {
                    selectedPayment.Course = ApplicationA.Instance.Courses[selectedPayment.Course.Id - 1];
                    selectedPayment.Course.Language = ApplicationA.Instance.Languages[selectedPayment.Course.Language.Id - 1];
                    selectedPayment.Course.CourseType = ApplicationA.Instance.CourseTypes[selectedPayment.Course.CourseType.Id - 1];
                }
            }
        }

        private void coursesdg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Course selectedCourse = CoursesView.CurrentItem as Course;
            if(selectedCourse != null)
            {
                if(selectedCourse.Language == null)
                {
                    selectedCourse.Language = ApplicationA.Instance.Languages[selectedCourse.Language.Id - 1];
                    selectedCourse.CourseType = ApplicationA.Instance.CourseTypes[selectedCourse.CourseType.Id - 1];
                }
            }
            
        }

        private void closeFilters(object sender, EventArgs e)
        {
            try
            {
                CoursesView.Filter = null;
                PaymentsView.Filter = null;
            }
            catch (NullReferenceException a) { Console.WriteLine(a.StackTrace); }
        }
    }
}
