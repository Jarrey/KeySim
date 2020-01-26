using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using static KeyboardSim.WinNative;

namespace KeyboardSim.Converter
{
    public class ModKeyToBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && parameter != null &&
                Enum.TryParse(value.ToString(), out ModKeys key) && 
                Enum.TryParse(parameter.ToString(), out ModKeys parKey))
            {
                return key == parKey;
            }
            return DependencyProperty.UnsetValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && parameter != null && (bool)value && 
                Enum.TryParse(parameter.ToString(), out ModKeys key))
            {
                return key;
            }
            return DependencyProperty.UnsetValue;
        }
    }
}
