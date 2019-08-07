using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Media;
using Viewer.Graphics;

namespace Viewer.Reader
{
    public abstract class ShapeReader
    {
        private readonly IFormatProvider m_formatProvider;

        protected ShapeReader(IFormatProvider formatProvider)
        {
            m_formatProvider = formatProvider;
        }

        public abstract IEnumerable<Shape> Read(string fileName);

        protected double Double(string value)
        {
            return Convert.ToDouble(value, m_formatProvider);
        }

        protected Point Point(string coordinates)
        {
            if (string.IsNullOrWhiteSpace(coordinates))
                throw new ArgumentNullException(nameof(coordinates));

            string[] tokens = coordinates.Split(';');

            return double.TryParse(tokens[0], NumberStyles.Float, m_formatProvider, out double x) &&
                   double.TryParse(tokens[1], NumberStyles.Float, m_formatProvider, out double y)
                ? new Point(x, y)
                : default(Point);
        }

        protected static Color Color(string colorValue)
        {
            if (string.IsNullOrWhiteSpace(colorValue))
                throw new ArgumentNullException(nameof(colorValue));

            string[] tokens = colorValue.Split(';');
            return byte.TryParse(tokens[0], out byte a) &&
                   byte.TryParse(tokens[1], out byte r) &&
                   byte.TryParse(tokens[2], out byte g) &&
                   byte.TryParse(tokens[3], out byte b)
                ? Argb(a, r, g, b)
                : Colors.Black;
        }

        protected static DashStyle DashStyle(string dashStyleValue)
        {
            return dashStyleValue.AsDashStyle();
        }

        private static Color Argb(byte a, byte r, byte g, byte b)
        {
            return System.Windows.Media.Color.FromArgb(a, r, g, b);
        }
    }
}