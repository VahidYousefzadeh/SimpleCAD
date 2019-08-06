using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace Viewer.Graphics
{
    public sealed class Intersections : FrameworkElement
    {
        private readonly VisualCollection m_children;

        public Intersections(View view, double scale)
        {
            m_children = new VisualCollection(this);

            var sw = new Stopwatch();
            sw.Start();
            Point[] intersections = FindIntersections(view.Shapes);
            sw.Stop();
            MessageBox.Show(sw.ElapsedMilliseconds.ToString());

            if (intersections != null)
            {
                m_children.Add(new CrossSymbols(scale, intersections));
            }
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

        private static Point[] FindIntersections(IList<Shape> shapes)
        {
            //var f = shapes.Select(o => o.Geometry);

            //var dd = IntersectionHelper.Intersections(f.ToArray());

            Point[] intersections = null;
            for (int i = 0; i < shapes.Count - 1; i++)
            {
                Geometry.Geometry a = shapes[i].Geometry;
                for (int j = i + 1; j < shapes.Count; j++)
                {
                    Geometry.Geometry b = shapes[j].Geometry;
                    intersections = intersections == null
                        ? a.Intersect(b)
                        : intersections.Union(a.Intersect(b)).ToArray();
                }
            }

            return intersections;
        }
    }
}