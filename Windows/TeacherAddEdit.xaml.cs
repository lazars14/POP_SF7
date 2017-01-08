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

namespace POP_SF7
{
    /// <summary>
    /// Interaction logic for PersonAddEdit.xaml
    /// </summary>
    public partial class TeacherAddEdit : Window
    {
        public ICollectionView CoursesView { get; set; }
        public ICollectionView LanguagesView { get; set; }

        public Teacher TeacherT { get; set; }
        public Decider Decider { get; set; }

        public string labelAddTeacher = "Dodavanje novog nastavnika";
        public string labelEditTeacher = "Izmena postojeceg nastavnika";

        public string WarningMessage = "Ukoliko izbrisete ovaj jezik svi kursevi sa ovim jezikom (za datog nastavnika) nece moci da se menjaju!";

        public TeacherAddEdit(Teacher teacher, Decider decider)
        {
            InitializeComponent();

            TeacherT = teacher;
            Decider = decider;

            setupWindow();
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
                    if (Teacher.Add(TeacherT))
                    {
                        TeacherT.Id = ApplicationA.Instance.Teachers.Count() + 1;
                        ApplicationA.Instance.Teachers.Add(TeacherT);
                    }
                }
                else
                {
                    if (!Teacher.Edit(TeacherT))
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
                if (selectedLanguage.Deleted == false)
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
                                if (TeacherTeachesLanguage.UnDelete(ttl))
                                {
                                    ttl.Deleted = false;
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
                if(selectedLanguage.Deleted == true)
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
                                if(TeacherTeachesLanguage.Delete(ttl))
                                {
                                    ttl.Deleted = true;
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
