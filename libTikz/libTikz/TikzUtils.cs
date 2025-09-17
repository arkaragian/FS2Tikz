using System.Text;
using System.Text.RegularExpressions;

namespace FS2Tikz;

public partial class TikzUtils {
    [GeneratedRegex(@"^[A-Za-z0-9:-]+$", RegexOptions.Compiled)]
    private static partial Regex MyRegex();

    public static bool IsValidTikzNodeName(string input) {

        Regex UnbracedPattern = MyRegex();

        if (string.IsNullOrEmpty(input)) {
            return false;
        }

        // Case 1: unbraced form
        if (UnbracedPattern.IsMatch(input)) {
            return true;
        }

        // Case 2: braced form { ... }
        if (input.Length >= 2 && input[0] == '{' && input[^1] == '}') {
            // Quick balance check for braces inside
            int depth = 0;
            for (int i = 0; i < input.Length; i++) {
                if (input[i] == '{') {
                    depth++;
                } else if (input[i] == '}') {
                    depth--;
                    if (depth < 0) {
                        return false;
                    }
                }
            }

            return depth == 0;
        }

        return false;
    }


    public static string NormalizeNodeContent(string content) {
        string result = content;
        ReadOnlySpan<char> chars = "\\&%$#_{}~^";
        foreach (char c in chars) {
            result = NormalizeForCharacter(result, c);
        }

        return result;
    }

    public static string NormalizeForCharacter(ReadOnlySpan<char> content, char c) {
        StringBuilder sb = new();
        for (int i = 0; i < content.Length; i++) {
            if (content[i] == c) {
                _ = sb.Append('\\').Append(content[i]);
            } else {
                _ = sb.Append(content[i]);
            }
        }

        return sb.ToString();
    }

}