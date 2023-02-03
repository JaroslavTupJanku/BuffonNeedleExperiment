using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace BuffonNeedleExperiment.Models
{
    public enum UIElementType
    {
        Line,
        Needle
    }

    public interface IElement
    {
        double Y1 { get; }
        double Y2 { get; }
        double X1 { get; }
        double X2 { get; }
        Brush Color { get; }
        UIElementType Type { get; }
    }
}
