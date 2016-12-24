using POP_SF7.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for PaymentAddEdit.xaml
    /// </summary>
    public partial class PaymentAddEdit : Window
    {
        public Payment SelectedPayment { get; set; }
        public Decider Decider { get; set; }
        public ObservableCollection<Payment> ListOfPayments { get; set; }

        public Course Course { get; set; }
        public Student Student { get; set; }

        public string labelAddPayment = "Dodavanje nove uplate";
        public string labelEditPayment = "Izmena postojece uplate";

        public PaymentAddEdit(Payment payment, Decider decider, ObservableCollection<Payment> listOfPayments)
        {
            InitializeComponent();
            SelectedPayment = payment;
            Decider = decider;
            ListOfPayments = listOfPayments;

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
    }
}
