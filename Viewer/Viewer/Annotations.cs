using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace Viewer
{
    public sealed class Annotations : FrameworkElement
    {
        private readonly VisualCollection m_children;

        private static readonly Pen s_pen = new Pen(Brushes.Orange, 2d);

        public Annotations(View view)
        {
            m_children = new VisualCollection(this);


            var sw = new Stopwatch();
            sw.Start();
            Point[] intersections = FindIntersections(view.Shapes);
            sw.Stop();
            MessageBox.Show(sw.ElapsedMilliseconds.ToString());


            Stopwatch s = new Stopwatch();
            s.Start();


            if (intersections != null)
            {
                m_children.Add(new CrossSymbols(s_pen, intersections));
            }

            s.Stop();
            MessageBox.Show(s.ElapsedMilliseconds.ToString());
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
                Geometry a = shapes[i].Geometry;
                for (int j = i + 1; j < shapes.Count; j++)
                {
                    Geometry b = shapes[j].Geometry;
                    intersections = intersections == null
                        ? a.Intersect(b)
                        : intersections.Union(a.Intersect(b)).ToArray();
                }
            }

            return intersections;
        }
    }
}