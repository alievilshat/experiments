using System;
using System.Collections;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace Mapper
{
    public class ConcatConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return string.Concat(values);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
