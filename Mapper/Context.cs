using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Mapper
{
    public class Context
    {
        public static string GetSourceContext(DependencyObject obj)
        {
            return (string)obj.GetValue(SourceContextProperty);
        }
        public static void SetSourceContext(DependencyObject obj, string value)
        {
            obj.SetValue(SourceContextProperty, value);
        }
        public static readonly DependencyProperty SourceContextProperty =
            DependencyProperty.RegisterAttached("SourceContext", typeof(string), typeof(Context), new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.Inherits));



        public static string GetTargetContext(DependencyObject obj)
        {
            return (string)obj.GetValue(TargetContextProperty);
        }
        public static void SetTargetContext(DependencyObject obj, string value)
        {
            obj.SetValue(TargetContextProperty, value);
        }
        public static readonly DependencyProperty TargetContextProperty =
            DependencyProperty.RegisterAttached("TargetContext", typeof(string), typeof(Context), new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.Inherits));
    }
}
