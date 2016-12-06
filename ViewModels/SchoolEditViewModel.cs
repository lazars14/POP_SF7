using POP_SF7.Models.School;
using POP_SF7.ViewModels.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace POP_SF7.ViewModels
{
    internal class SchoolEditViewModel
    {
        public SchoolEditViewModel()
        {
            School = school;
            SchoolUpdateCommand = new SchoolUpdateCommand(this);
        }

        private School school;
        public School School
        {
            get { return school; }
            set { school = value; }
        }

        public SchoolUpdateCommand SchoolUpdateCommand { get; set; }

        public void UpdateSchoolData()
        {

        }

    }
}
