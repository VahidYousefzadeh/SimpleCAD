using System.Windows;
using System.Windows.Media;

namespace Viewer
{
    public interface IWriter<out T>
    {
        T WriteLine(Point startPoint, Point endPoint, Color color, DashStyle dashStyle);
        T WriteCircle(Point center, double radius, Color color, DashStyle dashStyle, bool filled);
        T WriteTriangle(Point firstCorner, Point secondCorner, Point thirdCorner, Color color, DashStyle dashStyle, bool filled);
        T WriteRectangle(Point firstCorner, Point secondCorner, Color color, DashStyle dashStyle, bool filled);
    }
}