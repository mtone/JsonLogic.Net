using System;
using System.Collections;
using System.Collections.Generic;

// ReSharper disable CheckNamespace
namespace SimpleJson {
    public static class Utils {
        /// <summary>
        ///     Partial fix for serializing nested dictionaries instead of KeyValuePairs.
        ///     Type must implement non-generic IDictionary to work.
        ///     See <see href="https://github.com/facebook-csharp-sdk/simple-json/issues/80">related issue</see>
        /// </summary>
        public static IDictionary<string, object> TryConvertDictionary(object value) {
            if (value == null) {
                return null;
            }
            if (!IsStringKeyedDictionary(value)) {
                return null;
            }
            var dict = value as IDictionary; 
            if (dict == null) {
                // Can still be null type only implements generic IDictionary, like JsonObject
                return null;
            }
            var result = new Dictionary<string, object>();
            foreach (DictionaryEntry entry in dict) {
                result.Add((string) entry.Key, entry.Value);
            }
            return result;
        }
        
        /// <summary>
        ///     Checks if type is generic IDictionary with string key
        ///     https://stackoverflow.com/questions/16956903/determine-if-type-is-dictionary
        /// </summary>
        private static bool IsStringKeyedDictionary(object value) {
            var type = value.GetType();
            Type[] interfaces = type.GetInterfaces();
            foreach (var i in interfaces) {
                var isGenericDict = i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IDictionary<,>);
                if (!isGenericDict) {
                    continue;
                }
                var isStringKey = i.GetGenericArguments()[0] == typeof(string);
                if (isGenericDict && isStringKey) {
                    return true;
                }
            }
            return false;
        }
    }
}