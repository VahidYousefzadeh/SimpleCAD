using System;
using System.Windows;
using System.Windows.Media;

namespace Viewer
{
    public sealed class Annotations : FrameworkElement
    {
        private readonly VisualCollection m_children;

        public Annotations(View view)
        {
            m_children = new VisualCollection(this);

            m_children.Add(new Marker(new Pen(Brushes.Blue, 5.0), new Point(500, 500)));
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