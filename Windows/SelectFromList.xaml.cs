using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System;
using POP_SF7.Helpers;

namespace POP_SF7.Windows
{
    /// <summary>
    /// Interaction logic for SelectFromList.xaml
    /// </summary>

    public enum SelectFromMenuOrAddDecider { MENU, ADD }
    public enum CourseStudentDecider { STUDENT, COURSE }

    public partial class SelectFromList : Window
    {
        public SelectFromMenuOrAddDecider MenuAddDecider { get; set; }
        public CourseStudentDecider CourseStudentDecider { get; set; }

        public PaymentMenu MenuWindow { get; set; }
        public PaymentAddEdit AddEditWindow { get; set; }

        public string selectStudent = "Izaberite ucenika";
        public string selectCourse = "Izaberite kurs";

        public SelectFromList(SelectFromMenuOrAddDecider decider, CourseStudentDecider decider2, PaymentMenu menu)
        {
            InitializeComponent();

            MenuAddDecider = decider;
            CourseStudentDecider = decider2;

            MenuWindow = menu;

            setupWindow();
        }

        public SelectFromList(SelectFromMenuOrAddDecider decider, CourseStudentDecider decider2, PaymentAddEdit addEdit)
        {
            InitializeComponent();

            MenuAddDecider = decider;
            CourseStudentDecider = decider2;

            AddEditWindow = addEdit;

            setupWindow();
        }

        private void setupWindow()
        {
            if (CourseStudentDecider == CourseStudentDecider.COURSE)
            {
                description.Text = selectCourse;
                if(MenuAddDecider == SelectFromMenuOrAddDecider.ADD)
                {
                    dataGrid.ItemsSource = AddEditWindow.Student.ListOfCourses;
                }
                else
                {
                    dataGrid.ItemsSource = ApplicationA.Instance.Courses;
                }
                
            }
            else
            {
                description.Text = selectStudent;
                dataGrid.ItemsSource = ApplicationA.Instance.Students;
            }
        }

        private void closebtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void okbtn_Click(object sender, RoutedEventArgs e)
        {
            if (CourseStudentDecider == CourseStudentDecider.COURSE)
            {
                Course selectedCourse = (Course)dataGrid.SelectedItem;
                if(selectedCourse == null)
                {
                    MessageBox.Show("Morate da selektujete red u tabeli da biste ga preuzeli!");
                }
                else
                {
                    if(MenuAddDecider == SelectFromMenuOrAddDecider.MENU)
                    {
                        MenuWindow.SearchCourse = selectedCourse;
                        MenuWindow.coursetb.Text = selectedCourse.StartDate.ToShortDateString() + "-" + selectedCourse.EndDate.ToShortDateString() + ", " + selectedCourse.Price.ToString();
                    }
                    else
                    {
                        AddEditWindow.Course = selectedCourse;
                        AddEditWindow.coursetb.Text = selectedCourse.StartDate.ToShortDateString() + "-" + selectedCourse.EndDate.ToShortDateString() + ", " + selectedCourse.Price.ToString();
                    }
                    Close();
                }
            }
            else
            {
                Student selectedStudent = (Student)dataGrid.SelectedItem;
                if (selectedStudent == null)
                {
                    MessageBox.Show("Morate da selektujete red u tabeli da biste ga preuzeli!");
                }
                else
                {
                    if (MenuAddDecider == SelectFromMenuOrAddDecider.MENU)
                    {
                        MenuWindow.SearchStudent = selectedStudent;
                        MenuWindow.studenttb.Text = selectedStudent.FirstName + " " + selectedStudent.LastName;
                    }
                    else
                    {
                        AddEditWindow.Student = selectedStudent;
                        AddEditWindow.studenttb.Text = selectedStudent.FirstName + " " + selectedStudent.LastName;
                    }
                    Close();
                }
            }
        }

        private void dataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if(CourseStudentDecider == CourseStudentDecider.COURSE)
            {
                LoadColumnsHelper.LoadCourse(e);
            }
            else
            {
                LoadColumnsHelper.LoadStudent(e);
            }
        }
    }
}
