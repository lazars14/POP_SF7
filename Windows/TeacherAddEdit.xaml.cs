using POP_SF7.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public Teacher TeacherT { get; set; }
        public Decider Decider { get; set; }
        public ObservableCollection<Teacher> ListOfTeachers { get; set; }

        public string labelAddTeacher = "Dodavanje novog nastavnika";
        public string labelEditTeacher = "Izmena postojeceg nastavnika";

        public TeacherAddEdit(Teacher teacher, Decider decider, ObservableCollection<Teacher> listOfTeachers)
        {
            InitializeComponent();
            TeacherT = teacher;
            Decider = decider;
            ListOfTeachers = listOfTeachers;

            DataContext = TeacherT;
            personInfo.descriptionlbl.Text = (Decider == Decider.ADD) ? labelAddTeacher : labelEditTeacher;
        }

        private void okbtn_Click(object sender, RoutedEventArgs e)
        {
            if(Decider == Decider.ADD)
            {
                // dodavanje u bazi
                ListOfTeachers.Add(TeacherT);
            }
            else
            {
                // izmena u bazi
            }
            Close();
        }
    }
}
