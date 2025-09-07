using System.Text;
using System.Text.RegularExpressions;

namespace FS2Tikz;

public partial class TikzNode {
    /// <summary>
    /// The name of this node
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// The node content
    /// </summary>
    public string? Content { get; set; }

    /// <summary>
    /// The name of the global style that this node uses.
    /// </summary>
    public string? GlobalStyleName { get; set; }

    /// <summary>
    /// Additional style that this node uses.
    /// </summary>
    public TikzStyle? Style { get; set; }

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


    public string ToTikz(int indentLevel = 0) {
        StringBuilder sb = new();

        for (int i = 0; i < indentLevel; i++) {
            _ = sb.Append(' ');
        }


        _ = sb.Append("\\node ");
        _ = sb.Append('(').Append(Name).Append(')').Append(' ');
        _ = sb.Append('[');

        if (GlobalStyleName is not null) {
            _ = sb.Append(GlobalStyleName).Append(',');
        }

        if (Style is not null) {
            _ = sb.Append(GlobalStyleName is null ? Style.ToTikz(withBraces: true) : Style.ToTikz(withBraces: false));
        } else {
            _ = sb.Remove(sb.Length - 1, 1); // Removes last character
        }


        _ = sb.Append(']');

        _ = sb.Append('{');
        if (Content is not null) {
            _ = sb.Append(Content);
        }
        _ = sb.Append('}');

        _ = sb.Append(';');
        _ = sb.Append('\n');

        return sb.ToString();
    }

    [GeneratedRegex(@"^[A-Za-z0-9:._-]+$", RegexOptions.Compiled)]
    private static partial Regex MyRegex();
}