using POP_SF7.DB;
using POP_SF7.Helpers;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace POP_SF7.Windows
{
    /// <summary>
    /// Interaction logic for LanguageMenu.xaml
    /// </summary>
    public partial class LanguageMenu : Window
    {
        public ICollectionView view { get; set; }
        
        public LanguageMenu()
        {
            InitializeComponent();

            view = CollectionViewSource.GetDefaultView(ApplicationA.Instance.Languages);
            dynamicdg.ItemsSource = view;
            dynamicdg.IsSynchronizedWithCurrentItem = true;
        }

        private void addbtn_Click(object sender, RoutedEventArgs e)
        {
            Language newLanguage = new Language();
            LanguageAddEdit add = new LanguageAddEdit(newLanguage, Decider.ADD);
            add.Show();
        }

        private void editbtn_Click(object sender, RoutedEventArgs e)
        {
            Language selectedLanguage = view.CurrentItem as Language;
            if (selectedLanguage != null)
            {
                Language backup = (Language)selectedLanguage.Clone();
                LanguageAddEdit edit = new LanguageAddEdit(selectedLanguage, Decider.EDIT);
                if (edit.ShowDialog() != true)
                {
                    int index = ApplicationA.Instance.Languages.IndexOf(selectedLanguage);
                    ApplicationA.Instance.Languages[index] = backup;
                }
            }
            else
            {
                MessageBox.Show("Morate da selektujete red u tabeli kako bi izmenili jezik!");
            }
        }

        private void deletebtn_Click(object sender, RoutedEventArgs e)
        {
            Language selectedLanguage = view.CurrentItem as Language;
            if (selectedLanguage == null)
            {
                MessageBox.Show("Morate da selektujete red u tabeli kako bi izmenili jezik!");
            }
            else if (selectedLanguage.Deleted == true)
            {
                MessageBox.Show("Selektovani jezik je vec obrisan!");
            }
            else
            {
                var result = MessageBox.Show("Da li ste sigurni da hocete da obrisete ovaj jezik?", "Upozorenje", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    selectedLanguage = view.CurrentItem as Language;
                    if(LanguageDAO.Delete(selectedLanguage))
                    {
                        selectedLanguage.Deleted = true;
                    }
                }
            }
        }

        private void closebtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void dynamicdg_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            LoadColumnsHelper.LoadLanguage(e);
        }
    }
}
