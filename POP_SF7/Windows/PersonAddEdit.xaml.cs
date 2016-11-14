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

namespace POP_SF7.Windows
{
    /// <summary>
    /// Interaction logic for PersonAddEdit.xaml
    /// </summary>
    public partial class PersonAddEdit : Window
    {
        public string labelAddStudent = "Dodavanje novog ucenika";
        public string labelEditStudent = "Izmena postojeceg ucenika";
        public string labelAddTeacher = "Dodavanje novog nastavnika";
        public string labelAddTeacher = "Izmena postojeceg nastavnika";
        public string labelAddUser = "Dodavanje novog korisnika";
        public string labelAddUser = "Izmena postojeceg korisnika";

        public PersonAddEdit()
        {
            InitializeComponent();
        }

    }
}
