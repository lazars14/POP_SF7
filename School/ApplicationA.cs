﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_SF7
{
    public class ApplicationA
    {
        public const string CONNECTION_STRING = @"Integrated Security=true;
                                          Initial Catalog=Fakultet;
                                          Data Source=GORAN-PC";

        public School School { get; set; }
        public ObservableCollection<Language> Languages { get; set; }
        public ObservableCollection<CourseType> CourseTypes { get; set; }
        public ObservableCollection<Course> Courses { get; set; }
        public ObservableCollection<Payment> Payments { get; set; }
        public ObservableCollection<Teacher> Teachers { get; set; }
        public ObservableCollection<User> Users { get; set; }
        public ObservableCollection<Student> Students { get; set; }

        private static ApplicationA instance = new ApplicationA();

        public static ApplicationA Instance
        {
            get
            {
                return instance;
            }
        }

        private ApplicationA()
        {
            School = School.LoadSchool();
            Languages = new ObservableCollection<Language>();
            CourseTypes = new ObservableCollection<CourseType>();
            Courses = new ObservableCollection<Course>();
            Payments = new ObservableCollection<Payment>();
            Teachers = new ObservableCollection<Teacher>();
            Users = new ObservableCollection<User>();
            Students = new ObservableCollection<Student>();
        }

    }
}
