using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Viewer
{
    public sealed class View : FrameworkElement
    {
        private readonly VisualCollection m_children;

        public IList<Shape> Shapes => m_children.OfType<Shape>().ToList();


        public View(IEnumerable<Shape> shapes = null)
        {
            m_children = new VisualCollection(this);

            if (shapes != null)
            {
                foreach (Shape shape in shapes) AddShape(shape);
            }

            MouseLeftButtonUp += OnMouseLeftButtonUp;
        }

        private void OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Point pt = e.GetPosition((UIElement)sender);

            VisualTreeHelper.HitTest(this, null, HitTestCallback, new PointHitTestParameters(pt));
        }

        private static HitTestResultBehavior HitTestCallback(HitTestResult result)
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

        public void AddShape(Shape shape)
        {
            m_children.Add(shape);
        }
    }
}