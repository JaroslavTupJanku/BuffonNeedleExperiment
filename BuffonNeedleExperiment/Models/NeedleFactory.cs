using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace BuffonNeedleExperiment.Models
{
    public class Needle : IElement
    {
        public double Y1 { get; }
        public double Y2 { get; }
        public double X1 { get; }
        public double X2 { get; }
        public Brush Color { get; }
        public UIElementType Type { get; } = UIElementType.Needle;

        public Needle(double y1, double y2, double x1, double x2,  Brush color)
        {
            this.Y1 = y1;
            this.Y2 = y2;
            this.X1 = x1;
            this.X2 = x2;
            this.Color = color;
        }
    }

    public class NeedleFactory
    {
        private readonly Random generator = new Random();
        private readonly Settings sett;

        public NeedleFactory(Settings settings)
        {
            this.sett = settings;
        }

        public Needle GetNeedle(double width, double height, double[] array)
        {
            var angle = generator.NextDouble() * (Math.PI * 2);
            var x1 = generator.NextDouble() * (width - 2 * sett.NeedleLenght) + sett.NeedleLenght;
            var y1 = generator.NextDouble() * (height - sett.NeedleLenght*2) + sett.NeedleLenght;

            var x2 = sett.NeedleLenght * Math.Cos(angle) + x1;
            var y2 = sett.NeedleLenght * Math.Sin(angle) + y1;
            var isCrossed = array.Any(y => (y > y1 && y < y2) || ( y < y1 && y > y2));
            var color = isCrossed ? sett.CrossNeedleCol : sett.UnCrossNeedleCol;

            return new Needle(y1, y2, x1, x2, color);
        }
    }
}
