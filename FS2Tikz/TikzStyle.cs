using System.Text;

namespace FS2Tikz;


public class TikzStyle {
    /// <summary>
    /// Represents a list of flags like draw that do now need a spefcific value
    /// </summary>
    public List<string>? Flags { get; set; }

    /// <summary>
    /// Represents a list of style settings that are defined in terms of key value pairs
    /// </summary>
    public List<KeyValuePair<string, string>>? Pairs { get; set; }


    /// <summary>
    /// Generates tikz code
    /// <param name="withBraces">
    ///     Indicates if the generation will be donw with [ and ] braces.
    /// </param>
    /// </summary>
    public string ToTikz(bool withBraces) {
        StringBuilder sb = new();

        if (withBraces) {
            _ = sb.Append('[');
        }

        if (Flags is not null) {
            foreach (string flag in Flags) {
                _ = sb.Append(flag).Append(',');
            }
        }

        if (Pairs is null) {
            _ = sb.Remove(sb.Length - 1, 1); // Removes last character
        } else {
            foreach (KeyValuePair<string, string> kvp in Pairs) {
                _ = sb.Append(kvp.Key).Append('=').Append(kvp.Value).Append(',');
            }
            _ = sb.Remove(sb.Length - 1, 1); // Removes last character
        }

        if (withBraces) {
            _ = sb.Append(']').Append(';');
        }


        return sb.ToString();
    }

    public static TikzStyle DirectoryStyle() {
        return new TikzStyle {
            Flags = [
                "draw",
                "rectangle",
            ],
            Pairs = [
                new KeyValuePair<string,string>("align","center"),
                new KeyValuePair<string,string>("fill","yellow!10"),
                new KeyValuePair<string,string>("minimum width","6cm"),
            ]
        };
    }

    public static TikzStyle CSSolutionStyle() {
        return new TikzStyle {
            Flags = [
                "draw",
                "rectangle",
            ],
            Pairs = [
                new KeyValuePair<string,string>("align","center"),
                new KeyValuePair<string,string>("fill","purple!10"),
                new KeyValuePair<string,string>("minimum width","6cm"),
            ]
        };
    }


    public static TikzStyle CSProjectStyle() {
        return new TikzStyle {
            Flags = [
                "draw",
                "rectangle",
            ],
            Pairs = [
                new KeyValuePair<string,string>("align","center"),
                new KeyValuePair<string,string>("fill","green!10"),
                new KeyValuePair<string,string>("minimum width","6cm"),
            ]
        };
    }

    public static TikzStyle CSFileStyle() {
        return new TikzStyle {
            Flags = [
                "draw",
                "rectangle",
            ],
            Pairs = [
                new KeyValuePair<string,string>("align","center"),
                new KeyValuePair<string,string>("fill","purple!30"),
                new KeyValuePair<string,string>("minimum width","6cm"),
            ]
        };
    }


    public static TikzStyle GenericFileStyle() {
        return new TikzStyle {
            Flags = [
                "draw",
                "rectangle",
            ],
            Pairs = [
                new KeyValuePair<string,string>("align","center"),
                new KeyValuePair<string,string>("minimum width","6cm"),
            ]
        };
    }
}