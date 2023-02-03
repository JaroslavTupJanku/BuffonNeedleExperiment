using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace BuffonNeedleExperiment.View.Converter
{
    public class ColorConveter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var type = typeof(Colors);
            var res = type.GetProperties(BindingFlags.Static | BindingFlags.Public)
                .Where(p => p.GetValue(null, null).ToString() == value.ToString())
                .Select(p => p).FirstOrDefault();
            return res;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var colorName =  ((PropertyInfo)value).Name;
            var brush = new SolidColorBrush((Color)ColorConverter.ConvertFromString(colorName));
            return brush;
        }
    }
}
