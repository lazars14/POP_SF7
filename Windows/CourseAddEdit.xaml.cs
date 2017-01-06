using POP_SF7.Helpers;
using POP_SF7.School;
using POP_SF7.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace POP_SF7
{
    /// <summary>
    /// Interaction logic for CourseAddEdit.xaml
    /// </summary>
    public partial class CourseAddEdit : Window
    {
        public Course Course { get; set; }
        public Decider Decider { get; set; }

        public ObservableCollection<Student> ListOfStudents { get; set; }
        public ICollectionView StudentsView { get; set; }

        public string labelCourseAdd = "Dodavanje novog kursa";
        public string labelCourseEdit = "Izmena postojeceg kursa";

        public CourseAddEdit(Course course, Decider decider)
        {
            InitializeComponent();
            Course = course;
            Decider = decider;

            setupWindow();
        }

        private void setupWindow()
        {
            DataContext = Course;

            descriptionlbl.Text = (Decider == Decider.ADD) ? labelCourseAdd : labelCourseEdit;

            ListOfStudents = new ObservableCollection<Student>();

            languagecb.ItemsSource = ApplicationA.Instance.Languages;
            languagecb.DisplayMemberPath = "Name";
            languagecb.SelectedValuePath = "Id";
            languagecb.SelectedIndex = 0;

            courseTypecb.ItemsSource = ApplicationA.Instance.CourseTypes;
            courseTypecb.DisplayMemberPath = "Name";
            courseTypecb.SelectedValuePath = "Id";
            courseTypecb.SelectedIndex = 0;

            teachercb.ItemsSource = ApplicationA.Instance.Teachers;
            teachercb.DisplayMemberPath = "FullName";
            teachercb.SelectedValuePath = "Id";
            teachercb.SelectedIndex = 0;

            StudentsView = CollectionViewSource.GetDefaultView(ListOfStudents);
            studentsdg.ItemsSource = StudentsView;
            studentsdg.IsSynchronizedWithCurrentItem = true;

            if(Decider == Decider.ADD)
            {
                teachercb.IsEnabled = false;
            }
            else
            {
                languagecb.SelectedIndex = Course.Language.Id - 1;
                courseTypecb.SelectedIndex = Course.CourseType.Id - 1;
                teachercb.SelectedIndex = Course.Teacher.Id - 1;
            }
        }

        private void okbtn_Click(object sender, RoutedEventArgs e)
        {
            if(pricetb.Text.Equals("") || languagecb.SelectedItem == null || courseTypecb.SelectedItem == null || teachercb.SelectedItem == null)
            {
                MessageBox.Show(ApplicationA.FILL_ALL_FIELDS_WARNING);
            }
            else
            {
                if (Decider == Decider.ADD)
                {
                    if (Course.Add(Course))
                    {
                        Course.Id = ApplicationA.Instance.Courses.Count() + 1;
                        ApplicationA.Instance.Courses.Add(Course);
                    }
                }
                else
                {
                    if (!Course.Edit(Course))
                    {
                        cancelbtn_Click(null, null);
                    }
                }
                Close();
            }
        }

        private void studentsdg_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            LoadColumnsHelper.LoadStudent(e);
        }

        private void languagecb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<Teacher> filteredTeachers = new List<Teacher>();
            int languageId = 0;
            try
            {
                languageId = (int)languagecb.SelectedValue;
            }
            catch(NullReferenceException a) { Console.WriteLine(a.StackTrace); }
            foreach(TeacherTeachesLanguage ttl in ApplicationA.Instance.TeacherTeachesLanguageCollection)
            {
                if(ttl.LanguageId == languageId)
                {
                    Teacher teach = ApplicationA.Instance.Teachers[ttl.TeacherId - 1];
                    filteredTeachers.Add(teach);
                }
            }
            if(filteredTeachers.Count == 0)
            {
                MessageBox.Show("Ne postoji nastavnik koji predaje odabrani jezik!");
                teachercb.IsEnabled = false;
            }
            else
            {
                teachercb.ItemsSource = filteredTeachers;
                teachercb.IsEnabled = true;
            }
        }

        private void cancelbtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
