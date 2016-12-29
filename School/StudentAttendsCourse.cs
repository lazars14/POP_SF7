using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_SF7.School
{
    class StudentAttendsCourse
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public int StudentId { get; set; }
        public bool Deleted { get; set; }

        public StudentAttendsCourse(int id, int courseId, int studentId, bool deleted)
        {
            Id = id;
            CourseId = courseId;
            StudentId = studentId;
            Deleted = deleted;
        }
    }
}
