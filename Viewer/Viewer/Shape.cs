using System.Windows.Media;

namespace Viewer
{
    public abstract class Shape : DrawingVisual
    {
        public Geometry Geometry { get; protected set; }

        public Pen Pen { get; }

        protected Shape(Pen pen)
        {
            Pen = pen;
        }
    }
}