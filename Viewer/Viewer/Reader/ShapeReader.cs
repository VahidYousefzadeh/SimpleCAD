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

        public abstract IEnumerable<Shape> Read(string filename);

        protected double Double(string doubleString)
        {
            return Convert.ToDouble(doubleString, m_formatProvider);
        }

        protected Point Point(string coordinates)
        {
            string[] tokens = coordinates.Split(';');

            return double.TryParse(tokens[0], NumberStyles.Float, m_formatProvider, out double x) &&
                   double.TryParse(tokens[1], NumberStyles.Float, m_formatProvider, out double y)
                ? new Point(x, y)
                : default(Point);
        }

        protected Color Color(string color)
        {
            string[] tokens = color.Split(';');
            return byte.TryParse(tokens[0], out byte a) &&
                   byte.TryParse(tokens[1], out byte r) &&
                   byte.TryParse(tokens[2], out byte g) &&
                   byte.TryParse(tokens[3], out byte b)
                ? Argb(a, r, g, b)
                : Colors.Black;
        }

        protected DashStyle DashStyle(string dashStyle)
        {
            return dashStyle.AsDashStyle();
        }

        private static Color Argb(byte a, byte r, byte g, byte b)
        {
            return System.Windows.Media.Color.FromArgb(a, r, g, b);
        }
    }
}