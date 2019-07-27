using System.Windows;

namespace Viewer
{
    internal abstract class Geometry
    {
        internal abstract Point[] Intersect(Geometry other);
    }
}