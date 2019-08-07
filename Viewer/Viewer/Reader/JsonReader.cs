using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Script.Serialization;
using Viewer.Graphics;

namespace Viewer.Reader
{
    public sealed class JsonReader : ShapeReader
    {
        public JsonReader(IFormatProvider formatProvider) : base(formatProvider)
        {
        }

        public override IEnumerable<Shape> Read(string fileName)
        {
            List<Shape> shapes = new List<Shape>();

            var jsonSerializer = new JavaScriptSerializer();
            foreach (dynamic node in jsonSerializer.Deserialize<dynamic>(File.ReadAllText(fileName)))
            {
                switch (node["type"])
                {
                    case "line":
                        shapes.Add(ReadLine(node));
                        break;
                    case "circle":
                        shapes.Add(ReadCircle(node));
                        break;
                    case "triangle":
                        shapes.Add(ReadTriangle(node));
                        break;
                    case "rectangle":
                        shapes.Add(ReadRectangle(node));
                        break;
                }
            }

            return shapes;
        }

        private Shape ReadLine(dynamic node)
        {
            return new Line(Point(node["a"]), Point(node["b"]))
            {
                Color = Color(node["color"]),
                LineStyle = DashStyle(node["lineType"])
            };
        }

        private Shape ReadCircle(dynamic node)
        {
            return new Circle(Point(node["center"]), Double(node["radius"]))
            {
                Filled = Convert.ToBoolean(node["filled"]),
                Color = Color(node["color"]),
                LineStyle = DashStyle(node["lineType"])
            };
        }

        private Shape ReadTriangle(dynamic node)
        {
            return new Triangle(Point(node["a"]), Point(node["b"]), Point(node["c"]))
            {
                Filled = Convert.ToBoolean(node["filled"]),
                Color = Color(node["color"]),
                LineStyle = DashStyle(node["lineType"])
            };
        }

        private Shape ReadRectangle(dynamic node)
        {
            return new Rectangle(Point(node["a"]), Point(node["b"]))
            {
                Filled = Convert.ToBoolean(node["filled"]),
                Color = Color(node["color"]),
                LineStyle = DashStyle(node["lineType"])
            };
        }
    }
}