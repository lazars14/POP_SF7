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
    /// Interaction logic for LanguageCourseTypeAddEdit.xaml
    /// </summary>
    public partial class LanguageCourseTypeAddEdit : Window
    {
        public Language LanguageL { get; set; }
        public CourseType CourseTypeC { get; set; }
        public DeciderLanguageCourseType Decider { get; set; }

        public string labelAddLanguage = "Dodavanje novog jezika";
        public string labelEditLanguage = "Izmena postojeceg jezika";
        public string labelAddCourseType = "Dodavanje novog tipa kursa";
        public string labelEditCourseType = "Izmena postojeceg tipa kursa";

        // edit language
        public LanguageCourseTypeAddEdit(Language language, DeciderLanguageCourseType decider)
        {
            InitializeComponent();

            LanguageL = language;
            Decider = decider;

            setLabel();
        }

        // edit courseType
        public LanguageCourseTypeAddEdit(CourseType courseType, DeciderLanguageCourseType decider)
        {
            InitializeComponent();

            CourseTypeC = courseType;
            Decider = decider;

            setLabel();
        }

        // add language or courseType
        public LanguageCourseTypeAddEdit(DeciderLanguageCourseType decider)
        {
            InitializeComponent();
            setLabel();
        }

        public void setLabel()
        {
            if (Decider == DeciderLanguageCourseType.Language)
            {
                descriptionlbl.Text = (LanguageL == null) ? labelAddLanguage : labelEditLanguage;
            }
            else
            {
                descriptionlbl.Text = (CourseTypeC == null) ? labelAddCourseType : labelEditCourseType;
            }
        }
    }
}
