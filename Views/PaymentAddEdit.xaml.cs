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
    /// Interaction logic for PaymentAddEdit.xaml
    /// </summary>
    public partial class PaymentAddEdit : Window
    {
        public Payment SelectedPayment { get; set; }

        public string labelAddPayment = "Dodavanje nove uplate";
        public string labelEditPayment = "Izmena postojece uplate";

        public PaymentAddEdit(Payment payment)
        {
            InitializeComponent();
            SelectedPayment = payment;
            descriptionlbl.Text = (payment == null) ? labelAddPayment : labelEditPayment;
        }
    }
}
