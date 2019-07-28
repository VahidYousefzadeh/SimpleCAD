using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Viewer
{
    public class ShapeConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(ShapeDefinition).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject item = JObject.Load(reader);
            string type = item["type"].Value<string>();
            switch (type)
            {
                case "line":
                    return item.ToObject<LineDefinition>();
                case "circle":
                    return item.ToObject<CircleDefinition>();
                case "triangle":
                    return item.ToObject<TriangleDefinition>();
                default:
                    return null;
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
        }
    }
}