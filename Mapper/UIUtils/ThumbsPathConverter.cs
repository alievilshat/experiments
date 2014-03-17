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
            var thumbs = new Queue<Thumb>(values.Skip(1).OfType<Thumb>());
            if (thumbs.Any(i => !i.IsVisible))
                return null;
            var type = new Queue<char>(parameter.ToString().ToCharArray());

            PathFigure pathFigure = new PathFigure();
            pathFigure.StartPoint = getPoint(canvas, thumbs.Dequeue(), '*');
            while(thumbs.Count > 0)
            {
                pathFigure.Segments.Add(new LineSegment { Point = getPoint(canvas, thumbs.Dequeue(), type.Dequeue()) });
            }

            PathGeometry pathGeometry = new PathGeometry();
            pathGeometry.Figures = new PathFigureCollection();
            pathGeometry.Figures.Add(pathFigure);
            return pathGeometry;
        }

        Point Z = new Point(1, 1);
        Point previousPoint;
        private Point getPoint(Canvas canvas, Thumb thumb, char t)
        {
            var p = thumb.TranslatePoint(Z, canvas);

            switch (t)
            {
                case '-':
                    previousPoint = new Point(p.X, previousPoint.Y);
                    break;

                case '*':
                default:
                    previousPoint = p;
                    break;
            }
            return previousPoint;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
