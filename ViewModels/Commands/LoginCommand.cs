using POP_SF7.Models.People;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace POP_SF7.ViewModels.Commands
{
    class LoginCommand : ICommand
    {
        public LoginWindowViewModel ViewModel { get; set; }

        public LoginCommand(LoginWindowViewModel viewModel)
        {
            ViewModel = viewModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            try
            {
                User user = parameter as User;
                if (String.IsNullOrEmpty(user.UserName) || String.IsNullOrEmpty(user.Password))
                {
                    return false;
                }   
            }
            catch (NullReferenceException)
            {
                Console.WriteLine("Object is null");
            }

            return true;
        }

        public void Execute(object parameter)
        {
            ViewModel.Login();
        }
    }
}
