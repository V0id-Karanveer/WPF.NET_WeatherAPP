using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MVVM_Prac.ViewModel.Commands
{
    public class SearchCommand : ICommand
    {
        public WeatherVM Vm { get; set; }

        public SearchCommand(WeatherVM vm)
        {
            Vm = vm;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            string s = parameter as string;
            if (String.IsNullOrEmpty(s))
            {
                return false;
            }
            return true;
        }

        public void Execute(object parameter)
        {
            Vm.MakeQuery();
        }
    }
}
