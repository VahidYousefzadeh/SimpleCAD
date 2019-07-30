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

        public override string ToString()
        {
            return $"Type: \t\t {GetType().Name} \n" +
                   $"Color: \t\t {Pen.Brush} \n" +
                   $"LineType: \t\t {JsonDashStyleHelper.ToJson(Pen.DashStyle).ToUpper()}";
        }
    }
}