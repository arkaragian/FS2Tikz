using System.Text;

namespace FS2Tikz;

/// <summary>
/// A style that is applied to the complete tikz picture
/// </summary>
public class TikzPictureStyle {
    public required string Name { get; set; }
    public required TikzStyle Style { get; set; }


    public string ToTikz(int indentLevel) {
        StringBuilder sb = new();
        for (int i = 0; i < indentLevel; i++) {
            _ = sb.Append(' ');
        }

        _ = sb.Append("\\tikzstyle{").Append(Name).Append("}=");
        _ = sb.Append(Style.ToTikz(withBraces: true));
        _ = sb.Append('\n');

        return sb.ToString();
    }
    // \tikzstyle{mainpythonfile}      =   [draw , rectangle , align = center , fill = green!10  , minimum width = 6cm];
}