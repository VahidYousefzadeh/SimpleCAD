using System;
using System.Linq;
using System.Windows;

namespace Viewer
{
    /// <summary>
    /// Interaction logic for ViewPanel.xaml
    /// </summary>
    public partial class ViewPanel
    {
        public View View
        {
            get => (View)GetValue(s_viewProperty);
            set => SetValue(s_viewProperty, value);
        }

        public static readonly DependencyProperty s_viewProperty = DependencyProperty.Register(
            nameof(View), typeof(View), typeof(ViewPanel), new PropertyMetadata(OnViewPropertyChanged));

        public double Scale
        {
            get => (double)GetValue(s_scaleProperty);
            set => SetValue(s_scaleProperty, value);
        }

        public static readonly DependencyProperty s_scaleProperty = DependencyProperty.Register(
            nameof(Scale), typeof(double), typeof(ViewPanel), null);

        public double Dx
        {
            get => (double)GetValue(s_dxProperty);
            set => SetValue(s_dxProperty, value);
        }

        public static readonly DependencyProperty s_dxProperty = DependencyProperty.Register(
            nameof(Dx), typeof(double), typeof(ViewPanel), null);

        public double Dy
        {
            get => (double)GetValue(s_dyProperty);
            set => SetValue(s_dyProperty, value);
        }

        public static readonly DependencyProperty s_dyProperty = DependencyProperty.Register(
            nameof(Dy), typeof(double), typeof(ViewPanel), null);

        private static void OnViewPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ViewPanel) d).Annotations.Content = new Annotations(((ViewPanel) d).View);

            ((ViewPanel)d).ZoomToExtents();
        }

        public ViewPanel()
        {
            InitializeComponent();
        }

        private void ZoomToExtents()
        {
            Rect bounds = View.Shapes.Aggregate(Rect.Empty, (current, shape) => Rect.Union(current, shape.Geometry.Bounds));

            double xmax = bounds.Right;
            double xmin = bounds.Left;
            double ymax = Math.Max(bounds.Bottom, bounds.Top);
            double ymin = Math.Min(bounds.Bottom, bounds.Top);

            double xc = 0.5 * (xmin + xmax);
            double yc = 0.5 * (ymin + ymax);

            Scale = Math.Min(ActualWidth / (xmax - xmin), ActualHeight / (ymax - ymin)) * 0.9;
            Dx = -xc * Scale + 0.5 * ActualWidth;
            Dy = -yc * Scale + 0.5 * ActualHeight;
        }
    }
}
