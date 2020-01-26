using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace KeySim.Common.Converter
{
    public class EnumToBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            if (value != null && parameter != null && value.GetType().IsEnum)
            {
                var v = System.Convert.ChangeType(value, Enum.GetUnderlyingType(value.GetType()));
                var p = System.Convert.ChangeType(parameter, Enum.GetUnderlyingType(value.GetType()));
                return v.Equals(p);
            }

            return DependencyProperty.UnsetValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && parameter != null && (bool)value && int.TryParse(parameter.ToString(), out int p))
            {                
                var v = Enum.ToObject(targetType, p);
                return v;
            }
            return DependencyProperty.UnsetValue;
        }
    }
}
