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
        public ObservableCollection<Course> ListOfCourses { get; set; }
        public Decider Decider { get; set; }

        public List<Language> ListOfLanguages { get; set; }
        public List<CourseType> ListOfCourseTypes { get; set; }
        public List<Teacher> ListOfTeachers { get; set; }
        public ObservableCollection<Student> ListOfStudents { get; set; }
        public ICollectionView StudentsView { get; set; }

        public string labelCourseAdd = "Dodavanje novog kursa";
        public string labelCourseEdit = "Izmena postojeceg kursa";

        public CourseAddEdit(Course course, Decider decider, ObservableCollection<Course> listOfCourses)
        {
            InitializeComponent();
            Course = course;
            Decider = decider;
            ListOfCourses = listOfCourses;
        }

        public void setupWindow()
        {
            DataContext = Course;
            descriptionlbl.Text = (Decider == Decider.ADD) ? labelCourseAdd : labelCourseEdit;

            // ucitavanje iz baze
            ListOfLanguages = new List<Language>();
            ListOfCourseTypes = new List<CourseType>();
            ListOfTeachers = new List<Teacher>();
            ListOfStudents = new ObservableCollection<Student>();

            languagecb.ItemsSource = ListOfLanguages;
            languagecb.DisplayMemberPath = "Name";
            languagecb.SelectedValuePath = "Id";
            languagecb.SelectedIndex = 0;

            courseTypecb.ItemsSource = ListOfLanguages;
            courseTypecb.DisplayMemberPath = "Name";
            courseTypecb.SelectedValuePath = "Id";
            courseTypecb.SelectedIndex = 0;

            teachercb.ItemsSource = ListOfTeachers;
            teachercb.DisplayMemberPath = "LastName";
            teachercb.SelectedValuePath = "Id";
            teachercb.SelectedIndex = 0;

            StudentsView = CollectionViewSource.GetDefaultView(ListOfStudents);
            studentsdg.ItemsSource = StudentsView;
            studentsdg.IsSynchronizedWithCurrentItem = true;

            if(Decider == Decider.ADD)
            {
                beginDatedpc.SelectedDate = DateTime.Today;
                endDatedpc.SelectedDate = DateTime.Today;
            }
            else
            {
                Language language = Course.Language;
                CourseType courseType = Course.CourseType;
                Teacher teacher = Course.Teacher;
            }
        }

        private void okbtn_Click(object sender, RoutedEventArgs e)
        {
            if(Decider == Decider.ADD)
            {
                Course.Add(Course);
                Course.Id = ApplicationA.Instance.Courses.Count() + 1;
                ApplicationA.Instance.Courses.Add(Course);
            }
            else
            {
                Course.Edit(Course);
            }
            Close();
        }

        private void studentsdg_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            switch ((string)e.Column.Header)
            {
                case "FirstName":
                    e.Column.Header = "Ime";
                    break;
                case "LastName":
                    e.Column.Header = "Prezime";
                    break;
                case "Address":
                    e.Cancel = true;
                    break;
                case "Jmbg":
                    e.Cancel = true;
                    break;
                case "Deleted":
                    e.Column.Header = "Obrisan";
                    break;
                case "ListOfPayments":
                    e.Cancel = true;
                    break;
                case "ListOfCourses":
                    e.Cancel = true;
                    break;
            }
        }
    }
}
