using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using Viewer.Geometry;

namespace Viewer.Graphics
{
    public sealed class Intersections : FrameworkElement
    {
        private readonly VisualCollection m_children;

        public Intersections(View view, double scale)
        {
            if (view == null)
                throw new ArgumentNullException(nameof(view));

            m_children = new VisualCollection(this);

            Point[] intersections = FindIntersections(view.Shapes());

            m_children.Add(new CrossSymbols(scale, intersections));
        }

        protected override int VisualChildrenCount => m_children.Count;

        protected override Visual GetVisualChild(int index)
        {
            if (index < 0 || index >= m_children.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            return m_children[index];
        }

        private static Point[] FindIntersections(IList<Shape> shapes)
        {
            Point[] intersections = null;
            for (int i = 0; i < shapes.Count - 1; i++)
            {
                ShapeGeometry a = shapes[i].Geometry;
                for (int j = i + 1; j < shapes.Count; j++)
                {
                    ShapeGeometry b = shapes[j].Geometry;
                    intersections = intersections == null
                        ? a.Intersect(b)
                        : intersections.Union(a.Intersect(b)).ToArray();
                }
            }
            return intersections == null
                ? new Point[0]
                : intersections.Distinct(new PointComparer()).ToArray();
        }
    }
}