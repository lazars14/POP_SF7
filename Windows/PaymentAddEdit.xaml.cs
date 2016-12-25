using POP_SF7.Windows;
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
        }

        private void okbtn_Click(object sender, RoutedEventArgs e)
        {
            if(Decider == Decider.ADD)
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
