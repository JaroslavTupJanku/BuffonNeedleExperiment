using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace BuffonNeedleExperiment.Models
{
    public class Line : IElement
    {
        public double Y1 { get; }
        public double Y2 { get; }
        public double X1 { get; }
        public double X2 { get; }
        public Brush Color { get; } = Brushes.LightGray;
        public UIElementType Type { get; } = UIElementType.Line;

        public Line(double y1, double y2)
        {
            this.Y1 = y1;
            this.Y2 = y2;
        }
    }

    public class LineFactory
    {
        private readonly Settings sett;
        public List<IElement> Lines { get; } = new List<IElement>();

        public LineFactory(Settings settings)
        {
            this.sett = settings;
            this.sett.OnLineDistanceChanged += Sett_OnLineDistanceChanged;
        }

        public void GetLines()
        {
            for (int i = sett.ActulFieldHeight/2; i < sett.ActulFieldHeight; i += sett.LineDistance)
            {
                var lineOffset = sett.ActulFieldHeight / 2 - (i - sett.ActulFieldHeight / 2);
                Lines.Add(new Line(lineOffset, lineOffset));
                Lines.Add(new Line(i, i));
            }
            OnLinesChanged?.Invoke(this, EventArgs.Empty);
               
        }

        private void Sett_OnLineDistanceChanged(object sender, EventArgs e)
        {
            Lines.Clear();
            GetLines();
        }

        public event EventHandler OnLinesChanged;
    }
}
