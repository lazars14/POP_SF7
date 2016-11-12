using System.Collections.Generic;

namespace POP_SF7
{
    class TeacherCRUD
    {
        public TeacherCRUD() {}

        public static void addTeacher(List<Teacher> teachersList, Teacher teacher)
        {
            teachersList.Add(teacher);
        }

        public static bool deleteTeacher(List<Teacher> teachersList, int id)
        {
            for (int i = 0; i < teachersList.Count; i++)
            {
                if (teachersList[i].Id == id)
                {
                    teachersList[i].Deleted = true;
                    return true;
                }
            }
            return false;
        }

        public void editTeacher(List<Teacher> teachersList, Teacher editedTeacher)
        {
            for(int i = 0; i < teachersList.Count; i++)
            {
                if(teachersList[i].Id == editedTeacher.Id)
                {
                    teachersList[i] = editedTeacher;
                }
            }
        }

        public static Teacher findTeacherById(List<Teacher> teachersList, int id)
        {
            foreach(Teacher teacher in teachersList)
            {
                if(id == teacher.Id)
                {
                    return teacher;
                }
            }

            return null;
        }

        public static void sortTeachers(School school)
        {
            List<Teacher> list = school.ListOfTeachers;
            for (int i = 0; i < list.Count - 1; i++)
            {
                for (int j = i + 1; j < list.Count; j++)
                {
                    if (list[i].Id > list[j].Id)
                    {
                        var tmp = list[i];
                        list[i] = list[j];
                        list[j] = tmp;
                    }
                }
            }
        }

        public static void find(List<Teacher> teachersList, string userInput, string param, List<string> results)
        {
            foreach(Teacher teacher in teachersList)
            {
                switch (param)
                {
                    case "Ime":
                        if (param.Equals(teacher.FirstName))
                        {
                            results.Add(teacher.ToString());
                        }
                        break;
                    case "Prezime":
                        if (param.Equals(teacher.LastName))
                        {
                            results.Add(teacher.ToString());
                        }
                        break;
                    case "Jmbg":
                        if (param.Equals(teacher.JMBG))
                        {
                            results.Add(teacher.ToString());
                        }
                        break;
                }
            }
        }

        public static int getMaxId(List<Teacher> listOfTeachers)
        {
            int maxId = 0;
            foreach (Teacher teacher in listOfTeachers)
            {
                if (teacher.Id > maxId)
                {
                    maxId = teacher.Id;
                }
            }
            maxId += 1;
            return maxId;
        }
    }
}
