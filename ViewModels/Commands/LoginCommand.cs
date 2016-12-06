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
            if (String.IsNullOrEmpty(ViewModel.Username) || String.IsNullOrEmpty(ViewModel.Password))
            {
                return false;
            }
            return true;
        }

        public void Execute(object parameter)
        {
            ViewModel.Login();
        }
    }
}
