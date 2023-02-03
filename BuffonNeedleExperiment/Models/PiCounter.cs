using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Data;

namespace BuffonNeedleExperiment.Models
{
    public class PiCounter
    {
        private bool isRunnig;
        private double[] array;
        private readonly Settings sett;
        private readonly NeedleFactory factory;
        private readonly LineFactory lineFactory;
        private CancellationTokenSource tokenSource;

        public int IntersectCount { get; private set; }
        public Needle ActualNeedle { get; private set; }
        public double ActualEstimatedPi { get; private set; }

        public bool IsRunning
        {
            get => isRunnig;
            private set
            {
                isRunnig = value;
                IsRunningChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public PiCounter(Settings settings, LineFactory lineFactory)
        {
            this.sett = settings;
            this.lineFactory = lineFactory;
            this.factory = new NeedleFactory(settings);

            lineFactory.OnLinesChanged += LineFactory_OnLinesChanged;
        }

        public async Task EstimatePI(double height, double width)
        {
            try
            {
                IsRunning = true;
                tokenSource = new CancellationTokenSource();
                await Run(height, width, tokenSource.Token);
            }
            finally
            {
                IsRunning = false;
                IntersectCount = 0;
                tokenSource.Dispose();
            }
        }

        public async Task Run(double height, double width, CancellationToken token)
        {
            await Task.Run(() =>
            {
                for (int i = 1; i <= sett.NeedleCount; i++)
                {
                    ActualNeedle = factory.GetNeedle((double)width, (double)height, array);
                    if (ActualNeedle.Color == sett.CrossNeedleCol) { IntersectCount++; }
                    ActualEstimatedPi = 2 * (double)sett.NeedleLenght * i / (sett.LineDistance * IntersectCount);
                    Task.Delay(sett.TimeDelay).Wait();

                    if (token.IsCancellationRequested) return;
                    OnMeasuringChanged?.Invoke(this, EventArgs.Empty);
                }
            });
        }

        public void StopProcess()
        {
            tokenSource.Cancel();
        }

        private void LineFactory_OnLinesChanged(object sender, EventArgs e)
        {
            this.array = lineFactory.Lines.Select(x=>x.Y1).ToArray();
        }

        public event EventHandler IsRunningChanged;
        public event EventHandler OnMeasuringChanged;
    }
}
