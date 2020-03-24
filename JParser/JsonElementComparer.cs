using System;
using System.Collections.Generic;
using System.Text.Json;

namespace JParser
{
    public class JsonElementComparer : IComparer<JsonElement>
    {
        private static Lazy<JsonElementComparer> instance =
            new Lazy<JsonElementComparer>(() => new JsonElementComparer());

        public static JsonElementComparer Instance => instance.Value;

        public int Compare(JsonElement x, JsonElement y) => x.ValueKind == y.ValueKind
                ? 0
                : (x.ValueKind, y.ValueKind) switch
                {
                    (JsonValueKind.Number, JsonValueKind.Array) => -1,
                    (JsonValueKind.Number, JsonValueKind.Object) => -1,
                    (JsonValueKind.String, JsonValueKind.Array) => -1,
                    (JsonValueKind.String, JsonValueKind.Object) => -1,
                    (JsonValueKind.False, JsonValueKind.Array) => -1,
                    (JsonValueKind.False, JsonValueKind.Object) => -1,
                    (JsonValueKind.True, JsonValueKind.Array) => -1,
                    (JsonValueKind.True, JsonValueKind.Object) => -1,
                    (JsonValueKind.Object, JsonValueKind.Array) => -1,
                    (JsonValueKind.Null, JsonValueKind.Array) => -1,
                    (JsonValueKind.Undefined, JsonValueKind.Array) => -1,
                    _ => 1
                };
    }
}