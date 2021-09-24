using System.Collections.Generic;
using System.Linq;

namespace SharpSqlBuilder.Extensions
{
    internal static class DictionaryExtensions
    {
        public static bool TryGetValue<TValue>(this IDictionary<string, TValue> dictionary, string key, bool caseSensitive, out TValue value)
        {
            if (caseSensitive)
            {
                return dictionary.TryGetValue(key, out value);
            }

            var lowerKey = key.ToLowerInvariant();
            var initialKey = dictionary.Keys.FirstOrDefault(x => x.ToLowerInvariant() == lowerKey);
            if (initialKey != null)
                return dictionary.TryGetValue(initialKey, out value);

            value = default;
            return false;
        }

    }
}