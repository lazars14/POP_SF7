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
    /// Interaction logic for LanguagesCourseTypesMenu.xaml
    /// </summary>
    public partial class LanguagesCourseTypesMenu : Window
    {
        public string languagesLabel = "Jezici";
        public string courseTypesLabel = "Tipovi kurseva";

        public LanguagesCourseTypesMenu(string type)
        {
            InitializeComponent();
            descriptionlbl.Text = (type.Equals("Jezik")) ? languagesLabel : courseTypesLabel;
        }
    }
}
