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
    /// Interaction logic for PersonAddEdit.xaml
    /// </summary>
    public partial class TeacherAddEdit : Window
    {
        public User UserU { get; set; }
        public Teacher TeacherT { get; set; }
        public Student StudentS { get; set; }
        public PeopleDecider Decider { get; set; }

        public string labelAddUser = "Dodavanje novog korisnika";
        public string labelEditUser = "Izmena postojeceg korisnika";
        public string labelAddTeacher = "Dodavanje novog nastavnika";
        public string labelEditTeacher = "Izmena postojeceg nastavnika";
        public string labelAddStudent = "Dodavanje novog ucenika";
        public string labelEditStudent = "Izmena postojeceg ucenika";
        
        // add
        public TeacherAddEdit(PeopleDecider decider)
        {
            InitializeComponent();

            Decider = decider;

            setupWindow();
        }

        // edit user
        public PersonAddEdit(User selectedUser, PeopleDecider decider)
        {
            InitializeComponent();

            Decider = decider;
            UserU = selectedUser;

            setupWindow();
        }

        // edit teacher
        public PersonAddEdit(Teacher selectedTeacher, PeopleDecider decider)
        {
            InitializeComponent();

            Decider = decider;
            TeacherT = selectedTeacher;

            setupWindow();
        }

        // edit student
        public PersonAddEdit(Student selectedStudent, PeopleDecider decider)
        {
            InitializeComponent();

            Decider = decider;
            StudentS = selectedStudent;

            setupWindow();
        }

        public void setupWindow()
        {
            switch (Decider)
            {
                case PeopleDecider.User:
                    descriptionlbl.Text = (UserU == null) ? labelAddUser : labelEditUser;
                    tabControl.Visibility = Visibility.Collapsed;
                    break;
                case PeopleDecider.Teacher:
                    descriptionlbl.Text = (TeacherT == null) ? labelAddTeacher : labelEditTeacher;
                    usergb.Visibility = Visibility.Collapsed;
                    break;
                case PeopleDecider.Student:
                    descriptionlbl.Text = (StudentS == null) ? labelAddStudent : labelEditStudent;
                    usergb.Visibility = Visibility.Collapsed;
                    break;
            }
        }
    }
}
