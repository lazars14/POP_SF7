using POP_SF7.Windows;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Controls;
using POP_SF7.Helpers;
using System.Collections.Generic;
using System.Windows.Media;
using POP_SF7.DB;

namespace POP_SF7
{
    /// <summary>
    /// Interaction logic for PersonAddEdit.xaml
    /// </summary>
    public partial class TeacherAddEdit : Window
    {
        public ICollectionView LanguagesView { get; set; }
        public ICollectionView DeletedLanguagesView { get; set; }

        public List<Language> AddedLanguages { get; set; }
        public List<Language> EditedLanguages { get; set; }
        public List<Language> DeletedLanguages { get; set; }

        public Teacher TeacherT { get; set; }
        public Decider Decider { get; set; }

        public string labelAddTeacher = "Dodavanje novog nastavnika";
        public string labelEditTeacher = "Izmena postojeceg nastavnika";

        public string WarningMessage = "Ukoliko izbrisete ovaj jezik svi kursevi sa ovim jezikom (za datog nastavnika) nece moci da se menjaju!";

        public TeacherAddEdit(Teacher teacher, Decider decider, List<int> deletedIndexes)
        {
            TeacherT = teacher;
            Decider = decider;

            AddedLanguages = new List<Language>();
            EditedLanguages = new List<Language>();
            DeletedLanguages = new List<Language>();

            InitializeComponent();

            setupWindow();
        }

        private void setupWindow()
        {
            DataContext = TeacherT;
            personInfo.descriptionlbl.Text = (Decider == Decider.ADD) ? labelAddTeacher : labelEditTeacher;

            coursesdg.ItemsSource = TeacherT.ListOfCourses;
            coursesdg.IsSynchronizedWithCurrentItem = true;

            deletedCoursesdg.ItemsSource = TeacherT.ListOfDeletedCourses;
            deletedCoursesdg.IsSynchronizedWithCurrentItem = true;

            LanguagesView = CollectionViewSource.GetDefaultView(TeacherT.ListOfLanguages);
            languagesdg.ItemsSource = LanguagesView;
            languagesdg.IsSynchronizedWithCurrentItem = true;

            DeletedLanguagesView = CollectionViewSource.GetDefaultView(TeacherT.ListOfDeletedLanguages);
            deletedLanguagesdg.ItemsSource = DeletedLanguagesView;
            deletedLanguagesdg.IsSynchronizedWithCurrentItem = true;
        }

        private void okbtn_Click(object sender, RoutedEventArgs e)
        {
            if (personInfo.nametb.Text.Equals("") || personInfo.lastnametb.Text.Equals("") || personInfo.addresstb.Text.Equals("") || personInfo.jmbgtb.Text.Equals(""))
            {
                MessageBox.Show(ApplicationA.FILL_ALL_FIELDS_WARNING);
            }
            else
            {
                if (Decider == Decider.ADD)
                {
                    TeacherT.Id = ApplicationA.Instance.Teachers.Count() + 1;

                    if (TeacherDAO.Add(TeacherT) && saveLanguages())
                    {    
                        ApplicationA.Instance.Teachers.Add(TeacherT);
                    }
                }
                else
                {
                    if (TeacherDAO.Edit(TeacherT) && saveLanguages())
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

        private bool saveLanguages()
        {
            bool valid = true;

            foreach (Language lang in AddedLanguages)
            {
                valid = TeacherTeachesLanguageDAO.Add(TeacherT.Id, lang.Id);
            }

            foreach (Language lang in EditedLanguages)
            {
                valid = TeacherTeachesLanguageDAO.UnDelete(TeacherT.Id, lang.Id);
            }

            foreach (Language lang in DeletedLanguages)
            {
                valid = TeacherTeachesLanguageDAO.Delete(TeacherT.Id, lang.Id);
            }

            return valid;
        }

        private void languagesdg_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            DataGrid dg = sender as DataGrid;
            switch(dg.Name)
            {
                case "languagesdg":
                    LoadColumnsHelper.LoadLanguage(e);
                    break;
                case "deletedLanguagesdg":
                    LoadColumnsHelper.LoadLanguage(e);
                    break;
                case "coursesdg":
                    LoadColumnsHelper.LoadCourse(e);
                    break;
                case "deletedCoursesdg":
                    LoadColumnsHelper.LoadCourse(e);
                    break;
            }
        }

        private void cancelbtn_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void addLanguagebtn_Click(object sender, RoutedEventArgs e)
        {
            SelectCourseLangStud scl = new SelectCourseLangStud(this);
            scl.Show();
        }

        private void undeleteLanguagebtn_Click(object sender, RoutedEventArgs e)
        {
            Language selectedLanguage = DeletedLanguagesView.CurrentItem as Language;
            if (selectedLanguage == null)
            {
                MessageBox.Show("Morate da selektujete jezik da biste ga povratili!");
            }
            else
            {
                var result = MessageBox.Show("Da li ste sigurni da hocete da povratite dati jezik za ovog nastavnika?", "Upozorenje", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    if (DeletedLanguages.Contains(selectedLanguage))
                    {
                        DeletedLanguages.Remove(selectedLanguage);
                    }

                    EditedLanguages.Add(selectedLanguage);

                    TeacherT.ListOfLanguages.Add(selectedLanguage);
                    TeacherT.ListOfDeletedLanguages.Remove(selectedLanguage);
                }
            }
        }

        private void deleteLanguagebtn_Click(object sender, RoutedEventArgs e)
        {
            Language selectedLanguage = LanguagesView.CurrentItem as Language;
            if(selectedLanguage == null)
            {
                MessageBox.Show("Morate da selektujete jezik da biste ga obrisali!");
            }
            else
            {
                var result = MessageBox.Show("Da li ste sigurni da hocete da obrisete dati jezik za ovog nastavnika?\n" + WarningMessage, "Upozorenje", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    checkIfLanguageAddedOrDeleted(selectedLanguage);
                }
            }
        }

        private void checkIfLanguageAddedOrDeleted(Language language)
        {
            if (AddedLanguages.Contains(language))
            {
                AddedLanguages.Remove(language);
            }
            else if (EditedLanguages.Contains(language))
            {
                EditedLanguages.Remove(language);
            }

            DeletedLanguages.Add(language);

            TeacherT.ListOfDeletedLanguages.Add(language);
            TeacherT.ListOfLanguages.Remove(language);
        }
    }
}
