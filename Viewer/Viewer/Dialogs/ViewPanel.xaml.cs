using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Viewer.Graphics;

namespace Viewer.Dialogs
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
            nameof(Scale), typeof(double), typeof(ViewPanel), new PropertyMetadata(1.0));

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
            ((ViewPanel)d).ZoomToExtents();

            ((ViewPanel)d).Refresh();

            ((ViewPanel)d).Annotations.Content = new Annotations(((ViewPanel)d).View, ((ViewPanel)d).Scale);
        }

        public ViewPanel()
        {
            InitializeComponent();
        }

        private void Refresh()
        {
            foreach (Shape shape in View.Shapes)
            {
                shape.Thickness = 2d / Scale;
                shape.InvalidateVisual();
            }
        }

        private void ZoomToExtents()
        {
            Rect bounds = View.Bounds();

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

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.None;
            Point pt = e.GetPosition((UIElement)sender);
            PickCursor.Visibility = Visibility.Visible;

            Canvas.SetBottom(PickCursor, pt.Y);
            Canvas.SetLeft(PickCursor, pt.X);
        }

        private void OnMouseLeave(object sender, MouseEventArgs e)
        {
            PickCursor.Visibility = Visibility.Collapsed;
        }

        private void OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Point pt = e.GetPosition((UIElement)sender);

            var geom = new RectangleGeometry(new Rect(pt.X, pt.Y, PickCursor.Width, PickCursor.Height));

            VisualTreeHelper.HitTest(this, HitTestFilterCallback, HitTestCallback, new GeometryHitTestParameters(geom));
        }

        private static HitTestFilterBehavior HitTestFilterCallback(DependencyObject potentialHitTestTarget)
        {
            return potentialHitTestTarget is Shape 
                ? HitTestFilterBehavior.Continue 
                : HitTestFilterBehavior.ContinueSkipSelf;
        }

        private HitTestResultBehavior HitTestCallback(HitTestResult result)
        {
            if (result.VisualHit is Shape shape)
            {
                shape.Opacity = 0.4;
                MessageBox.Show($"{shape}");
                shape.Opacity = 1.0;
            }

            // Stop the hit test enumeration of objects in the visual tree.
            return HitTestResultBehavior.Stop;
        }
    }
}
