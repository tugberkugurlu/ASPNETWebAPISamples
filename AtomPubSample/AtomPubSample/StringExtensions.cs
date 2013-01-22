using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace AtomPubSample {

    internal static class StringExtensions {

        internal static string ToSlug(this string value, int? maxLength = null) {

            // if it's already a valid slug, return it
            if (new Regex(@"(^[a-z0-9])([a-z0-9-]+)*([a-z0-9])$").IsMatch(value)) {

                return value;
            }

            return GenerateSlug(value, maxLength);
        }

        private static string GenerateSlug(string value, int? maxLength = null) {

            // prepare string, remove accents, lower case and convert hyphens to whitespace
            var result = RemoveAccent(value).Replace("-", " ").ToLowerInvariant();

            result = Regex.Replace(result, @"[^a-z0-9\s-]", string.Empty); // remove invalid characters
            result = Regex.Replace(result, @"\s+", " ").Trim(); // convert multiple spaces into one space

            if (maxLength.HasValue) // cut and trim
                result = result.Substring(0, result.Length <= maxLength ? result.Length : maxLength.Value).Trim();

            return Regex.Replace(result, @"\s", "-"); // replace all spaces with hyphens
        }

        private static string RemoveAccent(string value) {

            var bytes = Encoding.GetEncoding("Cyrillic").GetBytes(value);
            return Encoding.ASCII.GetString(bytes);
        }
    }
}