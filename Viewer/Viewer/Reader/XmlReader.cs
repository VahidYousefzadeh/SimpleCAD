using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Viewer.Graphics;

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
                    case "line":
                        shapes.Add(ReadLine(element));
                        break;
                    case "circle":
                        shapes.Add(ReadCircle(element));
                        break;
                    case "triangle":
                        shapes.Add(ReadTriangle(element));
                        break;
                    case "rectangle":
                        shapes.Add(ReadRectangle(element));
                        break;
                }
            }

            return shapes;
        }

        private Shape ReadLine(XContainer xc)
        {
            return new Line(Point(xc.Element("a")?.Value), Point(xc.Element("b")?.Value))
            {
                Color = Color(xc.Element("color")?.Value),
                LineStyle = DashStyle(xc.Element("lineType")?.Value)
            };
        }

        private Shape ReadCircle(XContainer xc)
        {
            return new Circle(
                Point(xc.Element("center")?.Value),
                Double(xc.Element("radius")?.Value))
            {
                Filled = Convert.ToBoolean(xc.Element("filled")?.Value),
                Color = Color(xc.Element("color")?.Value),
                LineStyle = DashStyle(xc.Element("lineType")?.Value)
            };
        }

        private Shape ReadTriangle(XContainer xc)
        {
            return new Triangle(
                Point(xc.Element("a")?.Value),
                Point(xc.Element("b")?.Value),
                Point(xc.Element("c")?.Value))
            {
                Filled = Convert.ToBoolean(xc.Element("filled")?.Value),
                Color = Color(xc.Element("color")?.Value),
                LineStyle = DashStyle(xc.Element("lineType")?.Value)
            };
        }

        private Shape ReadRectangle(XContainer xc)
        {
            return new Rectangle(Point(xc.Element("a")?.Value), Point(xc.Element("b")?.Value))
            {
                Filled = Convert.ToBoolean(xc.Element("filled")?.Value),
                Color = Color(xc.Element("color")?.Value),
                LineStyle = DashStyle(xc.Element("lineType")?.Value)
            };
        }
    }
}