using System.Windows;
using System.Windows.Media;

namespace Viewer
{
    public sealed class Circle : Shape
    {
        private readonly bool m_filled;

        /// <summary>
        /// Initializes an instance of <see cref="Circle"/> class.
        /// </summary>
        public Circle(Point center, double radius, bool filled)
        {
            Geometry = new CircleGeometry(center, radius);

            m_filled = filled;

            InvalidateVisual();
        }

        protected override void Render(DrawingContext drawingContext)
        {
            var circleGeometry = (CircleGeometry) Geometry;
            drawingContext.DrawEllipse(
                m_filled ? Brush() : null,
                Pen(),
                circleGeometry.Center,
                circleGeometry.Radius,
                circleGeometry.Radius);
        }

        public override T Write<T>(IWriter<T> writer)
        {
            var circleGeometry = (CircleGeometry) Geometry;
            return writer.WriteCircle(circleGeometry.Center, circleGeometry.Radius, Color, LineStyle, m_filled);
        }
    }
}