using System;
using System.Windows;
using System.Windows.Media;
using System.Xml.Linq;

namespace Viewer
{
    internal sealed class Line : Shape
    {
        /// <summary>
        /// Initializes an instance of <see cref="Line"/> class.
        /// </summary>
        public Line(Point startPoint, Point endPoint)
        {
            Geometry = new LineGeometry(startPoint, endPoint);

            InvalidateVisual();
        }

        protected override void Render(DrawingContext drawingContext)
        {
            var lineGeometry = (LineGeometry) Geometry;

            drawingContext.DrawLine(Pen(), lineGeometry.StartPoint, lineGeometry.EndPoint);
        }

        protected override string ToJsonInternal(IFormatProvider provider)
        {
            return $"\"type\": \"line\",\n" +
                   $"{Geometry.ToJson(provider)}";
        }

        protected override XElement[] ToXmlInternal(IFormatProvider provider)
        {
            return new[]
            {
                new XElement("type", "line"),
                Geometry.ToXml(provider)
            };
        }
    }
}