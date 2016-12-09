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
            // izmena u bazi
        }
    }
}
