using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Mapper
{
    /// <summary>
    /// Interaction logic for Transformation.xaml
    /// </summary>
    public partial class Connector : UserControl
    {
        public Connector()
        {
            InitializeComponent();
        }

        public PathGeometry Geometry
        {
            get { return (PathGeometry)GetValue(GeometryProperty); }
            set { SetValue(GeometryProperty, value); }
        }
        public static readonly DependencyProperty GeometryProperty =
            DependencyProperty.Register("Geometry", typeof(PathGeometry), typeof(Connector), null);
    }
}
