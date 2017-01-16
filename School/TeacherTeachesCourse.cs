using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace POP_SF7.School
{
    public class TeacherTeachesCourse
    {
        public int Id { get; set; }
        public int TeacherId { get; set; }
        public int CourseId { get; set; }
        public bool Deleted { get; set; }

        public TeacherTeachesCourse(int id, int teacherId, int courseId, bool deleted)
        {
            Id = id;
            TeacherId = teacherId;
            CourseId = courseId;
            Deleted = deleted;
        }
    }
}
