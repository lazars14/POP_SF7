using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace POP_SF7.Helpers
{
    class Search
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Jmbg { get; set; }
        public string UserName { get; set; }

        public PeopleDecider Decider { get; set; }

        public Search() { }

        public Search(string firstName, string lastName, string jmbgOfUsername, PeopleDecider decider)
        {
            FirstName = firstName;
            LastName = lastName;
            Decider = decider;
            if(Decider == PeopleDecider.User)
            {
                UserName = jmbgOfUsername;
            }
            else
            {
                Jmbg = jmbgOfUsername;
            }
        }
        
        public bool firstname(object s)
        {
            switch(Decider)
            {
                case PeopleDecider.Teacher:
                    Teacher t = s as Teacher;
                    return t.FirstName == FirstName;
                case PeopleDecider.User:
                    User u = s as User;
                    return u.FirstName == FirstName;
                case PeopleDecider.Student:
                    Student ss = s as Student;
                    return ss.FirstName == FirstName;
            }
            return false;
        }

        public bool lastname(object s)
        {
            switch (Decider)
            {
                case PeopleDecider.Teacher:
                    Teacher t = s as Teacher;
                    return t.LastName == LastName;
                case PeopleDecider.User:
                    User u = s as User;
                    return u.LastName == LastName;
                case PeopleDecider.Student:
                    Student ss = s as Student;
                    return ss.LastName == LastName;
            }
            return false;
        }

        public bool jmbg(object s)
        {
            switch (Decider)
            {
                case PeopleDecider.Teacher:
                    Teacher t = s as Teacher;
                    return t.Jmbg == Jmbg;
                case PeopleDecider.Student:
                    Student ss = s as Student;
                    return ss.Jmbg == Jmbg;
            }
            return false;
        }

        public bool username(object s)
        {
            User u = s as User; 
            return u.UserName == UserName;
        }
    }
}
