using POP_SF7.DB;
using System.Windows;

namespace POP_SF7
{
    /// <summary>
    /// Interaction logic for SchoolEdit.xaml
    /// </summary>
    public partial class SchoolEdit : Window
    {
        public SchoolS SchoolS { get; set; }

        public SchoolEdit(SchoolS school)
        {
            InitializeComponent();
            SchoolS = school;
            DataContext = SchoolS;
        }

        private void okbtn_Click(object sender, RoutedEventArgs e)
        {
            if(nametb.Text.Equals("") || addresstb.Text.Equals("") || phonetb.Text.Equals("") || nametb.Text.Equals("") || emailtb.Text.Equals("") || websitetb.Text.Equals("") || pibtb.Text.Equals("") || identificationNumbertb.Text.Equals("") || accountNumbertb.Text.Equals(""))
            {
                MessageBox.Show(ApplicationA.FILL_ALL_FIELDS_WARNING);
            }
            else if(!SchoolDAO.UpdateSchool(SchoolS))
            {
                cancelbtn_Click(null, null);
            }
            Close();
        }

        private void cancelbtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
