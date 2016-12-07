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
    class LoginWindowViewModel
    {
        public List<User> ListOfUsers { get; set; }
        public User User { get; set; }

        public LoginCommand LoginCommand { get; set; }

        public LoginWindowViewModel()
        {
            ListOfUsers = new List<User>(); // ucitavanje iz baze
            LoginCommand = new LoginCommand(this);
            User = new User();
        }

        public void Login()
        {
            Console.WriteLine("CEraj");
        }
    }
}
