using POP_SF7.Models.People;
using POP_SF7.ViewModels.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_SF7.ViewModels
{
    class LoginWindowViewModel : INotifyPropertyChanged
    {
        public List<User> ListOfUsers { get; set; }

        private string username;
        public string Username
        {
            get { return username; }
            set { username = value; OnPropertyChanged("Username"); }
        }

        private string password;
        public string Password
        {
            get { return password; }
            set { password = value; OnPropertyChanged("Password"); }
        }

        public LoginCommand LoginCommand { get; set; }

        public LoginWindowViewModel()
        {
            ListOfUsers = new List<User>(); // ucitavanje iz baze
            LoginCommand = new LoginCommand(this);
        }

        #region
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

        public void Login()
        {
            Console.WriteLine("CEraj");
        }
    }
}
