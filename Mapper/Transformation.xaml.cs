using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Mapper
{
    /// <summary>
    /// Interaction logic for Transformation.xaml
    /// </summary>
    public partial class Transformation : UserControl
    {
        public Transformation()
        {
            InitializeComponent();
        }

        public TreeViewItem Source
        {
            get { return (TreeViewItem)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }
        public static readonly DependencyProperty SourceProperty = DependencyProperty.Register("Source", typeof(TreeViewItem), typeof(Transformation), null);

        public TreeViewItem Target
        {
            get { return (TreeViewItem)GetValue(TargetProperty); }
            set { SetValue(TargetProperty, value); }
        }
        public static readonly DependencyProperty TargetProperty = DependencyProperty.Register("Target", typeof(TreeViewItem), typeof(Transformation), null);


        private static Point EMPTY = new Point();
        public Point StartPoint
        {
            get { return (Point)GetValue(StartPointProperty); }
            set { SetValue(StartPointProperty, value); }
        }
        public static DependencyProperty StartPointProperty = DependencyProperty.Register("StartPoint", typeof(Point), typeof(MainWindow), new PropertyMetadata(EMPTY));

        public Point EndPoint
        {
            get { return (Point)GetValue(EndPointProperty); }
            set { SetValue(EndPointProperty, value); }
        }
        public static DependencyProperty EndPointProperty = DependencyProperty.Register("EndPoint", typeof(Point), typeof(MainWindow), new PropertyMetadata(EMPTY));

        private void transformation_Loaded(object sender, RoutedEventArgs e)
        {
            //Source.LayoutUpdated += (o, a) => StartPoint = getLocation(Source);
            //Target.LayoutUpdated += (o, a) => EndPoint = getLocation(Target);
            StartPoint = new Point(10, 10);
            EndPoint = new Point(100, 100);
        }

        private Point getLocation(TreeViewItem element)
        {
            if (element == null)
                return EMPTY;

            var c = (FrameworkElement)element.FindAncestor<ItemsControl>().ItemContainerGenerator.ContainerFromItem(element);
            Thumb anchor = getAnchor(c);
            var transformer = canvas.TransformToVisual(anchor);
            var res = transformer.Transform(new Point(0, 0));
            return res;
        }

        Dictionary<FrameworkElement, Thumb> thumbcache = new Dictionary<FrameworkElement, Thumb>();
        private Thumb getAnchor(FrameworkElement treeViewItem)
        {
            Thumb res;
            if (thumbcache.TryGetValue(treeViewItem, out res))
                return res;

            var thumb = findThumb(treeViewItem);
            thumbcache.Add(treeViewItem, thumb);
            return thumb;
        }

        private Thumb findThumb(FrameworkElement element)
        {
            if (element.GetType() == typeof(Thumb))
                return (Thumb)element;

            foreach (var e in LogicalTreeHelper.GetChildren(element).OfType<FrameworkElement>())
            {
                var r = findThumb(e);
                if (r != null)
                    return r;
            }

            return null;
        }
    }
}
