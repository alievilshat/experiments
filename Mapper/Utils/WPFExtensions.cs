using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Mapper
{
    public static class WPFExtensions
    {
        public static T FindAncestor<T>(this DependencyObject dependencyObject)
            where T : class
        {
            DependencyObject target = dependencyObject;
            do
            {
                target = VisualTreeHelper.GetParent(target);
            }
            while (target != null && !(target is T));
            return target as T;
        }

        public static IEnumerable<DependencyObject> GetChildren(this DependencyObject node)
        {
            yield return node;
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(node); i++)
            {
                foreach (var r in VisualTreeHelper.GetChild(node, i).GetChildren()) yield return r;
            }
        }
    }
}
