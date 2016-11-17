using System.Collections.Generic;

namespace POP_SF7
{
    class CourseCRUD
    {
        public static void addCourse(List<Course> coursesList, Course course)
        {
            coursesList.Add(course);
        }

        public static bool deleteCourse(List<Course> coursesList, int id)
        {
            for(int i = 0; i < coursesList.Count; i++)
            {
                if(coursesList[i].Id == id)
                {
                    coursesList[i].Deleted = true;
                    return true;
                }
            }
            return false;
        }

        public void editCourse(List<Course> coursesList, Course editedCourse)
        {
            for(int i = 0; i < coursesList.Count; i++)
            {
                if(coursesList[i].Id == editedCourse.Id)
                {
                    coursesList[i] = editedCourse;
                }
            }
        }

        public static void sortCourses(School school)
        {
            List<Course> list = school.ListOfCourses;
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

        public static Course findCourseById(List<Course> coursesList, int id)
        {
            foreach(Course course in coursesList)
            {
                if(course.Id == id)
                {
                    return course;
                }
            }

            return null;
        }

        public static void find(List<Course> coursesList, string userInput, string param, List<string> results)
        {
            foreach (Course course in coursesList)
            {
                switch (param)
                {
                case "Jezik":
                    if (userInput.Equals(course.Language))
                    {
                        results.Add(course.ToString());
                    }
                    break;
                case "Tip":
                    if (userInput.Equals(course.CourseType))
                    {
                        results.Add(course.ToString());
                    }
                    break;
                }
            }
        }

        public static int getMaxId(List<Course> listOfCourses)
        {
            int maxId = 0;
            foreach (Course course in listOfCourses)
            {
                if (course.Id > maxId)
                {
                    maxId = course.Id;
                }
            }
            maxId += 1;
            return maxId;
        }
    }
}
