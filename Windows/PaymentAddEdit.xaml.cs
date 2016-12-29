using POP_SF7.Windows;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace POP_SF7
{
    /// <summary>
    /// Interaction logic for PaymentAddEdit.xaml
    /// </summary>
    public partial class PaymentAddEdit : Window
    {
        public Payment SelectedPayment { get; set; }
        public Decider Decider { get; set; }

        public Course Course { get; set; }
        public Student Student { get; set; }

        public string labelAddPayment = "Dodavanje nove uplate";
        public string labelEditPayment = "Izmena postojece uplate";

        public PaymentAddEdit(Payment payment, Decider decider)
        {
            InitializeComponent();
            SelectedPayment = payment;
            Decider = decider;

            DataContext = SelectedPayment;
            descriptionlbl.Text = (payment == null) ? labelAddPayment : labelEditPayment;
            setupCourseAndStudent();
        }

        private void setupCourseAndStudent()
        {
            if(Decider == Decider.EDIT)
            {
                Course = ApplicationA.Instance.Courses[SelectedPayment.Course.Id - 1];
                Student = ApplicationA.Instance.Students[SelectedPayment.Student.Id - 1];

                coursetb.Text = Course.StartDate.ToShortDateString() + "-" + Course.EndDate.ToShortDateString() + ", " + Course.Price.ToString();
                studenttb.Text = Student.FirstName + " " + Student.LastName;
            }
            else
            {
                datepck.SelectedDate = DateTime.Today;
            }
        }

        private void okbtn_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(coursetb.Text) || string.IsNullOrEmpty(studenttb.Text) || string.IsNullOrEmpty(amounttb.Text))
            {
                MessageBox.Show("Morate da popunite sva polja!");
            }
            else
            {
                SelectedPayment.Course = Course;
                SelectedPayment.Student = Student;

                if (Decider == Decider.ADD)
                {
                    Payment.Add(SelectedPayment);
                    SelectedPayment.Id = ApplicationA.Instance.Payments.Count() + 1;
                    ApplicationA.Instance.Payments.Add(SelectedPayment);
                }
                else
                {
                    Payment.Edit(SelectedPayment);
                }
                Close();
            }
        }

        private void coursebtn_Click(object sender, RoutedEventArgs e)
        {
            SelectFromList window = new SelectFromList(SelectFromMenuOrAddDecider.ADD, CourseStudentDecider.COURSE, this);
            window.Show();
        }

        private void studentbtn_Click(object sender, RoutedEventArgs e)
        {
            SelectFromList window = new SelectFromList(SelectFromMenuOrAddDecider.ADD, CourseStudentDecider.STUDENT, this);
            window.Show();
        }
    }
}
