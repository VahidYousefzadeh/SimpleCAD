using System;
using System.Globalization;
using System.Windows;
using System.Windows.Media;
using Newtonsoft.Json;

namespace Viewer
{
    public abstract class ShapeDefinition
    {
        [JsonProperty(PropertyName = "type")]
        public abstract string Type { get; }

        [JsonProperty(PropertyName = "color")]
        public string Color { get; set; }

        [JsonProperty(PropertyName = "lineType")]
        public string LineType { get; set; }

        public abstract Shape Convert();

        protected Pen Pen()
        {
            return new Pen(Brush(GetColor()), 1d) {DashStyle = DashStyle()};

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
                switch (LineType)
                {
                    case "dash":
                        return DashStyles.Dash;
                    case "dot":
                        return DashStyles.Dot;
                    case "dashDot":
                        return DashStyles.DashDot;
                    default:
                        return DashStyles.Solid;
                }
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

        protected Brush RandomBrush()
        {
            var rand = new Random();

            return Brush(Argb((byte)rand.Next(0, 256),
                (byte)rand.Next(0, 256),
                (byte)rand.Next(0, 256),
                (byte)rand.Next(0, 256)));
        }

        private static Brush Brush(Color color)
        {
            var brush = new SolidColorBrush(color);

            if (brush.CanFreeze)
                brush.Freeze();

            return brush;
        }

        private static Color Argb(byte a, byte r, byte g, byte b)
        {
            return System.Windows.Media.Color.FromArgb(a, r, g, b);
        }
    }
}