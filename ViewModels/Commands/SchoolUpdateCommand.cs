﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace POP_SF7.ViewModels.Commands
{
    internal class SchoolUpdateCommand : ICommand
    {
        public SchoolUpdateCommand(SchoolViewModel schoolViewModel)
        {
            ViewModel = schoolViewModel;
        }

        public SchoolViewModel ViewModel { get; set; }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            throw new NotImplementedException();
        }

        public void Execute(object parameter)
        {
            throw new NotImplementedException();
        }
    }
}
