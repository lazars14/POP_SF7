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
    /// Interaction logic for CourseAddEdit.xaml
    /// </summary>
    public partial class CourseAddEdit : Window
    {
        public string labelCourseAdd = "Dodavanje novog kursa";
        public string labelCourseEdit = "Izmena postojeceg kursa";

        public CourseAddEdit(Course course, string action)
        {
            InitializeComponent();
            if(action.Equals("Izmena"))
            {
                // fill fields with course data
            }
            descriptionlbl.Text = (action.Equals("Dodavanje")) ? labelCourseAdd : labelCourseEdit;
        }
    }
}
