﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Converter
{
    public class RelayCommand : ICommand
    {
        public event EventHandler CanExecuteChanged = (sender, e)=> { };

        private Action<object> _Action;
        public RelayCommand(Action<object> action)
        {
            _Action = action;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _Action(parameter);
        }
    }
}
