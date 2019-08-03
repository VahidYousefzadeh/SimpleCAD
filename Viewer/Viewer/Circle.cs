using System;
using System.Windows;
using System.Windows.Media;
using System.Xml.Linq;

namespace Viewer
{
    internal sealed class Circle : Shape
    {
        /// <summary>
        /// Initializes an instance of <see cref="Circle"/> class.
        /// </summary>
        public Circle(Point center, double radius, bool filled)
        {
            Geometry = new CircleGeometry(center, radius);

            InvalidateVisual();
        }

        protected override void Render(DrawingContext drawingContext)
        {
            var circleGeometry = (CircleGeometry) Geometry;
            drawingContext.DrawEllipse(
                null,
                Pen(),
                circleGeometry.Center,
                circleGeometry.Radius,
                circleGeometry.Radius);
        }

        protected override string ToJsonInternal(IFormatProvider provider)
        {
            return $"\"type\": \"circle\",\n" +
                   $"{Geometry.ToJson(provider)},\n" +
                   $"\"filled\": false";
        }

        protected override XElement[] ToXmlInternal(IFormatProvider provider)
        {
            return new[]
            {
                new XElement("type", "circle"),
                Geometry.ToXml(provider),
                new XElement("filled", false)
            };
        }
    }
}