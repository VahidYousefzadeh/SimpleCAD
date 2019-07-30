using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Media;

namespace Viewer
{
    public static class ShapeToJsonConverter
    {
        public static string Convert(IList<Shape> shapes)
        {
            if (shapes == null || shapes.Count == 0)
                return "";

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("[");
            for (int i = 0; i < shapes.Count; i++)
            {
                Shape shape = shapes[i];
                string json = ShapeToJson(shape);
                if (string.IsNullOrEmpty(json))
                    continue;

                sb.Append(json);

                if (i != shapes.Count - 1)
                    sb.Append(",");
            }

            sb.AppendLine("]");

            return sb.ToString();
        }

        private static string ShapeToJson(Shape shape)
        {
            if (shape is Line line)
                return LineToJson(line);

            return "";
        }

        private static string LineToJson(Line line)
        {

            IFormatProvider f = new CultureInfo("de");
            LineGeometry lineGeometry = (LineGeometry) line.Geometry;
            var sb = new StringBuilder();
            sb.AppendLine("{");
            sb.AppendLine("\"type\": \"line\", ");

            sb.Append("\"a\": \"");
            sb.Append($"{lineGeometry.StartPoint.X.ToString(f)}; {lineGeometry.StartPoint.Y.ToString(f)}");
            sb.Append("\",");

            sb.AppendLine();

            sb.Append("\"b\": \"");
            sb.Append($"{lineGeometry.EndPoint.X.ToString(f)}; {lineGeometry.EndPoint.Y.ToString(f)}");
            sb.Append("\",");

            sb.AppendLine();
            sb.AppendLine(ToJson(line.Pen.Brush));
            sb.AppendLine(ToJson(line.Pen.DashStyle));

            sb.AppendLine("}");

            return sb.ToString();
        }

        private static string ToJson(Brush brush)
        {
            switch (brush)
            {
                case SolidColorBrush solidColorBrush:
                {
                    Color color = solidColorBrush.Color;
                    return $"\"color\": \"{color.A}; {color.R}; {color.G}; {color.B}\",";
                }

                default:
                    return " \"color\": \"127; 255; 255; 255\",";
            }
        }

        private static string ToJson(DashStyle dashStyle)
        {
            return $"\"lineType\": \"{ JsonDashStyleHelper.ToJson(dashStyle)}\"";
        }
    }
}