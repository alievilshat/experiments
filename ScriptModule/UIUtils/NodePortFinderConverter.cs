using System;
using System.Globalization;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Xml.Schema;

namespace Mapper
{
    class NodePortFinderConverter : IMultiValueConverter
    {
        public SchemaControl Schema { get; set; }

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return getNodeLocation((string)values.FirstOrDefault());
        }

        private Thumb getNodeLocation(string path)
        {
            if (Schema == null || path == null)
                return null;

            var parts = path.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);

            var node = Schema.GetChildrenBFS().OfType<TreeViewItem>().FirstOrDefault();
            if (node == null)
                return null;

            foreach (var p in parts)
            {
                node = node.GetChildrenBFS().OfType<TreeViewItem>()
                    .FirstOrDefault(i => string.Compare(i.DataContext.As<XmlSchemaElement>().Name, p, true) == 0);

                if (node == null)
                    return null;
            }

            if (!node.IsVisible)
                return null;

            return node.GetChildren().OfType<Thumb>().FirstOrDefault();
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
