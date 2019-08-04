using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Script.Serialization;

namespace Viewer.Reader
{
    public class JsonReader : ShapeReader
    {
        public override IEnumerable<Shape> Read(string filename)
        {
            List<Shape> shapes = new List<Shape>();

            var jsonSerializer = new JavaScriptSerializer();
            var nodes = jsonSerializer.Deserialize<dynamic>(File.ReadAllText(filename));
            foreach (dynamic node in nodes)
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
            return new Circle(Point(node["center"]), Convert.ToDouble(node["radius"]))
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
    }
}