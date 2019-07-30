using System.Globalization;
using System.Windows;
using System.Windows.Media;

namespace Viewer
{
    public abstract class ShapeDefinition
    {
        public abstract string Type { get; }

        public string Color { get; set; }

        public string LineType { get; set; }

        public abstract Shape Convert();

        protected Pen Pen()
        {
            return Utility.Freeze(new Pen(Brush(GetColor()), 1d) {DashStyle = DashStyle()});

            Color GetColor()
            {
                string[] tokens = Color.Split(';');
                return byte.TryParse(tokens[0], out byte a) &&
                       byte.TryParse(tokens[1], out byte r) &&
                       byte.TryParse(tokens[2], out byte g) &&
                       byte.TryParse(tokens[3], out byte b)
                    ? Argb(a, r, g, b)
                    : Colors.Black;
            }

            DashStyle DashStyle()
            {
                return JsonDashStyleHelper.FromJson(LineType);
            }
        }

        protected Point Point(string coordinates)
        {
            string[] tokens = coordinates.Split(';');

            return double.TryParse(tokens[0], NumberStyle(), CultureInfo(), out double x) &&
                   double.TryParse(tokens[1], NumberStyle(), CultureInfo(), out double y)
                ? new Point(x, y)
                : default(Point);

            NumberStyles NumberStyle()
            {
                return NumberStyles.Float;
            }

            CultureInfo CultureInfo()
            {
                return new CultureInfo("de");
            }
        }

        private static Brush Brush(Color color)
        {
            return Utility.Freeze(new SolidColorBrush(color));
        }

        private static Color Argb(byte a, byte r, byte g, byte b)
        {
            return System.Windows.Media.Color.FromArgb(a, r, g, b);
        }


    }
}