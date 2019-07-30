using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Web.Script.Serialization;
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
            sb.AppendLine("\"lineType\":\"solid\"");

            sb.AppendLine("}");

            return sb.ToString();

        }

        private static string ToJson(Brush brush)
        {
            if (brush is SolidColorBrush solidColorBrush)
            {
                Color color = solidColorBrush.Color;
                return $"\"color\": \"{color.A}; {color.R}; {color.G}; {color.B}\",";
            }

            return " \"color\": \"127; 255; 255; 255\",";
        }
    }
    public static class JsonToShapeDefinitionConverter
    {
        public static IList<ShapeDefinition> Parse(string path)
        {
            List<ShapeDefinition> shapeDefinitions = new List<ShapeDefinition>();

            var jsonSerializer = new JavaScriptSerializer();
            var nodes = jsonSerializer.Deserialize<dynamic>(File.ReadAllText(path));
            foreach (dynamic node in nodes)
            {
                switch (node["type"])
                {
                    case "line":
                        shapeDefinitions.Add(JsonToLineDefinition(node));
                        break;
                    case "circle":
                        shapeDefinitions.Add(JsonToCircleDefinition(node));
                        break;
                    case "triangle":
                        shapeDefinitions.Add(JsonToTriangleDefinition(node));
                        break;
                }
            }

            return shapeDefinitions;
        }

        private static ShapeDefinition JsonToLineDefinition(dynamic node)
        {
            return new LineDefinition
            {
                A = node["a"],
                B = node["b"],
                Color = node["color"],
                LineType = node["lineType"],
            };
        }

        private static ShapeDefinition JsonToCircleDefinition(dynamic node)
        {
            return new CircleDefinition
            {
                Center = node["center"],
                Color = node["color"],
                LineType = node["lineType"],
                Radius = Convert.ToDouble(node["radius"]),
                Filled = Convert.ToBoolean(node["filled"])
            };
        }

        private static ShapeDefinition JsonToTriangleDefinition(dynamic node)
        {
            return new TriangleDefinition
            {
                A = node["a"],
                B = node["b"],
                C = node["c"],
                Color = node["color"],
                LineType = node["lineType"],
                Filled = Convert.ToBoolean(node["filled"])
            };
        }
    }
}