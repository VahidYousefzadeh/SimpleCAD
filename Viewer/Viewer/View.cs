using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Viewer
{
    public sealed class View : FrameworkElement
    {
        private readonly VisualCollection m_children;

        public View()
        {
            m_children = new VisualCollection(this);

            MouseLeftButtonUp += OnMouseLeftButtonUp;
        }

        // Capture the mouse event and hit test the coordinate point value against
        // the child visual objects.
        private void OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            // Retreive the coordinates of the mouse button event.
            Point pt = e.GetPosition((UIElement)sender);

            // Initiate the hit test by setting up a hit test result callback method.
            VisualTreeHelper.HitTest(this, null, HitTestCallback, new PointHitTestParameters(pt));
        }

        // If a child visual object is hit, toggle its opacity to visually indicate a hit.
        public HitTestResultBehavior HitTestCallback(HitTestResult result)
        {
            if (result.VisualHit is DrawingVisual visual)
            {
                visual.Opacity = visual.Opacity >= 1.0 ? 0.4 : 1.0;
            }

            // Stop the hit test enumeration of objects in the visual tree.
            return HitTestResultBehavior.Stop;
        }

        protected override int VisualChildrenCount => m_children.Count;

        protected override Visual GetVisualChild(int index)
        {
            if (index < 0 || index >= m_children.Count)
            {
                throw new ArgumentOutOfRangeException();
            }

            return m_children[index];
        }

        public void DrawLine(Pen pen, Point startPoint, Point endPoint)
        {
            m_children.Add(new LineVisual(pen, startPoint, endPoint));
        }

        public void DrawCircle(Brush fill, Pen pen, Point center, double radius)
        {
            m_children.Add(new CircleVisual(fill, pen, center, radius));
        }

        public void DrawTriangle(Brush fill, Pen pen, Point a, Point b, Point c)
        {
            m_children.Add(new TriangleVisual(fill, pen, a, b, c));
        }

        public void Clear()
        {
            m_children.Clear();
        }
    }
}