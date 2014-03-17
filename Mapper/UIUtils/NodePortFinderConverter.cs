using System;
using System.Globalization;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Xml.Schema;

namespace Mapper
{
    class NodePortFinderConverter : IValueConverter
    {
        public SchemaControl Schema { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return getNodeLocation(value.As<string>());
        }

        private Thumb getNodeLocation(string path)
        {
            if (Schema == null || path == null)
                return null;

            var parts = path.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);

            var node = Schema.GetChildrenBFS().OfType<TreeViewItem>().Skip(1).First();
            foreach (var p in parts)
                node = node.GetChildrenBFS().OfType<TreeViewItem>()
                    .First(i => string.Compare(i.DataContext.As<XmlSchemaElement>().Name, p, true) == 0);

            if (!node.IsVisible)
                return null;

            return node.GetChildren().OfType<Thumb>().FirstOrDefault();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
