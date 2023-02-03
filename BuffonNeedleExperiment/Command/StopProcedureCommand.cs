using BuffonNeedleExperiment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BuffonNeedleExperiment.Command
{
    public class StopProcedureCommand : ICommand
    {
        private readonly PiCounter counter;

        public StopProcedureCommand(PiCounter counter)
        {
            this.counter = counter;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            counter.StopProcess();
        }
    }
}
