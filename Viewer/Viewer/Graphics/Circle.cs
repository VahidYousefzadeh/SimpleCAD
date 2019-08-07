using System;
using System.Windows;
using System.Windows.Media;
using Viewer.Geometry;

namespace Viewer.Graphics
{
    public sealed class Circle : Shape
    {
        private bool m_filled;

        public bool Filled
        {
            get => m_filled;
            set
            {
                m_filled = value;
                IsDirty = true;
            }
        }

        /// <summary>
        /// Initializes an instance of <see cref="Circle"/> class.
        /// </summary>
        public Circle(Point center, double radius)
        {
            Geometry = new CircleGeometry(center, radius);

            InvalidateVisual();
        }

        protected override void Render(DrawingContext drawingContext)
        {
            if (drawingContext == null) return;

            CircleGeometry circleGeometry = (CircleGeometry) Geometry;
            drawingContext.DrawEllipse(
                m_filled ? Brush() : null,
                Pen(),
                circleGeometry.Center,
                circleGeometry.Radius,
                circleGeometry.Radius);
        }

        public override T Write<T>(IWriter<T> writer)
        {
            if (writer == null)
                throw new ArgumentNullException(nameof(writer));

            CircleGeometry circleGeometry = (CircleGeometry) Geometry;
            return writer.WriteCircle(circleGeometry.Center, circleGeometry.Radius, Color, LineStyle, m_filled);
        }
    }
}