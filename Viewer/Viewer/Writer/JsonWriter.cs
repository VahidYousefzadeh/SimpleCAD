using System;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using Viewer.Graphics;

namespace Viewer.Writer
{
    public sealed class JsonWriter : IWriter<string>
    {
        private readonly IFormatProvider m_formatProvider;

        public JsonWriter(IFormatProvider formatProvider)
        {
            m_formatProvider = formatProvider;
        }

        public string WriteLine(Point a, Point b, Color color, DashStyle dashStyle)
        {
            return string.Join(
                "\n",
                "{",
                string.Join(
                    ",\n",
                    "\"type\": \"line\"",
                    WriteLineGeometry(a, b),
                    WriteColor(color),
                    WriteDashStyle(dashStyle)),
                "}");
        }

        public string WriteCircle(Point center, double radius, Color color, DashStyle dashStyle, bool filled)
        {
            return string.Join(
                "\n",
                "{",
                string.Join(
                    ",\n",
                    "\"type\": \"circle\"",
                    WriteCircleGeometry(center, radius),
                    WriteFilled(filled),
                    WriteColor(color),
                    WriteDashStyle(dashStyle)),
                "}");
        }

        public string WriteTriangle(Point a, Point b, Point c, Color color, DashStyle dashStyle, bool filled)
        {
            return string.Join(
                "\n",
                "{",
                string.Join(
                    ",\n",
                    "\"type\": \"triangle\"",
                    WriteTriangleGeometry(a, b, c),
                    WriteFilled(filled),
                    WriteColor(color),
                    WriteDashStyle(dashStyle)),
                "}");
        }

        public string WriteRectangle(Point a, Point b, Color color, DashStyle dashStyle, bool filled)
        {
            return string.Join(
                "\n",
                "{",
                string.Join(
                    ",\n",
                    "\"type\": \"rectangle\"",
                    WriteLineGeometry(a, b),
                    WriteFilled(filled),
                    WriteColor(color),
                    WriteDashStyle(dashStyle)),
                "}");
        }

        public string WriteShapes(Shape[] shapes)
        {
            string json = string.Join(",\n", shapes.Select(o => o.Write(this)));
            return string.Join("\n","[", json, "]");
        }

        private string WriteLineGeometry(Point a, Point b)
        {
            return $"\"a\": \"{a.X.ToString(m_formatProvider)}; {a.Y.ToString(m_formatProvider)}\",\n" +
                   $"\"b\": \"{b.X.ToString(m_formatProvider)}; {b.Y.ToString(m_formatProvider)}\"";
        }

        private string WriteCircleGeometry(Point center, double radius)
        {
            return $"\"center\": \"{center.X.ToString(m_formatProvider)}; {center.Y.ToString(m_formatProvider)}\",\n" +
                   $"\"radius\": \"{radius.ToString(m_formatProvider)}\"";
        }

        private string WriteTriangleGeometry(Point a, Point b, Point c)
        {
            return $"\"a\": \"{a.X.ToString(m_formatProvider)}; {a.Y.ToString(m_formatProvider)}\",\n" +
                   $"\"b\": \"{b.X.ToString(m_formatProvider)}; {b.Y.ToString(m_formatProvider)}\",\n" +
                   $"\"c\": \"{c.X.ToString(m_formatProvider)}; {c.Y.ToString(m_formatProvider)}\"";
        }

        private static string WriteColor(Color color)
        {
            return $"\"color\": \"{color.A}; {color.R}; {color.G}; {color.B}\"";
        }

        private static string WriteDashStyle(DashStyle dashStyle)
        {
            return $"\"lineType\": \"{dashStyle.AsString()}\"";
        }

        private static string WriteFilled(bool filled)
        {
            string boolString = filled.ToString().ToLower();
            return $"\"filled\": {boolString}";
        }
    }
}