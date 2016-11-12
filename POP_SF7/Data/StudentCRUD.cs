using System.Collections.Generic;

namespace POP_SF7
{
    class StudentCRUD
    {
        public StudentCRUD() {}
        
        public static void addStudent(List<Student> studentList, Student student)
        {
            studentList.Add(student);
        }

        public static bool deleteStudent(List<Student> studentsList, int id)
        {
            for (int i = 0; i < studentsList.Count; i++)
            {
                if (studentsList[i].Id == id)
                {
                    studentsList[i].Deleted = true;
                    return true;
                }
            }
            return false;
        }

        public void editStudent(List<Student> studentsList, Student editedStudent)
        {
            for(int i = 0; i < studentsList.Count; i++)
            {
                if(studentsList[i].Id == editedStudent.Id)
                {
                    studentsList[i] = editedStudent;
                }
            }
        }

        public static Student findStudentById(List<Student> studentsList, int id)
        {
            foreach(Student student in studentsList)
            {
                if(id == student.Id)
                {
                    return student;
                }
            }

            return null;
        }

        public static void sortStudents(School school)
        {
            List<Student> list = school.ListOfStudents;
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


        public static void find(List<Student> studentsList, string userInput, string param, List<string> results)
        {
            foreach (Student student in studentsList)
            {
                switch(param)
                {
                    case "Ime":
                        if (param.Equals(student.FirstName))
                        {
                            results.Add(student.ToString());
                        }
                        break;
                    case "Prezime":
                        if (param.Equals(student.LastName))
                        {
                            results.Add(student.ToString());
                        }
                        break;
                    case "Jmbg":
                        if (param.Equals(student.JMBG))
                        {
                            results.Add(student.ToString());
                        }
                        break;
                }
            }
        }

        public static int getMaxId(List<Student> listOfStudents)
        {
            int maxId = 0;
            foreach (Student student in listOfStudents)
            {
                if (student.Id > maxId)
                {
                    maxId = student.Id;
                }
            }
            maxId += 1;
            return maxId;
        }
    }
}
