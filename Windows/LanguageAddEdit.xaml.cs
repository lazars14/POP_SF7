using POP_SF7.DB;
using System.Linq;
using System.Windows;

namespace POP_SF7.Windows
{
    /// <summary>
    /// Interaction logic for LanguageAddEdit.xaml
    /// </summary>
    /// 

    public enum Decider { ADD, EDIT }

    public partial class LanguageAddEdit : Window
    {
        public string labelAddLanguage = "Dodavanje novog jezika";
        public string labelEditLanguage = "Izmena postojeceg jezika";

        public Language LanguageL { get; set; }
        public Decider Decider { get; set; }

        public LanguageAddEdit(Language language, Decider decider)
        {
            InitializeComponent();
            LanguageL = language;

            DataContext = LanguageL;
            Decider = decider;

            userControl.descriptionlbl.Text = (decider == Decider.ADD) ? labelAddLanguage : labelEditLanguage;
        }

        private void okbtn_Click(object sender, RoutedEventArgs e)
        {
            if(userControl.nametb.Text.Equals(""))
            {
                MessageBox.Show(ApplicationA.FILL_ALL_FIELDS_WARNING);
            }
            else
            {
                if (Decider == Decider.ADD)
                {
                    if (LanguageDAO.Add(LanguageL))
                    {
                        LanguageL.Id = ApplicationA.Instance.Languages.Count() + 1;
                        ApplicationA.Instance.Languages.Add(LanguageL);
                    }
                }
                else
                {
                    if (LanguageDAO.Edit(LanguageL))
                    {
                        DialogResult = true;
                    }
                    else
                    {
                        cancelbtn_Click(null, null);
                    }
                }
                Close();
            }
            
        }

        private void cancelbtn_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
