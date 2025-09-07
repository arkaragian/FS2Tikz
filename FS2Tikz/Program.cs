// See https://aka.ms/new-console-template for more information
namespace FS2Tikz;


public class Program {
    public static int Main(string[] args) {


        TikzFSPictureGeneratorSettings settings = new() {
            ExcludeDirectories = [
                ".git",
                //"bin",
                //"obj"
            ]
        };

        TikzFSPictureGenerator gen = new(settings);
        TikzPicture? pic = gen.ParseFileSystem(args[0]);

        if (pic is null) {
            Console.WriteLine("No Picure was produced");
            return 0;
        }

        string code = pic.ToTikz();

        using StreamWriter outputFile = new("out.tex");
        outputFile.Write(code);

        return 0;
    }


}