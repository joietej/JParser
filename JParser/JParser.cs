using JParser.Extensions;
using System.Text.Json;
using System.Threading.Tasks;

namespace JParser
{
    public static class JParser
    {
        public static async Task<string> ParseAndSortAsync(string jsonData) => await JsonDocument
                .Parse(jsonData)
                .Sort()
                .ToUtf8StringAsync();
    }
}