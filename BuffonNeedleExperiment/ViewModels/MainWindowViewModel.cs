using BuffonNeedleExperiment.Command;
using BuffonNeedleExperiment.Models;
using LiveCharts;
using LiveCharts.Defaults;
using OxyPlot;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace BuffonNeedleExperiment.ViewModels
{
    public class MainWindowViewModel: INotifyPropertyChanged
    {
        private bool isRunnig;
        private const double max = 50;
        private double maxValue = max;
        private readonly Settings sett;
        private readonly LineFactory lineFactory;
        private readonly PiCounter counter;

        private double deviation = double.NaN;
        private double pointCount = double.NaN;
        private double estimatedPI = double.NaN;
        private double intersectCount = double.NaN;
        private readonly object itemsLock = new object();

        public ChartValues<double> ChartValues { get; } = new ChartValues<double>();
        public ObservableCollection<DataPoint> ChartValuess { get; private set; } = new AsyncObservableCollection<DataPoint>();
        public ObservableCollection<IElement> Elements { get; private set; }
        
        public ICommand LoadWindowCommand { get; }
        public ICommand DropNeedleCommand { get; }
        public ICommand StopProcessCommand { get; }
        public RefreshCommand RefreshCommand { get; }

        #region Property
        public Brush CrossNeedleColor
        {
            get => sett.CrossNeedleCol;
            set => sett.CrossNeedleCol = value;
        }

        public bool IsRunning
        {
            get => isRunnig;
            private set             
            {
                isRunnig = value;
                this.NotifyPropertyChanged(nameof(IsRunning));
            }
        }

        public int TimeDelay
        {
            get => sett.TimeDelay;
            set => sett.TimeDelay = value;
        }

        public Brush UncrossedNeedleColor
        {
            get => sett.UnCrossNeedleCol;
            set => sett.UnCrossNeedleCol = value;
        }

        public int NeedleCount
        {
            get => sett.NeedleCount;
            set => sett.NeedleCount = value;
        }

        public double EstimatedPI
        {
            get => estimatedPI;
            private set
            {
                estimatedPI = value;
                this.NotifyPropertyChanged(nameof(EstimatedPI));
            }
        }

        public double MaxValue
        {
            get => maxValue;
            private set
            {
                maxValue = value;
                this.NotifyPropertyChanged(nameof(MaxValue));
            }
        }

        public double Deviation
        {
            get => deviation;
            private set
            {
                deviation = value;
                this.NotifyPropertyChanged(nameof(Deviation));
            }
        }

        public double PointCount
        {
            get => pointCount;
            private set
            {
                pointCount = value;
                this.NotifyPropertyChanged(nameof(PointCount));
            }
        }

        public double IntersectCount
        {
            get => intersectCount;
            private set
            {
                intersectCount = value;
                this.NotifyPropertyChanged(nameof(IntersectCount));
            }
        }

        public int NeedleLength
        {
            get => sett.NeedleLenght;
            set => sett.NeedleLenght = value;
        }

        public int LineDistance
        {
            get => sett.LineDistance;
            set => sett.LineDistance = value;
        }
        #endregion

        public MainWindowViewModel(LineFactory lineFactory, PiCounter counter, Settings settings)
        {
            this.sett = settings;
            this.counter = counter;
            this.lineFactory = lineFactory;

            lineFactory.GetLines();
            this.Elements = new AsyncObservableCollection<IElement>(lineFactory.Lines);

            this.RefreshCommand = new RefreshCommand();
            this.DropNeedleCommand = new DropNeedlesCommand(counter);
            this.StopProcessCommand = new StopProcedureCommand(counter);

            counter.IsRunningChanged += Counter_IsRunningChanged;
            counter.OnMeasuringChanged += Counter_OnMeasuringChanged;
            RefreshCommand.OnRefreshRequired += RefreshCommand_OnRefreshRequired;
            lineFactory.OnLinesChanged += LineFactory_OnLinesChanged;
        }

        private void LineFactory_OnLinesChanged(object sender, EventArgs e)
        {
            Elements.RemoveAll(x => x.Type == UIElementType.Line);
            foreach (var item in lineFactory.Lines)
            {
                Elements.Add(item);
            }
        }

        private void RefreshCommand_OnRefreshRequired(object sender, EventArgs e)
        {
            Elements.RemoveAll(x => x.Type == UIElementType.Needle);
            IntersectCount = double.NaN;
            EstimatedPI = double.NaN;
            PointCount = double.NaN;
            Deviation = double.NaN;
            ChartValues.Clear();
            MaxValue = max;
        }

        private void Counter_IsRunningChanged(object sender, EventArgs e)
        {
            this.IsRunning = counter.IsRunning;
            if (this.IsRunning)
            {
                Elements.RemoveAll(x => x.Type == UIElementType.Needle);
                ChartValues.Clear();
                MaxValue = max;
                PointCount = 0;
            }
        }

        private void Counter_OnMeasuringChanged(object sender, EventArgs e)
        {
            PointCount++;
            //ChartValuess.Add(new DataPoint(PointCount, counter.ActualEstimatedPi));
            ChartValues.Add(counter.ActualEstimatedPi);
            EstimatedPI = counter.ActualEstimatedPi;
            IntersectCount = counter.IntersectCount;
            Deviation = Math.PI - counter.ActualEstimatedPi;
            MaxValue = PointCount > maxValue ? double.NaN : maxValue;
            Elements.Add(counter.ActualNeedle);
        }

        protected void NotifyPropertyChanged(string vlastnost)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(vlastnost));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
