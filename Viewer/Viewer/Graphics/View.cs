using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace Viewer.Graphics
{
    public sealed class View : FrameworkElement
    {
        private readonly VisualCollection m_children;

        public Shape[] Shapes => m_children.OfType<Shape>().ToArray();

        public View(IEnumerable<Shape> shapes = null)
        {
            m_children = new VisualCollection(this);

            if (shapes == null) return;

            foreach (Shape shape in shapes)
                m_children.Add(shape);
        }

        public Rect Bounds()
        {
            return Shapes.Aggregate(Rect.Empty, (current, shape) => Rect.Union(current, shape.Geometry.Bounds));
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
    }
}