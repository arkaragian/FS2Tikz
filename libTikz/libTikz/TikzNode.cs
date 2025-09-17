using System.Text;
using System.Text.RegularExpressions;

namespace libTikz;

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
}
