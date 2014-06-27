using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Xml;

namespace Mapper
{
    class ContentPathsConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length != 2)
                return null;

            var path = values[0].As<string>();
            var node = values[1].As<XmlNode>();

            var res = new ObservableCollection<string>();

            foreach (var n in node.ChildNodes)
            {
                var e = n.As<XmlElement>();
                if (e != null && e.LocalName == "value-of" && e.Attributes["select"] != null)
                    res.Add(path + "/" + e.Attributes["select"].Value);
            }
            return res;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
