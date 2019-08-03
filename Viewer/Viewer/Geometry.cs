using System;
using System.Windows;
using System.Xml.Linq;

namespace Viewer
{
    public abstract class Geometry
    {
        public abstract Point[] Intersect(Geometry other);

        public abstract Rect Bounds { get; }

        public abstract string ToJson(IFormatProvider provider);

        public abstract XElement ToXml(IFormatProvider provider);
    }
}