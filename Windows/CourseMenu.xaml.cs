﻿using POP_SF7.Windows;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace POP_SF7
{
    /// <summary>
    /// Interaction logic for CourseMenu.xaml
    /// </summary>
    public partial class CourseMenu : Window
    {
        public ICollectionView CoursesView { get; set; }
        public ICollectionView StudentsView { get; set; }

        public CourseMenu()
        {
            InitializeComponent();
            checkIfLoaded();
            setupWindow();

            CoursesView = CollectionViewSource.GetDefaultView(ApplicationA.Instance.Courses);

            coursesdg.ItemsSource = CoursesView;
            coursesdg.IsSynchronizedWithCurrentItem = true;
        }

        private void setupWindow()
        {
            POP_SF7.Language.Load();
            CourseType.Load();

            languagecb.ItemsSource = ApplicationA.Instance.Languages;
            languagecb.DisplayMemberPath = "Name";
            languagecb.SelectedValuePath = "Id";

            courseTypecb.ItemsSource = ApplicationA.Instance.CourseTypes;
            courseTypecb.DisplayMemberPath = "Name";
            courseTypecb.SelectedValuePath = "Id";
        }

        private void checkIfLoaded()
        {
            if(ApplicationA.Instance.Courses.Count == 0)
            {
                Course.Load();
            }
        }

        private void addbtn_Click(object sender, RoutedEventArgs e)
        {
            Course newCourse = new Course();
            CourseAddEdit add = new CourseAddEdit(newCourse, Decider.ADD);
            add.Show();
        }

        private void editbtn_Click(object sender, RoutedEventArgs e)
        {
            Course selectedCourse = CoursesView.CurrentItem as Course;
            if(selectedCourse == null)
            {
                MessageBox.Show("Morate da selektujete neki kurs da biste ga izmenili!");
            }
            else
            {
                Course backup = (Course)selectedCourse.Clone();
                CourseAddEdit edit = new CourseAddEdit(selectedCourse, Decider.EDIT);
                if(edit.ShowDialog() != true)
                {
                    int index = ApplicationA.Instance.Courses.IndexOf(selectedCourse);
                    ApplicationA.Instance.Courses[index] = backup;
                }
            }
        }

        private void deletebtn_Click(object sender, RoutedEventArgs e)
        {
            Course selectedCourse = CoursesView.CurrentItem as Course;
            if(selectedCourse == null)
            {
                MessageBox.Show("Morate da selektujete neki kurs da biste ga obrisali!");
            }
            else if (selectedCourse.Deleted == true)
            {
                MessageBox.Show("Selektovan kurs je vec obrisan!");
            }
            else
            {
                var result = MessageBox.Show("Da li ste sigurni da hocete da obrisete ovog korisnika?", "Upozorenje", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    selectedCourse = CoursesView.CurrentItem as Course;
                    Course.Delete(selectedCourse);
                    selectedCourse.Deleted = true;
                }
            }
        }

        private void sortbtn_Click(object sender, RoutedEventArgs e)
        {
            // problem oko bool? (moze da bude i null) i bool
            bool ascending = sortAscrb.IsChecked ?? false;
            ListSortDirection direction = (ascending == true) ? ListSortDirection.Ascending : ListSortDirection.Descending;

            CoursesView.SortDescriptions.Clear();
            if (idrb.IsChecked ?? false)
            {
                CoursesView.SortDescriptions.Add(new SortDescription(("Id"), direction));
            }
            else if (pricerb.IsChecked ?? false)
            {
                CoursesView.SortDescriptions.Add(new SortDescription(("Price"), direction));
            }
            else if (numberOfStudentsrb.IsChecked ?? false)
            {

            }
            else if (startDaterb.IsChecked ?? false)
            {
                CoursesView.SortDescriptions.Add(new SortDescription(("StartDate"), direction));
            }
            else if (endDaterb.IsChecked ?? false)
            {
                CoursesView.SortDescriptions.Add(new SortDescription(("EndDate"), direction));
            }
            CoursesView.Refresh();
        }

        private void searchbtn_Click(object sender, RoutedEventArgs e)
        {
            bool language = languagechb.IsChecked ?? false;
            bool courseType = courseTypechb.IsChecked ?? false;


            if (language && courseType)
            {
                // pokupi podatke iz oba comboboxa
                // funkcija
            }
            else if(language)
            {
                // pokupi podatke iz comboboxa za jezik
                // funkcija
            }
            else if(courseType)
            {
                // pokupi podatke iz comboboxa za tip kursa
                // funkcija
            }
            else
            {
                MessageBox.Show("Morate da otkacite makar jedan kriterijum da biste pretrazili kurs!");
            }
        }

        private void coursesdg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void cancelSearchbtn_Click(object sender, RoutedEventArgs e)
        {
            CoursesView.Filter = null;
        }

        private void studentsdg_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            switch ((string)e.Column.Header)
            {
                case "Id":
                    e.Cancel = true;
                    break;
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
                    e.Column.Header = "JMBG";
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
                case "Error":
                    e.Cancel = true;
                    break;
            }
        }

        private void coursesdg_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            switch ((string)e.Column.Header)
            {
                case "Id":
                    e.Cancel = true;
                    break;
                case "Language":
                    e.Cancel = true;
                    break;
                case "CourseType":
                    e.Cancel = true;
                    break;
                case "Price":
                    e.Column.Header = "Cena";
                    break;
                case "ListOfStudents":
                    e.Cancel = true;
                    break;
                case "Teacher":
                    e.Cancel = true;
                    break;
                case "StartDate":
                    e.Column.Header = "Datum pocetka";
                    break;
                case "EndDate":
                    e.Column.Header = "Datum kraja";
                    break;
                case "Deleted":
                    e.Column.Header = "Obrisan";
                    break;
                case "Error":
                    e.Cancel = true;
                    break;
            }
        }
    }
}
