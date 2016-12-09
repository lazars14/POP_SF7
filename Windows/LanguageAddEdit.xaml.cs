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
        public ObservableCollection<Language> ListOfLanguages { get; set; }

        public LanguageAddEdit(Language language, Decider decider, ObservableCollection<Language> listOfLanguages)
        {
            InitializeComponent();
            DataContext = LanguageL;

            LanguageL = language;
            Decider = decider;
            ListOfLanguages = listOfLanguages;

            descriptionlbl.Text = (language == null) ? labelAddLanguage : labelEditLanguage;
        }

        private void okbtn_Click(object sender, RoutedEventArgs e)
        {
            if(Decider == Decider.ADD)
            {
                // dodavanje u bazu
                ListOfLanguages.Add(LanguageL);   
            }
            else
            {
                // izmena u bazi
            }
            Close();
        }
    }
}
