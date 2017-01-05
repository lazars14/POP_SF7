using POP_SF7.Helpers;
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
    /// Interaction logic for PaymentMenu.xaml
    /// </summary>
    public partial class PaymentMenu : Window
    {
        public ICollectionView view { get; set; }

        public Course SearchCourse { get; set; }
        public Student SearchStudent { get; set; }

        public PaymentMenu()
        {
            InitializeComponent();
            setupWindow();
        }

        private void setupWindow()
        {
            view = CollectionViewSource.GetDefaultView(ApplicationA.Instance.Payments);

            paymentsdg.ItemsSource = view;
            paymentsdg.IsSynchronizedWithCurrentItem = true;
        }

        private void addbtn_Click(object sender, RoutedEventArgs e)
        {
            Payment newPayment = new Payment();
            PaymentAddEdit addPayment = new PaymentAddEdit(newPayment, Decider.ADD);
            addPayment.Show();
        }

        private void editbtn_Click(object sender, RoutedEventArgs e)
        {
            Payment selectedPayment = view.CurrentItem as Payment;
            if(selectedPayment == null)
            {
                MessageBox.Show("Morate da selektujete jednu uplatu da biste je izmenili!");
            }
            else
            {
                Payment backup = (Payment)selectedPayment.Clone();
                PaymentAddEdit edit = new PaymentAddEdit(selectedPayment, Decider.EDIT);
                if(edit.ShowDialog() != true)
                {
                    int index = ApplicationA.Instance.Payments.IndexOf(selectedPayment);
                    ApplicationA.Instance.Payments[index] = backup;
                }
            }
        }

        private void deletebtn_Click(object sender, RoutedEventArgs e)
        {
            Payment selectedPayment = view.CurrentItem as Payment;
            if (selectedPayment == null)
            {
                MessageBox.Show("Morate da selektujete jednu uplatu da biste je izmenili!");
            }
            else if (selectedPayment.Deleted == true)
            {
                MessageBox.Show("Selektovana uplata je vec obrisana!");
            }
            else
            {
                var result = MessageBox.Show("Da li ste sigurni da hocete da obrisete ovu uplatu?", "Upozorenje", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    selectedPayment = view.CurrentItem as Payment;
                    Payment.Delete(selectedPayment);
                    selectedPayment.Deleted = true;
                }
            }
        }

        private void sortbtn_Click(object sender, RoutedEventArgs e)
        {
            // problem oko bool? (moze da bude i null) i bool
            bool ascending = sortAscrb.IsChecked ?? false;
            ListSortDirection direction = (ascending == true) ? ListSortDirection.Ascending : ListSortDirection.Descending;

            view.SortDescriptions.Clear();
            if (idrb.IsChecked ?? false)
            {
                view.SortDescriptions.Add(new SortDescription(("Id"), direction));
            }
            else if (amountrb.IsChecked ?? false)
            {
                view.SortDescriptions.Add(new SortDescription(("Amount"), direction));
            }
            else if (daterb.IsChecked ?? false)
            {
                view.SortDescriptions.Add(new SortDescription(("Date"), direction));
            }
            view.Refresh();
        }

        private bool courseSearchCondition(object s)
        {
            Payment c = s as Payment;
            return c.Course.Id == SearchCourse.Id;
        }

        private bool studentSearchCondition(object s)
        {
            Payment c = s as Payment;
            return c.Student.Id == SearchStudent.Id;
        }

        private void searchbtn_Click(object sender, RoutedEventArgs e)
        {
            bool course = coursechb.IsChecked ?? false;
            bool student = studentchb.IsChecked ?? false;

            Predicate<object> coursePredicate = new Predicate<object>(courseSearchCondition);
            Predicate<object> studentPredicate = new Predicate<object>(studentSearchCondition);

            if (course && student)
            {
                view.Filter = coursePredicate + studentPredicate;
            }
            else if (course)
            {
                view.Filter = coursePredicate;
            }
            else if (student)
            {
                view.Filter = studentPredicate;
            }
            else
            {
                MessageBox.Show("Morate da otkacite jedan ili oba kriterijuma da biste pretrazili kurseve!");
            }
        }

        private void cancelSearchbtn_Click(object sender, RoutedEventArgs e)
        {
            view.Filter = null;
        }

        private void paymentsdg_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            LoadColumnsHelper.LoadPayment(e);
        }

        private void coursebtn_Click(object sender, RoutedEventArgs e)
        {
            SelectFromList window = new SelectFromList(SelectFromMenuOrAddDecider.MENU, CourseStudentDecider.COURSE, this);
            window.Show();
        }

        private void studentbtn_Click(object sender, RoutedEventArgs e)
        {
            SelectFromList window = new SelectFromList(SelectFromMenuOrAddDecider.MENU, CourseStudentDecider.STUDENT, this);
            window.Show();
        }

        private void paymentsdg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Payment selectedPayment = view.CurrentItem as Payment;
            try
            {
                if (selectedPayment.Student.FirstName == null)
                {
                    selectedPayment.Student = ApplicationA.Instance.Students[selectedPayment.Student.Id - 1];

                    selectedPayment.Course = ApplicationA.Instance.Courses[selectedPayment.Course.Id - 1];
                    selectedPayment.Course.Language = ApplicationA.Instance.Languages[selectedPayment.Course.Language.Id - 1];
                    selectedPayment.Course.CourseType = ApplicationA.Instance.CourseTypes[selectedPayment.Course.CourseType.Id - 1];
                }
            }
            catch(NullReferenceException a) { Console.WriteLine(a.StackTrace); }
        }
    }
}
