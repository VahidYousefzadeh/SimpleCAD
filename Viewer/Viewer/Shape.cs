using System.Windows.Media;

namespace Viewer
{
    internal abstract class Shape : DrawingVisual
    {
        public Geometry Geometry { get; protected set; }
    }
}