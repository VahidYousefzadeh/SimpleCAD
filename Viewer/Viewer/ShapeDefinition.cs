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
        }

        protected Point Point(string coordinates)
        {
            string[] tokens = coordinates.Split(';');

            var culture = new CultureInfo("de");
            if (double.TryParse(tokens[0], NumberStyles.Float, culture, out double x) &&
                double.TryParse(tokens[1], NumberStyles.Float, culture, out double y))
            {
                return new Point(x, y);
            }

            return default(Point);
        }

        protected Brush RandomBrush()
        {
            var rand = new Random();

            return Brush(Argb((byte)rand.Next(0, 256),
                (byte)rand.Next(0, 256),
                (byte)rand.Next(0, 256),
                (byte)rand.Next(0, 256)));
        }

        private Brush Brush(Color color)
        {
            var brush = new SolidColorBrush(color);

            if (brush.CanFreeze)
                brush.Freeze();

            return brush;
        }

        private DashStyle DashStyle()
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

        private Color GetColor()
        {
            string[] tokens = Color.Split(';');
            return byte.TryParse(tokens[0], out byte a) &&
                   byte.TryParse(tokens[1], out byte r) &&
                   byte.TryParse(tokens[2], out byte g) &&
                   byte.TryParse(tokens[3], out byte b)
                ? Argb(a, r, g, b)
                : Colors.Black;
        }

        private static Color Argb(byte a, byte r, byte g, byte b)
        {
            return System.Windows.Media.Color.FromArgb(a, r, g, b);
        }
    }
}