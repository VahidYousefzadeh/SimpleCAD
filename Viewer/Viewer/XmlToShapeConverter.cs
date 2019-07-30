using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Viewer
{
    public static class XmlToShapeConverter
    {
        public static IEnumerable<Shape> Parse(string path)
        {
            IList<ShapeDefinition> shapeDefinitions = new List<ShapeDefinition>();

            XElement root = XElement.Load(path);
            foreach (XElement element in root.Descendants())
            {
                switch ($"{element.Name}")
                {
                    case "Line":
                        shapeDefinitions.Add(XmlToLineDefinition(element));
                        break;
                    case "Circle":
                        shapeDefinitions.Add(XmlToCircleDefinition(element));
                        break;
                    case "Triangle":
                        shapeDefinitions.Add(XmlToTriangleDefinition(element));
                        break;
                }
            }

            return shapeDefinitions.Select(o => o.Convert());
        }

        private static ShapeDefinition XmlToLineDefinition(XContainer element)
        {
            return new LineDefinition
            {
                A = element.Element("A")?.Value,
                B = element.Element("B")?.Value,
                Color = element.Element("Color")?.Value,
                LineType = element.Element("LineType")?.Value
            };
        }


        private static ShapeDefinition XmlToCircleDefinition(XElement element)
        {
            var circle = new CircleDefinition
            {
                Center = element.Element("Center")?.Value,
                Color = element.Element("Color")?.Value,
                LineType = element.Element("LineType")?.Value
            };

            if (double.TryParse(element.Element("Radius")?.Value, out double radius))
                circle.Radius = radius;

            if (bool.TryParse(element.Element("Filled")?.Value, out bool filled))
                circle.Filled = filled;

            return circle;
        }

        private static ShapeDefinition XmlToTriangleDefinition(XElement element)
        {
            var triangle = new TriangleDefinition
            {
                A = element.Element("A")?.Value,
                B = element.Element("B")?.Value,
                C = element.Element("C")?.Value,
                Color = element.Element("Color")?.Value,
                LineType = element.Element("LineType")?.Value
            };

            if (bool.TryParse(element.Element("Filled")?.Value, out bool filled))
                triangle.Filled = filled;

            return triangle;
        }
    }
}