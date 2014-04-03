using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Media;

namespace Mapper
{
    class ThumbsPathConverter : IMultiValueConverter
    {
        // values:
        // [0] - canvas
        // [1] - start point
        // [i] - intermediate point
        // [n] - end point
        // parameter:
        // - - horizontal line
        // * - point to point
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null || values.Length < 3)
                return null;

            var canvas = (Canvas)values[0];
            var thumbs = values.Skip(1).Select(i => i.As<Thumb>()).ToArray();
            var points = new LinkedList<Point>();

            if (thumbs.Any(i => i == null || !i.IsVisible))
                return null;

            var first = points.AddFirst(getPoint(thumbs[0], canvas));
            var last = points.AddLast(getPoint(thumbs[thumbs.Length - 1], canvas));

            switch ((string)parameter)
            {
                case "-*":
                    points.AddAfter(first, new Point(getPoint(thumbs[1], canvas).X, first.Value.Y));
                    break;
                case "*-":
                    points.AddBefore(last, new Point(getPoint(thumbs[1], canvas).X, last.Value.Y));
                    break;
                case "-*-":
                    points.AddAfter(first, new Point(getPoint(thumbs[1], canvas).X, first.Value.Y));
                    points.AddBefore(last, new Point(getPoint(thumbs[2], canvas).X, last.Value.Y));
                    break;
                default:
                    throw new NotSupportedException("Parameter " + parameter + " is not supported");
            }
            

            PathFigure pathFigure = new PathFigure();
            pathFigure.StartPoint = points.First();

            foreach (var p in points.Skip(1))
            {
                pathFigure.Segments.Add(new LineSegment { Point = p });
            }

            PathGeometry pathGeometry = new PathGeometry();
            pathGeometry.Figures = new PathFigureCollection();
            pathGeometry.Figures.Add(pathFigure);
            return pathGeometry;
        }

        private Point getPoint(Thumb thumb, Canvas canvas)
        {
            Point Z = new Point(1, 1);
            return thumb.TranslatePoint(Z, canvas);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
