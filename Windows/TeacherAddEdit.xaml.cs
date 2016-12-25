using POP_SF7.Windows;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

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
                Teacher.Add(TeacherT);
                TeacherT.Id = ApplicationA.Instance.Teachers.Count() + 1;
                ListOfTeachers.Add(TeacherT);
            }
            else
            {
                Teacher.Edit(TeacherT);
            }
            Close();
        }
    }
}
