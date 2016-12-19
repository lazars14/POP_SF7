using POP_SF7.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    public partial class StudentAddEdit : Window
    {
        public Student StudentS { get; set; }
        public Decider Decider { get; set; }
        public ObservableCollection<Student> ListOfStudents { get; set; }

        public string labelAddStudent = "Dodavanje novog ucenika";
        public string labelEditStudent = "Izmena postojeceg ucenika";
        
        public StudentAddEdit(Student student, Decider decider, ObservableCollection<Student> listOfStudents)
        {
            InitializeComponent();
            StudentS = student;
            Decider = decider;
            ListOfStudents = listOfStudents;

            DataContext = StudentS;
            personInfo.descriptionlbl.Text = (Decider == Decider.ADD) ? labelAddStudent : labelEditStudent;
        }

        private void okbtn_Click(object sender, RoutedEventArgs e)
        {
            if(Decider == Decider.ADD)
            {
                // dodavanje u bazu
                ListOfStudents.Add(StudentS);
            }
            else
            {
                // izmena u bazi
            }
            Close();
        }
    }
}
