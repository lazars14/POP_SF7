
using System;
using System.ComponentModel;

namespace POP_SF7
{
    public class CourseType : INotifyPropertyChanged, ICloneable
    {
        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; OnPropertyChanged("Id"); }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; OnPropertyChanged("Name"); }
        }

        private bool deleted;
        public bool Deleted
        {
            get { return deleted; }
            set { deleted = value; OnPropertyChanged("Deleted"); }
        }

        public CourseType() { }

        public CourseType(int id, string name, bool deleted)
        {
            Id = id;
            Name = name;
            Deleted = deleted;
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        #endregion

        #region ICloneable

        public object Clone()
        {
            CourseType courseCopy = new CourseType();
            courseCopy.Id = Id;
            courseCopy.Name = Name;
            courseCopy.Deleted = Deleted;

            return courseCopy;
        }

        #endregion

    }
}
