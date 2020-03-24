using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace JParser.Extensions
{
    public static class JsonDocumentExtensions
    {
        public static JsonDocument Sort(this JsonDocument jsonDocument) =>
            jsonDocument.RootElement
                .EnumerateObject()
                .Select(jp => jp.Sort())
                .OrderBy(j => j.Value, JsonElementComparer.Instance)
                .ToDocument();

        public static async Task<string> ToUtf8StringAsync(this JsonDocument jsonDocument)
        {
            using var output = new MemoryStream();
            using var writer = new Utf8JsonWriter(output, new JsonWriterOptions { Indented = true });

            jsonDocument.WriteTo(writer);

            await writer.FlushAsync();

            return Encoding.UTF8.GetString(output.ToArray());
        }
    }
}