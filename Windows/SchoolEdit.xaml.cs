using System.Windows;

namespace POP_SF7
{
    /// <summary>
    /// Interaction logic for SchoolEdit.xaml
    /// </summary>
    public partial class SchoolEdit : Window
    {
        public SchoolS SchoolS { get; set; }

        public SchoolEdit(SchoolS school)
        {
            InitializeComponent();
            SchoolS = school;
            DataContext = SchoolS;
        }

        private void okbtn_Click(object sender, RoutedEventArgs e)
        {
            SchoolS.UpdateSchool(SchoolS);
            Close();
        }
    }
}
