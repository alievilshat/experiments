using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace ScriptModule.UIUtils
{
    /// <summary>
    /// Convert between boolean and visibility
    /// </summary>
    public sealed class BoolToVisibilityConverter : IValueConverter
    {
        public bool Invert { get; set; }

        /// <summary> 
        /// Convert bool or Nullable&lt;bool&gt; to Visibility
        /// </summary> 
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool bValue = false;
            if (value is bool)
            {
                bValue = (bool)value;
            }
            else if (value is bool?)
            {
                var tmp = (bool?)value;
                bValue = tmp.HasValue && tmp.Value;
            }
            if (Invert)
                bValue = !bValue;

            return bValue ? Visibility.Visible : Visibility.Collapsed;
        }

        /// <summary>
        /// Convert Visibility to boolean 
        /// </summary>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Visibility)
            {
                var res = (Visibility)value == Visibility.Visible;
                if (Invert)
                    res = !res;
                return res;
            }
            return false;
        }
    } 
}
