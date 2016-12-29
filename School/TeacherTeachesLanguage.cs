using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_SF7.School
{
    class TeacherTeachesLanguage
    {
        public int Id { get; set; }
        public int TeacherId { get; set; }
        public int LanguageId { get; set; }
        public bool Deleted { get; set; }

        public TeacherTeachesLanguage(int id, int teacherId, int languageId, bool deleted)
        {
            Id = id;
            TeacherId = teacherId;
            LanguageId = languageId;
            Deleted = deleted;
        }
    }
}
