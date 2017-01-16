using POP_SF7.Windows;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Controls;
using POP_SF7.Helpers;
using POP_SF7.School;
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
        public ICollectionView CoursesView { get; set; }
        public ICollectionView LanguagesView { get; set; }

        public List<int> DeletedIndexes { get; set; }
        public static SolidColorBrush Red = new SolidColorBrush(Colors.Red);

        public Teacher TeacherT { get; set; }
        public Decider Decider { get; set; }

        public string labelAddTeacher = "Dodavanje novog nastavnika";
        public string labelEditTeacher = "Izmena postojeceg nastavnika";

        public string WarningMessage = "Ukoliko izbrisete ovaj jezik svi kursevi sa ovim jezikom (za datog nastavnika) nece moci da se menjaju!";

        public TeacherAddEdit(Teacher teacher, Decider decider, List<int> deletedIndexes)
        {
            TeacherT = teacher;
            Decider = decider;
            DeletedIndexes = deletedIndexes;

            InitializeComponent();

            setupWindow();
        }

        private void languagesdg_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            int rowIndex = e.Row.GetIndex();
            if(Decider == Decider.EDIT)
            {
                if(DeletedIndexes.Contains(rowIndex))
                {
                    e.Row.Background = Red;
                }
            }
        }

        private void setupWindow()
        {
            DataContext = TeacherT;
            personInfo.descriptionlbl.Text = (Decider == Decider.ADD) ? labelAddTeacher : labelEditTeacher;

            CoursesView = CollectionViewSource.GetDefaultView(TeacherT.ListOfCourses);
            coursesdg.ItemsSource = CoursesView;
            coursesdg.IsSynchronizedWithCurrentItem = true;

            LanguagesView = CollectionViewSource.GetDefaultView(TeacherT.ListOfLanguages);
            languagesdg.ItemsSource = LanguagesView;
            languagesdg.IsSynchronizedWithCurrentItem = true;
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
                    if (TeacherDAO.Add(TeacherT))
                    {
                        TeacherT.Id = ApplicationA.Instance.Teachers.Count() + 1;
                        ApplicationA.Instance.Teachers.Add(TeacherT);
                    }
                }
                else
                {
                    if (!TeacherDAO.Edit(TeacherT))
                    {
                        cancelbtn_Click(null, null);
                    }
                }
                Close();
            }
        }

        private void languagesdg_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            DataGrid dg = sender as DataGrid;
            if(dg.Name.Equals("languagesdg"))
            {
                LoadColumnsHelper.LoadLanguage(e);
            }
            else
            {
                LoadColumnsHelper.LoadCourse(e);
            }
        }

        private bool Deleted(int rowIndex)
        {
            bool valid = false;

            foreach(DataGridRow row in LanguagesView)
            {
                int index = row.GetIndex();
                if(DeletedIndexes.Contains(index))
                {
                    valid = true;
                }
            }

            return valid;
        }

        private void changeColor(int rowForDeletion, SolidColorBrush brush)
        {
            foreach (DataGridRow row in LanguagesView)
            {
                if (row.GetIndex() == rowForDeletion)
                {
                    row.Background = brush;
                }
            }
        }

        private void cancelbtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void addLanguagebtn_Click(object sender, RoutedEventArgs e)
        {
            SelectCourseLangStud scl = new SelectCourseLangStud(this);
            scl.Show();
        }

        private void undeleteLanguagebtn_Click(object sender, RoutedEventArgs e)
        {
            Language selectedLanguage = LanguagesView.CurrentItem as Language;
            if (selectedLanguage == null)
            {
                MessageBox.Show("Morate da selektujete jezik da biste ga povratili!");
            }
            else
            {
                if (!Deleted(LanguagesView.CurrentPosition)) // ovde ide provera na osnovu boje
                {
                    MessageBox.Show("Selektovani jezik nije obrisan!");
                }
                else
                {
                    var result = MessageBox.Show("Da li ste sigurni da hocete da povratite dati jezik za ovog nastavnika?", "Upozorenje", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    if (result == MessageBoxResult.Yes)
                    {
                        foreach (TeacherTeachesLanguage ttl in ApplicationA.Instance.TeacherTeachesLanguageCollection)
                        {
                            if (ttl.TeacherId == TeacherT.Id && ttl.LanguageId == selectedLanguage.Id)
                            {
                                if (TeacherTeachesLanguageDAO.UnDelete(ttl))
                                {
                                    ttl.Deleted = false;
                                    changeColor(LanguagesView.CurrentPosition, null);
                                    // boja - default
                                }
                            }
                        }
                    }
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
                if(Deleted(LanguagesView.CurrentPosition)) // ovde ide provera na osnovu boje
                {
                    MessageBox.Show("Selektovani jezik je vec obrisan!");
                }
                else
                {
                    var result = MessageBox.Show("Da li ste sigurni da hocete da obrisete dati jezik za ovog nastavnika?\n" + WarningMessage, "Upozorenje", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    if (result == MessageBoxResult.Yes)
                    {
                        foreach (TeacherTeachesLanguage ttl in ApplicationA.Instance.TeacherTeachesLanguageCollection)
                        {
                            if(ttl.TeacherId == TeacherT.Id && ttl.LanguageId == selectedLanguage.Id)
                            {
                                if(TeacherTeachesLanguageDAO.Delete(ttl))
                                {
                                    ttl.Deleted = true;
                                    changeColor(LanguagesView.CurrentPosition, Red);
                                    // boja - crvena
                                }
                            }
                        }
                    }
                }
            }
        }

        
    }
}
