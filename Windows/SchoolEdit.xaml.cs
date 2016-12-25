using System.Windows;

namespace POP_SF7
{
    /// <summary>
    /// Interaction logic for SchoolEdit.xaml
    /// </summary>
    public partial class SchoolEdit : Window
    {
        public School School { get; set; }

        public SchoolEdit(School school)
        {
            InitializeComponent();
            School = school;
            DataContext = School;
        }

        private void okbtn_Click(object sender, RoutedEventArgs e)
        {
            School.UpdateSchool(School);
            Close();
        }
    }
}
