using System;
using System.Windows;
using System.Windows.Media;
using System.Xml.Linq;

namespace Viewer
{
    public sealed class Triangle : Polygon
    {
        /// <summary>
        /// Initializes an instance of <see cref="Triangle"/> class.
        /// </summary>
        public Triangle(Brush fill, Point a, Point b, Point c)
            : base(fill, a, b, c)
        {
        }

        protected override string ToJsonInternal(IFormatProvider provider)
        {
            return $"\"type\": \"triangle\",\n" +
                   $"{Geometry.ToJson(provider)},\n" +
                   $"\"filled\": false";
        }

        protected override XElement[] ToXmlInternal(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }
    }
}