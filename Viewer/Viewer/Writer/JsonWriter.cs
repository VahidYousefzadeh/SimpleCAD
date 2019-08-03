using System;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace Viewer.Writer
{
    public sealed class JsonWriter : IWriter<string>
    {
        private readonly IFormatProvider m_formatProvider;
        private const string Separator = ",\n";

        public JsonWriter(IFormatProvider formatProvider)
        {
            m_formatProvider = formatProvider;
        }

        public string WriteLine(Point a, Point b, Color color, DashStyle dashStyle)
        {
            return string.Join(
                Separator,
                "{",
                WriteLineGeometry(a, b),
                WriteColor(color),
                WriteDashStyle(dashStyle),
                "}");
        }

        public string WriteCircle(Point center, double radius, Color color, DashStyle dashStyle, bool filled)
        {
            return string.Join(
                Separator,
                "{",
                WriteCircleGeometry(center, radius),
                WriteFilled(filled),
                WriteColor(color),
                WriteDashStyle(dashStyle),
                "}");
        }

        public string WriteTriangle(Point a, Point b, Point c, Color color, DashStyle dashStyle, bool filled)
        {
            throw new NotImplementedException();
        }

        public string WriteView(View view)
        {
            return string.Join(",","[", view.Shapes.Select(o => o.Write(this)), "]");
        }

        private string WriteLineGeometry(Point a, Point b)
        {
            return $"\"a\": \"{a.X.ToString(m_formatProvider)}; {a.Y.ToString(m_formatProvider)}\"{Separator}" +
                   $"\"b\": \"{b.X.ToString(m_formatProvider)}; {b.Y.ToString(m_formatProvider)}\"";
        }

        private string WriteCircleGeometry(Point center, double radius)
        {
            return $"\"center\": \"{center.X.ToString(m_formatProvider)}; {center.Y.ToString(m_formatProvider)}\"{Separator}" +
                   $"\"radius\": \"{radius.ToString(m_formatProvider)}\"";
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
            return $"\"filled\": {filled}";
        }

    }
}