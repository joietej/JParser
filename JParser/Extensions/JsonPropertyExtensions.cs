using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace JParser.Extensions
{
    public static class JsonPropertyExtensions
    {
        public static JsonProperty Join(this IEnumerable<JsonProperty> jsonProperties, string propertyName) =>
           jsonProperties.ToDocument(propertyName).RootElement
                         .EnumerateObject()
                         .First();

        public static JsonProperty Sort(this JsonProperty jsonProperty) => jsonProperty.Value.ValueKind switch
        {
            JsonValueKind.Object => jsonProperty.Value.EnumerateObject()
                                                      .Select(value => value.Sort())
                                                      .OrderBy(j => j.Value, JsonElementComparer.Instance)
                                                      .ToList()
                                                      .Join(jsonProperty.Name),

            JsonValueKind.Array => jsonProperty.Value.EnumerateArray()
                                                      .Select(value => value.Sort())
                                                      .ToList()
                                                      .ToDocument(jsonProperty.Name, true)
                                                      .RootElement
                                                      .EnumerateObject()
                                                      .First(),
            _ => jsonProperty
        };

        public static JsonDocument ToDocument(this IEnumerable<JsonProperty> jsonProperties, string propertyName = "")
        {
            using var output = new MemoryStream();
            using var writer = new Utf8JsonWriter(output);

            writer.WriteStartObject();

            if (false == string.IsNullOrWhiteSpace(propertyName)) writer.WriteStartObject(propertyName);

            jsonProperties.ToList()
                          .ForEach(j => j.WriteTo(writer));

            if (false == string.IsNullOrWhiteSpace(propertyName)) writer.WriteEndObject();

            writer.WriteEndObject();

            writer.Flush();

            return JsonDocument.Parse(output.ToArray());
        }
    }
}