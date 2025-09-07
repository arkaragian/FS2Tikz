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
    //

    public static TikzPictureStyle DirectoryStyle() {
        return new TikzPictureStyle {
            Name = "directory",
            Style = new TikzStyle {
                Flags = [
                    "draw",
                    "rectangle",
                ],
                Pairs = [
                    new KeyValuePair<string,string>("align","center"),
                    new KeyValuePair<string,string>("fill","yellow!20"),
                    new KeyValuePair<string,string>("minimum width","6cm"),
                ]
            }
        };
    }

    public static TikzPictureStyle CSSolutionStyle() {
        return new TikzPictureStyle {
            Name = "cssolution",
            Style = new TikzStyle {
                Flags = [
                    "draw",
                    "rectangle",
                ],
                Pairs = [
                    new KeyValuePair<string,string>("align","center"),
                    new KeyValuePair<string,string>("fill","purple!10"),
                    new KeyValuePair<string,string>("minimum width","6cm"),
                ]
            }
        };
    }


    public static TikzPictureStyle CSProjectStyle() {
        return new TikzPictureStyle {
            Name = "csproject",
            Style = new TikzStyle {
                Flags = [
                    "draw",
                    "rectangle",
                ],
                Pairs = [
                    new KeyValuePair<string,string>("align","center"),
                    new KeyValuePair<string,string>("fill","green!10"),
                    new KeyValuePair<string,string>("minimum width","6cm"),
                ]
            }
        };
    }

    public static TikzPictureStyle CSFileStyle() {
        return new TikzPictureStyle {
            Name = "csfile",
            Style = new TikzStyle {
                Flags = [
                    "draw",
                    "rectangle",
                ],
                Pairs = [
                    new KeyValuePair<string,string>("align","center"),
                    new KeyValuePair<string,string>("fill","purple!30"),
                    new KeyValuePair<string,string>("minimum width","6cm"),
                ]
            }
        };
    }

    public static TikzPictureStyle ExeStyle() {
        return new TikzPictureStyle {
            Name = "exefile",
            Style = new TikzStyle {
                Flags = [
                    "draw",
                    "rectangle",
                ],
                Pairs = [
                    new KeyValuePair<string,string>("align","center"),
                    new KeyValuePair<string,string>("fill","gray!10"),
                    new KeyValuePair<string,string>("minimum width","6cm"),
                ]
            }
        };
    }


    public static TikzPictureStyle JsonStyle() {
        return new TikzPictureStyle {
            Name = "jsonfile",
            Style = new TikzStyle {
                Flags = [
                    "draw",
                    "rectangle",
                ],
                Pairs = [
                    new KeyValuePair<string,string>("align","center"),
                    new KeyValuePair<string,string>("fill","gray!40"),
                    new KeyValuePair<string,string>("minimum width","6cm"),
                ]
            }
        };
    }

    public static TikzPictureStyle PdfStyle() {
        return new TikzPictureStyle {
            Name = "pdffile",
            Style = new TikzStyle {
                Flags = [
                    "draw",
                    "rectangle",
                ],
                Pairs = [
                    new KeyValuePair<string,string>("align","center"),
                    new KeyValuePair<string,string>("fill","red!10"),
                    new KeyValuePair<string,string>("minimum width","6cm"),
                ]
            }
        };
    }


    public static TikzPictureStyle GenericFileStyle() {
        return new TikzPictureStyle {
            Name = "file",
            Style = new TikzStyle {
                Flags = [
                    "draw",
                    "rectangle",
                ],
                Pairs = [
                    new KeyValuePair<string,string>("align","center"),
                    new KeyValuePair<string,string>("minimum width","6cm"),
                ]
            }
        };
    }

}