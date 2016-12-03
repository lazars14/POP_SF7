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
    /// Interaction logic for PaymentMenu.xaml
    /// </summary>
    public partial class PaymentMenu : Window
    {
        public PaymentMenu()
        {
            InitializeComponent();
        }

        private void addbtn_Click(object sender, RoutedEventArgs e)
        {
            PaymentAddEdit addPayment = new PaymentAddEdit(null);
            addPayment.Show();
        }

        private void editbtn_Click(object sender, RoutedEventArgs e)
        {
            // provera da li je selektovana uplata, ako nije message box sa upozorenjem
            Payment selectedPayment = (Payment)paymentsdg.SelectedItem;
            PaymentAddEdit editPayment = new PaymentAddEdit(selectedPayment);
            editPayment.Show();
        }

        private void deletebtn_Click(object sender, RoutedEventArgs e)
        {
            // provera da li je selektovana uplata, ako nije message box sa upozorenjem
            Payment selectedPayment = (Payment)paymentsdg.SelectedItem;
            // message dialog da li ste sigurni da hocete da obrisete ovaj kurs?
            // funkcija za brisanje
        }

        private void sortbtn_Click(object sender, RoutedEventArgs e)
        {
            // problem oko bool? (moze da bude i null) i bool
            bool ascending = sortAscrb.IsChecked ?? false;

            if (idrb.IsChecked ?? false) ; //funkcija za sortiranje
            else if (amountrb.IsChecked ?? false) ; //funkcija za sortiranje
            else if (studentidrb.IsChecked ?? false) ; //funkcija za sortiranje
            else if (daterb.IsChecked ?? false) ; //funkcija za sortiranje
        }

        private void searchbtn_Click(object sender, RoutedEventArgs e)
        {
            bool course = coursechb.IsChecked ?? false;
            bool student = studentchb.IsChecked ?? false;
            if (course && student)
            {
                // pokupi podatke iz oba textboxa
                // funkcija
            }
            else if (course)
            {
                // pokupi podatke iz textboxa za kurs
                // funkcija
            }
            else if (student)
            {
                // pokupi podatke iz textboxa za ucenika
                // funkcija
            }
            else
            {
                // MessageBox koji kaze da mora da se selektuje nesto od ta dva ili oba
            }
        }
    }
}
