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
    /// Interaction logic for LanguagesCourseTypesMenu.xaml
    /// </summary>
    /// 

    public enum DeciderLanguageCourseType { Language, CourseType }

    public partial class LanguagesCourseTypesMenu : Window
    {
        DeciderLanguageCourseType Decider { get; set; }
        public string languagesLabel = "Jezici";
        public string courseTypesLabel = "Tipovi kurseva";

        public LanguagesCourseTypesMenu(DeciderLanguageCourseType decider)
        {
            Decider = decider;
            InitializeComponent();
            descriptionlbl.Text = (decider == DeciderLanguageCourseType.Language) ? languagesLabel : courseTypesLabel;
        }

        private void addbtn_Click(object sender, RoutedEventArgs e)
        {
            LanguageCourseTypeAddEdit add = new LanguageCourseTypeAddEdit(Decider);
            add.Show();
        }

        private void editbtn_Click(object sender, RoutedEventArgs e)
        {
            // provera da li je selektovan jezik, ako nije message box
            if(Decider == DeciderLanguageCourseType.Language)
            {
                Language selectedLanguage = (Language) dynamicdg.SelectedItem;
                LanguageCourseTypeAddEdit edit = new LanguageCourseTypeAddEdit(selectedLanguage, Decider);
                edit.Show();
            }
            else
            {
                CourseType selectedCourseType = (CourseType)dynamicdg.SelectedItem;
                LanguageCourseTypeAddEdit edit = new LanguageCourseTypeAddEdit(selectedCourseType, Decider);
                edit.Show();
            }

        }

        private void deletebtn_Click(object sender, RoutedEventArgs e)
        {
            // provera da li je selektovan jezik, ako nije message box
            if (Decider == DeciderLanguageCourseType.Language)
            {
                Language selectedLanguage = (Language)dynamicdg.SelectedItem;
                // message box da li ste sigurni
                // f-ja za brisanje
            }
            else
            {
                CourseType selectedCourseType = (CourseType)dynamicdg.SelectedItem;
                // message box da li ste sigurni
                // f-ja za brisanje
            }
        }

        private void closebtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
