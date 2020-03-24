using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace JParser.Extensions
{
    public static class JsonElementExtensions
    {
        public static JsonElement Sort(this JsonElement jsonElement, string propertyName = "") =>
            jsonElement.ValueKind switch
            {
                JsonValueKind.Object => jsonElement.EnumerateObject()
                                                   .Select(j => j.Value.Sort(j.Name))
                                                   .OrderBy(j => j, JsonElementComparer.Instance)
                                                   .ToList()
                                                   .ToDocument(propertyName).RootElement,

                JsonValueKind.Array => jsonElement.EnumerateArray()
                                                  .Select(value => value.Sort(propertyName))
                                                  .OrderBy(j => j, JsonElementComparer.Instance)
                                                  .ToList()
                                                  .ToDocument(propertyName, true).RootElement,
                _ => jsonElement
            };

        public static JsonDocument ToDocument(this JsonElement jsonElement, string propertyName = "")
        {
            using var output = new MemoryStream();
            using var writer = new Utf8JsonWriter(output);

            if (false == string.IsNullOrWhiteSpace(propertyName))
                writer.WriteStartObject(propertyName);
            else
                writer.WriteStartObject();

            jsonElement.WriteTo(writer);

            writer.WriteEndObject();

            writer.Flush();

            return JsonDocument.Parse(output.ToArray());
        }

        public static JsonDocument ToDocument(this IEnumerable<JsonElement> jsonElements, string propertyName = "", bool isArray = false)
        {
            using var output = new MemoryStream();
            using var writer = new Utf8JsonWriter(output);

            writer.WriteStartObject();

            switch (!string.IsNullOrWhiteSpace(propertyName))
            {
                case true when isArray:
                    writer.WriteStartArray(propertyName);
                    break;

                case true when !isArray:
                    writer.WriteStartObject(propertyName);
                    break;

                case false when isArray:
                    writer.WriteStartArray();
                    break;

                default:
                    break;
            }

            jsonElements.ToList()
                        .ForEach(e => e.WriteTo(writer));

            if (isArray) writer.WriteEndArray();

            if (!string.IsNullOrWhiteSpace(propertyName) && !isArray) writer.WriteEndObject();

            writer.WriteEndObject();

            writer.Flush();

            return JsonDocument.Parse(output.ToArray());
        }
    }
}