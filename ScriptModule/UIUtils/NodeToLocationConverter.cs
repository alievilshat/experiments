using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace Mapper
{
    [ValueConversion(typeof(FrameworkElement), typeof(Point?))]   
    public class NodeToLocationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            var container = getContainer((ItemsControl)value);
            var anchor = getCanvas(container);

            var transformer = container.TransformToAncestor(anchor);
            return transformer.Transform(new Point(0, 0));
        }

        private FrameworkElement getContainer(ItemsControl v)
        {
            var stack = new Stack<ItemsControl>();
            do
            {
                stack.Push(v);
                v = (ItemsControl)LogicalTreeHelper.GetParent(v);
            }
            while (v.GetType() != typeof(TreeView));

            while (stack.Count > 0)
            {
                var e = stack.Pop();
                v = (ItemsControl)v.ItemContainerGenerator.ContainerFromItem(e);
            }
            return v;
        }

        private Canvas _canvas = null;
        private FrameworkElement getCanvas(FrameworkElement v)
        {
            if (_canvas == null)
            {
                _canvas = v.FindAncestor<Canvas>();
            }
            return _canvas;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
