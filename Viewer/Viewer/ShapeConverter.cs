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

        public override object ReadJson(JsonReader reader,
            Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject item = JObject.Load(reader);
            string type = item["type"].Value<string>();
            if (type == "line")
            {
                return item.ToObject<LineDefinition>();
            }

            if (type == "circle")
            {
                return item.ToObject<CircleDefinition>();
            }

            if (type == "triangle")
            {
                return item.ToObject<TriangleDefinition>();
            }

            return null;
        }

        public override void WriteJson(JsonWriter writer,
            object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}