// See https://aka.ms/new-console-template for more information
namespace FS2Tikz;

public struct Filters {
    public List<string>? ExcludeFiles { get; set; }
    public List<string>? ExcludeDirectories { get; set; }
    public List<string>? ExcludeExtensions { get; set; }
}

public class Program {
    public static int Main(string[] args) {
        FileSystemToTikz(args[0]);

        return 0;
    }

    public static void FileSystemToTikz(string location) {
        bool isDirectory = Directory.Exists(location);
        if (!isDirectory) {
            return;
        }

        DirectoryInfo rootDir = new(location);

        TikzPicture picture = new();

        // _ = sb.Append("\\documentclass[tikz]{standalone}\n");
        // _ = sb.Append("\\usetikzlibrary{positioning}\n");
        // _ = sb.Append("\\begin{document}\n");
        // _ = sb.Append("\\begin{tikzpicture}[node distance=0.6cm]\n");
        // gen.CodeIndentLevel++;
        //
        Filters f = new();
        ShiftProvider sprov = new();

        _ = DirectoryToTikz(rootDir, picture, f, upstreamName: null, sprov);

        // _ = sb.Append("\\end{tikzpicture}\n");
        // _ = sb.Append("\\end{document}\n");

        string code = picture.ToTikz();

        using StreamWriter outputFile = new("out.tex");
        outputFile.Write(code);

        return;


    }

    /// <summary>
    /// Recursivelly generate a tikz strcture for the given directory
    /// <param name="dir">The directory for which the code will be generated</param>
    /// <param name="picture">The tikz picture object where structue will be generated into</param>
    /// <param name="filters">Things to ignore</param>
    /// <param name="upstreamName">The Tikz node name where the items will be bellow of</param>
    /// <param name="startIndentLevel">The indentation level where this subtree starrs</param>
    /// </summary>
    public static (string, TikzNode) DirectoryToTikz(DirectoryInfo dir, TikzPicture picture, Filters filters, string? upstreamName, ShiftProvider sprov) {

        TikzNode node = new() {
            Name = dir.Name,
            GlobalStyleName = "directory",
            Content = dir.Name,
        };

        int shift = sprov.GetXShift();

        if (shift is not 0) {
            node.Style ??= new TikzStyle();
            node.Style.Pairs ??= [];
            node.Style.Pairs.Add(new KeyValuePair<string, string>("xshift", $"{shift}pt"));
        }


        if (upstreamName is not null) {
            node.Style ??= new TikzStyle();
            node.Style.Pairs ??= [];
            node.Style.Pairs.Add(new KeyValuePair<string, string>("below of", upstreamName));
        }

        string newParent = picture.AddNode(node);
        bool incrementedLevel = false;


        DirectoryInfo[] childDirectories = dir.GetDirectories();

        if (childDirectories.Length > 0) {
            sprov.IndentLevel++;
            incrementedLevel = true;
            Console.WriteLine("{0} Has Subdirectories. Incrementing once. New level is {1}", dir.Name, sprov.IndentLevel);
            for (int i = 0; i < childDirectories.Length; i++) {
                DirectoryInfo d = childDirectories[i];
                //The operation before might have indented the tree a lot. We need the equivalent
                //of \r in tikz for nodes.
                (newParent, TikzNode dirNode) = DirectoryToTikz(d, picture, filters, newParent, sprov);

                //TikzNode dirNode = DirectoryToTikz(d, picture, filters, newParent, sprov);
                //newParent = dirNode.Name;


                TikzLine connection = new() {
                    StartNode = node,
                    EndNode = dirNode
                };

                picture.AddLine(connection);
            }
            sprov.IndentLevel--;
        }

        //if (incrementedLevel) {
        //    sprov.IndentLevel--;
        //}

        FileInfo[] files = dir.GetFiles();

        if (files.Length > 0) {
            sprov.IndentLevel++;
            // if (!incrementedLevel) {
            //     Console.WriteLine("{0} Has Files but level is not incremented. Incrementing once. Now at level {1}", dir.Name, sprov.IndentLevel);
            //     sprov.IndentLevel++;
            //     incrementedLevel = true;
            // }
            for (int i = 0; i < files.Length; i++) {
                TikzNode fileNode = FileToTikz(files[i], picture, newParent, sprov);
                newParent = fileNode.Name;


                TikzLine connection = new() {
                    StartNode = node,
                    EndNode = fileNode
                };

                picture.AddLine(connection);
            }

            sprov.IndentLevel--;
        }

        //if (incrementedLevel) {
        //    sprov.IndentLevel--;
        //    Console.WriteLine("Level incremented. Decrementing once. New level is {0}", sprov.IndentLevel);
        //}


        //return node;
        return (newParent, node);

    }

    public static TikzNode FileToTikz(FileInfo f, TikzPicture picture, string parent, ShiftProvider sprov) {
        TikzNode node = new() {
            Name = f.Name,
            GlobalStyleName = picture.HasStyle(f.Extension) ? picture.GetStyle(f.Extension).Name : "file",
            Content = f.Name,
            Style = new TikzStyle {
                Pairs = [
                    new KeyValuePair<string, string>("below of", parent)
                ]
            }
        };

        int shift = sprov.GetXShift();
        if (shift is not 0) {
            node.Style.Pairs.Add(new KeyValuePair<string, string>("xshift", $"{shift}pt"));
        }
        _ = picture.AddNode(node);
        return node;
    }

    public static int CalculateShift(int currentLevel, int operationResultShift) {
        if (operationResultShift > currentLevel) {
            return currentLevel - operationResultShift;
        } else if (operationResultShift < currentLevel) {
            throw new InvalidOperationException("Cannot get shift");
        } else {
            return 0;
        }
    }
}