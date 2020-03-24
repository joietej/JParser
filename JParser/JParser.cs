using JParser.Extensions;
using System.Text.Json;
using System.Threading.Tasks;

namespace JParser
{
    public class JParser
    {
        public async Task<string> ParseAsync(string jsonData)
        {
            var sortedJson = JsonDocument
                .Parse(jsonData)
                .Sort();

            return await sortedJson.ToUtf8StringAsync();
        }
    }
}