using System.Globalization;
using System.Text;

namespace FS2Tikz;

public class TikzLine {
    // \draw [-] (projectdir.south west) |-  (csproj.west);

    public required TikzNode StartNode { get; set; }
    public required TikzNode EndNode { get; set; }

    public string ToTikz(int indentLevel) {
        StringBuilder sb = new();

        for (int i = 0; i < indentLevel; i++) {
            _ = sb.Append(' ');
        }

        _ = sb.Append(CultureInfo.InvariantCulture, $"\\draw [-] ([xshift=10pt]{StartNode.Name}.south west) |- ({EndNode.Name}.west);\n");

        return sb.ToString();
    }


}