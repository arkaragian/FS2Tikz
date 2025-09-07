using System.Text;

namespace FS2Tikz;

public class TikzPicture {
    public List<TikzPictureStyle> PictureStyles { get; set; }
    public List<TikzNode> Nodes { get; set; }
    public List<TikzLine> Lines { get; set; }
    private readonly Dictionary<string, TikzPictureStyle> _FileStyleMap;
    private readonly NodeNameGenerator _nameGenerator;

    public TikzPicture() {
        _nameGenerator = new();
        PictureStyles = [];
        Nodes = [];
        Lines = [];

        _FileStyleMap = [];


        TikzPictureStyle dirStyle = TikzPictureStyle.DirectoryStyle();
        PictureStyles.Add(dirStyle);

        TikzPictureStyle @file = TikzPictureStyle.GenericFileStyle();
        PictureStyles.Add(@file);

        TikzPictureStyle cssolution = TikzPictureStyle.CSSolutionStyle();
        PictureStyles.Add(cssolution);
        _FileStyleMap.Add(".sln", cssolution);

        TikzPictureStyle csproject = TikzPictureStyle.CSProjectStyle();
        PictureStyles.Add(csproject);
        _FileStyleMap.Add(".csproj", csproject);


        TikzPictureStyle csfile = TikzPictureStyle.CSFileStyle();
        PictureStyles.Add(csfile);
        _FileStyleMap.Add(".cs", csfile);


        TikzPictureStyle exefile = TikzPictureStyle.ExeStyle();
        PictureStyles.Add(exefile);
        _FileStyleMap.Add(".exe", exefile);
        _FileStyleMap.Add(".dll", exefile);
        _FileStyleMap.Add(".pdb", exefile);


        TikzPictureStyle pdfstyle = TikzPictureStyle.PdfStyle();
        PictureStyles.Add(pdfstyle);
        _FileStyleMap.Add(".pdf", pdfstyle);


        TikzPictureStyle jsonstyle = TikzPictureStyle.JsonStyle();
        PictureStyles.Add(jsonstyle);
        _FileStyleMap.Add(".json", jsonstyle);




    }

    public bool HasStyle(string name) {
        return _FileStyleMap.ContainsKey(name);
    }
    public TikzPictureStyle GetStyle(string name) {
        return _FileStyleMap[name];
    }

    public void AddStyle(TikzPictureStyle style) {
        //TODO: Check that this name does not exist already!
        Console.WriteLine("Adding Style {0}", style.Name);
        PictureStyles.Add(style);
    }

    public string AddNode(TikzNode node) {
        Console.WriteLine("Adding Node {0}", node.Name);
        string newName = _nameGenerator.GenerateName(node.Name);
        node.Name = newName;
        Nodes.Add(node);
        return newName;
    }

    public void AddLine(TikzLine line) {
        Lines.Add(line);
    }

    private static void Begin(StringBuilder sb) {
        _ = sb.Append("\\documentclass[tikz]{standalone}\n");
        _ = sb.Append("\\usetikzlibrary{positioning}\n");
        _ = sb.Append("\\begin{document}\n");
        _ = sb.Append("\\begin{tikzpicture}[node distance=0.6cm]\n\n");
    }

    private static void End(StringBuilder sb) {
        _ = sb.Append("\\end{tikzpicture}\n");
        _ = sb.Append("\\end{document}\n");
    }


    public string ToTikz() {
        Console.WriteLine("Generating Tikz Code");
        StringBuilder sb = new();
        Begin(sb);

        foreach (TikzPictureStyle gs in PictureStyles) {
            _ = sb.Append(gs.ToTikz(indentLevel: 4));
        }

        if (PictureStyles.Count > 0) {
            _ = sb.Append("\n\n");
        }


        foreach (TikzNode n in Nodes) {
            _ = sb.Append(n.ToTikz(indentLevel: 4));
        }


        _ = sb.Append("\n\n");

        foreach (TikzLine l in Lines) {
            _ = sb.Append(l.ToTikz(indentLevel: 4));
        }

        End(sb);
        return sb.ToString();
    }
}