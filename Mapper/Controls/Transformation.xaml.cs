using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Xml.Schema;

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

        private static Point EMPTY = new Point();

        private void transformation_Loaded(object sender, RoutedEventArgs e)
        {
            Source.LayoutUpdated += (o, a) => updatePath();
            Target.LayoutUpdated += (o, a) => updatePath();
            updatePath();
        }

        private Point? prevStartPoint = EMPTY;
        private Point? prevEndPoint = EMPTY;

        private void updatePath()
        {
            var startPoint = getNodeLocation(Source, SourcePath);
            var endPoint = getNodeLocation(Target, TargetPath);

            if (!startPoint.HasValue || !endPoint.HasValue)
            {
                PathCoordinates = null;
                prevStartPoint = null;
                prevEndPoint = null;
                return;
            }

            if (prevStartPoint == startPoint && prevEndPoint == endPoint)
                return;

            prevStartPoint = startPoint.Value;
            prevEndPoint = endPoint.Value;

            PathFigure pathFigure = new PathFigure();
            pathFigure.StartPoint = startPoint.Value;
            pathFigure.Segments.Add(new LineSegment { Point = new Point(getThumbLocation(Source).X, startPoint.Value.Y) });
            pathFigure.Segments.Add(new LineSegment { Point = new Point(getThumbLocation(Target).X, endPoint.Value.Y) });
            pathFigure.Segments.Add(new LineSegment { Point = endPoint.Value });

            PathGeometry pathGeometry = new PathGeometry();
            pathGeometry.Figures = new PathFigureCollection();
            pathGeometry.Figures.Add(pathFigure);
            PathCoordinates = pathGeometry;
        }

        private Point? getNodeLocation(SchemaControl control, string path)
        {
            if (control == null)
                return EMPTY;

            var parts = path.Split(new[] {'/'}, StringSplitOptions.RemoveEmptyEntries);

            var node = control.GetChildrenBFS().OfType<TreeViewItem>().Skip(1).First();
            foreach (var p in parts)
                node = node.GetChildrenBFS().OfType<TreeViewItem>().First(i => string.Compare(i.DataContext.As<XmlSchemaElement>().Name, p, true) == 0);

            if (!node.IsVisible)
                return null;

            return getThumbLocation(node);
        }

        private Point getThumbLocation(FrameworkElement node)
        {
            var thumb = node.GetChildren().OfType<Thumb>().First();
            var transformer = thumb.TransformToVisual(canvas);
            var res = transformer.Transform(new Point(0, 0));
            return res;
        }


        public SchemaControl Source
        {
            get { return (SchemaControl)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }
        public static readonly DependencyProperty SourceProperty = DependencyProperty.Register("Source", typeof(SchemaControl), typeof(Transformation), null);


        public string SourcePath
        {
            get { return (string)GetValue(SourcePathProperty); }
            set { SetValue(SourcePathProperty, value); }
        }
        public static readonly DependencyProperty SourcePathProperty =
            DependencyProperty.Register("SourcePath", typeof(string), typeof(Transformation), null);


        public SchemaControl Target
        {
            get { return (SchemaControl)GetValue(TargetProperty); }
            set { SetValue(TargetProperty, value); }
        }
        public static readonly DependencyProperty TargetProperty = DependencyProperty.Register("Target", typeof(SchemaControl), typeof(Transformation), null);


        public string TargetPath
        {
            get { return (string)GetValue(TargetPathProperty); }
            set { SetValue(TargetPathProperty, value); }
        }
        public static readonly DependencyProperty TargetPathProperty =
            DependencyProperty.Register("TargetPath", typeof(string), typeof(Transformation), null);


        public PathGeometry PathCoordinates
        {
            get { return (PathGeometry)GetValue(PathCoordinatesProperty); }
            set { SetValue(PathCoordinatesProperty, value); }
        }
        public static readonly DependencyProperty PathCoordinatesProperty =
            DependencyProperty.Register("PathCoordinates", typeof(PathGeometry), typeof(Transformation), null);
    }
}
