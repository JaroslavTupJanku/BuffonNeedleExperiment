using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BuffonNeedleExperiment.View
{
    /// <summary>
    /// Interaction logic for MyChart.xaml
    /// </summary>
    public partial class MyChart : UserControl
    {
        public MyChart()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty estimatedPI = DependencyProperty.Register("EstimatedPI", typeof(String), typeof(MyChart), new FrameworkPropertyMetadata("NaN"));
        public String EstimatedPI
        {
            get { return GetValue(estimatedPI).ToString(); }
            set { SetValue(estimatedPI, value); }
        }

        public static readonly DependencyProperty pointCount = DependencyProperty.Register("PointCount", typeof(String), typeof(MyChart), new FrameworkPropertyMetadata("NaN"));
        public String PointCount
        {
            get { return GetValue(pointCount).ToString(); }
            set { SetValue(pointCount, value); }
        }

        public static readonly DependencyProperty ChartValuesProperty = DependencyProperty.Register("ChartValues", typeof(ChartValues<double>), typeof(MyChart));
        public ChartValues<double> ChartValues
        {
            get { return (ChartValues<double>)GetValue(ChartValuesProperty); }
            set { SetValue(ChartValuesProperty, value); }
        }

        public static readonly DependencyProperty DeviationProperty = DependencyProperty.Register("Deviation", typeof(string), typeof(MyChart), new FrameworkPropertyMetadata("NaN"));
        public string Deviation
        {
            get { return GetValue(DeviationProperty).ToString(); }
            set { SetValue(DeviationProperty, value); }
        }
    }
}
