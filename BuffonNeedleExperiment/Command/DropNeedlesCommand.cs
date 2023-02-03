using BuffonNeedleExperiment.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;

namespace BuffonNeedleExperiment.Command
{
    public class DropNeedlesCommand : ICommand
    {
        private readonly PiCounter counter;

        public DropNeedlesCommand(PiCounter counter)
        {
            this.counter = counter;
        }


        public bool CanExecute(object parameter)
        {
            return true;
        }


        public async void Execute(object parameter)
        {
            try
            {
                var values = (object[])parameter;
                await counter.EstimatePI((double)values[1], (double)values[0]);
            }
            catch
            {
                throw new Exception();
            }
        }

        public event EventHandler CanExecuteChanged;
    }
}
