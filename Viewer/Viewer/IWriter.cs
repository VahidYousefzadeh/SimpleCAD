using System.Windows;
using System.Windows.Media;

namespace Viewer
{
    public interface IWriter<out T>
    {
        T WriteLine(Point a, Point b, Color color, DashStyle dashStyle);
        T WriteCircle(Point center, double radius, Color color, DashStyle dashStyle, bool filled);
        T WriteTriangle(Point a, Point b, Point c, Color color, DashStyle dashStyle, bool filled);
        T WriteShapes(Shape[] shapes);
    }
}