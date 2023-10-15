using System;
using System.Text.RegularExpressions;

namespace WpfUi;

public static class StringExtensions {
    public static string ToCapitalizedWithSpaces(this string input) {
        if (string.IsNullOrWhiteSpace(input)) {
            return string.Empty;
        }

        string pattern = @"(\p{Lu}\p{Ll}*|\p{Ll}+)(?=\p{Lu}|\b)";
        MatchCollection matches = Regex.Matches(input, pattern);

        string result = string.Join(" ", matches);

        return result;
    }
}
