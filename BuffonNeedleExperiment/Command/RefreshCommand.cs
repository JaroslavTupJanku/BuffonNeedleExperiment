using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BuffonNeedleExperiment.Command
{
    public class RefreshCommand : ICommand
    {
        public RefreshCommand()
        {
        }


        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            this.OnRefreshRequired?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler OnRefreshRequired;
        public event EventHandler CanExecuteChanged;
    }

}
