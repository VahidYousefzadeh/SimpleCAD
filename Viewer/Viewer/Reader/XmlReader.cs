using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Viewer.Reader
{
    public sealed class XmlReader : ShapeReader
    {
        public XmlReader(IFormatProvider formatProvider) : base(formatProvider)
        {
        }

        public override IEnumerable<Shape> Read(string filename)
        {
            IList<Shape> shapes = new List<Shape>();

            XElement root = XElement.Load(filename);
            foreach (XElement element in root.Descendants())
            {
                switch ($"{element.Name}")
                {
                    case "Line":
                        shapes.Add(ReadLine(element));
                        break;
                    case "Circle":
                        shapes.Add(ReadCircle(element));
                        break;
                    case "Triangle":
                        shapes.Add(ReadTriangle(element));
                        break;
                }
            }

            return shapes;
        }

        private Shape ReadLine(XContainer xc)
        {
            return new Line(Point(xc.Element("A")?.Value), Point(xc.Element("B")?.Value))
            {
                Color = Color(xc.Element("Color")?.Value),
                LineStyle = DashStyle(xc.Element("LineType")?.Value)
            };
        }

        private Shape ReadCircle(XContainer xc)
        {
            return new Circle(
                Point(xc.Element("Center")?.Value),
                Convert.ToDouble(xc.Element("Radius")?.Value))
            {
                Filled = Convert.ToBoolean(xc.Element("Filled")?.Value),
                Color = Color(xc.Element("Color")?.Value),
                LineStyle = DashStyle(xc.Element("LineType")?.Value)
            };
        }

        private Shape ReadTriangle(XContainer xc)
        {
            return new Triangle(
                Point(xc.Element("a")?.Value),
                Point(xc.Element("b")?.Value),
                Point(xc.Element("c")?.Value))
            {
                Filled = Convert.ToBoolean(xc.Element("Filled")?.Value),
                Color = Color(xc.Element("Color")?.Value),
                LineStyle = DashStyle(xc.Element("LineType")?.Value)
            };
        }
    }
}