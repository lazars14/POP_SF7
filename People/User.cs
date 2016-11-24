using System.ComponentModel;

namespace POP_SF7
{
    public enum Role { Administrator, Employee }

    public class User : Person, INotifyPropertyChanged
    {
        private string userName;
        public string UserName
        {
            get { return userName; }
            set { userName = value; OnPropertyChanged("UserName"); }
        }

        private string password;
        public string Password
        {
            get { return password; }
            set { password = value; OnPropertyChanged("Password"); }
        }

        private Role userRole;
        public Role UserRole
        {
            get { return userRole; }
            set { userRole = value; OnPropertyChanged("UserRole"); }
        }

        public User(int id, string firstName, string lastName, string jmbg, string personAddress, string userName, string password, Role userRole, bool deleted) : base(id, firstName, lastName, jmbg, personAddress, deleted)
        {
            UserName = userName;
            Password = password;
            UserRole = userRole;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
