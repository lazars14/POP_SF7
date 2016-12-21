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
    /// Interaction logic for LanguageCourseTypeAddEdit.xaml
    /// </summary>
    public partial class CourseTypeAddEdit : Window
    {
        public CourseType CourseTypeC { get; set; }
        public Decider Decider { get; set; }

        public string labelAddCourseType = "Dodavanje novog tipa kursa";
        public string labelEditCourseType = "Izmena postojeceg tipa kursa";

        public CourseTypeAddEdit(CourseType courseType, Decider decider)
        {
            InitializeComponent();

            CourseTypeC = courseType;
            Decider = decider;

            DataContext = CourseTypeC;

            descriptionlbl.Text = (decider == Decider.ADD) ? labelAddCourseType : labelEditCourseType;
        }

        private void okbtn_Click(object sender, RoutedEventArgs e)
        {
            if (Decider == Decider.ADD)
            {
                CourseType.Add(CourseTypeC);
                CourseTypeC.Id = ApplicationA.Instance.CourseTypes.Count() + 1;
                ApplicationA.Instance.CourseTypes.Add(CourseTypeC);
            }
            else
            {
                CourseType.Edit(CourseTypeC);
            }
            Close();
        }
    }
}
