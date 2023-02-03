using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace BuffonNeedleExperiment.Models
{
    public class Settings
    {
        private int lineDistance = 80;
        public int LineDistance 
        { 
            get => lineDistance; 
            set
            {
                lineDistance = value;
                OnLineDistanceChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public int ActulFieldHeight { get; set; } = 220;
        public int NeedleLenght { get; set; } = 80;
        public int TimeDelay { get; set; } = 5;
        public int NeedleCount { get; set; } = 500;
        public Brush CrossNeedleCol { get; set; } = Brushes.Red;
        public Brush UnCrossNeedleCol { get; set; } = Brushes.YellowGreen;

        public event EventHandler OnLineDistanceChanged;
    }

    public static class ExtensionMethods
    {
        public static int RemoveAll<T>(this ObservableCollection<T> coll, Func<T, bool> condition)
        {
            var itemsToRemove = coll.Where(condition).ToList();

            foreach (var itemToRemove in itemsToRemove)
            {
                coll.Remove(itemToRemove);
            }

            return itemsToRemove.Count;
        }
    }
}
