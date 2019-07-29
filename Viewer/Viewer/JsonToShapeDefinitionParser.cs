using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Script.Serialization;

namespace Viewer
{
    public static class JsonToShapeDefinitionParser
    {
        public static IList<ShapeDefinition> Parse(string path)
        {
            List<ShapeDefinition> shapeDefinitions = new List<ShapeDefinition>();

            var jsonSerializer = new JavaScriptSerializer();
            var nodes = jsonSerializer.Deserialize<dynamic>(File.ReadAllText(path));
            foreach (dynamic node in nodes)
            {
                if (node["type"] == "line")
                    shapeDefinitions.Add(JsonToLineDefinition(node));
                else if (node["type"] == "circle")
                    shapeDefinitions.Add(JsonToCircleDefinition(node));
                else if (node["type"] == "triangle")
                    shapeDefinitions.Add(JsonToTriangleDefinition(node));
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