using POP_SF7.Models.Courses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_SF7.ViewModels
{
    internal class CourseViewModel
    {
        public Course Course { get; set; }

        public CourseViewModel(Course course)
        {
            Course = course;
        }

    }
}
