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

            nametb.DataContext = LanguageL;
            deletedcb.DataContext = LanguageL;

            descriptionlbl.Text = (decider == Decider.ADD) ? labelAddLanguage : labelEditLanguage;
        }

        private void okbtn_Click(object sender, RoutedEventArgs e)
        {
            if(Decider == Decider.ADD)
            {
                POP_SF7.Language.Add(LanguageL);

                LanguageL.Id = ApplicationA.Instance.Languages.Count() + 1;
                ApplicationA.Instance.Languages.Add(LanguageL);
            }
            else
            {
                POP_SF7.Language.Edit(LanguageL);
            }
            Close();
        }
    }
}
