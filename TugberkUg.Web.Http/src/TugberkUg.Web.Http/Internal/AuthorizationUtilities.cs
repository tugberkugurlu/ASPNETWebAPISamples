using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TugberkUg.Web.Http.Internal {

    internal class AuthorizationUtilities {

        public static readonly string[] _emptyArray = new string[0];

        public static string[] splitString(string original) {

            if (string.IsNullOrEmpty(original))
                return _emptyArray;

            IEnumerable<string> source =
                from piece in original.Split(new char[] {

					','
				})
                let trimmed = piece.Trim()
                where !string.IsNullOrEmpty(trimmed)
                select trimmed;
            return source.ToArray<string>();
        }
    }
}