using POP_SF7.Windows;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

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
                Student.Add(StudentS);
                StudentS.Id = ApplicationA.Instance.Students.Count() + 1;
                ApplicationA.Instance.Students.Add(StudentS);
            }
            else
            {
                Student.Edit(StudentS);
            }
            Close();
        }
    }
}
