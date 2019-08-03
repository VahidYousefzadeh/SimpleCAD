using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
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

            if (shapes == null) return;

            foreach (Shape shape in shapes)
                m_children.Add(shape);
        }

        public Rect Bounds()
        {
            return Shapes.Aggregate(Rect.Empty, (current, shape) => Rect.Union(current, shape.Geometry.Bounds));
        }

        public string ToJson(IFormatProvider provider)
        {
            return $"[\n{string.Join(",\n", Shapes.Select(o => o.ToJson(provider)))}\n]";
        }

        public string ToXml(IFormatProvider provider)
        {
            return "";
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