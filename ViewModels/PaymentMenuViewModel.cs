using POP_SF7.Models.Courses;
using POP_SF7.Models.People;
using POP_SF7.Models.School;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_SF7.ViewModels
{
    internal class PaymentMenuViewModel
    {
        public List<Payment> ListOfPayments { get; set; }
        public List<Course> ListOfCourses { get; set; }
        public List<Student> ListOfStudents { get; set; }

        public bool SearchCourse { get; set; }
        public bool SearchStudent { get; set; }
        public string Course { get; set; }
        public string Student { get; set; }

        public string SortCriteria { get; set; }
        public string SortOrder { get; set; }
    }
}
