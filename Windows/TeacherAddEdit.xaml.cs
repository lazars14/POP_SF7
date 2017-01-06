using POP_SF7.Windows;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Controls;
using POP_SF7.Helpers;

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
    }
}
