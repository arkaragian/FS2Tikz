// See https://aka.ms/new-console-template for more information

using FS2Tikz.Configuration;
using libTikz;
using System.CommandLine;
using System.Text.Json;

namespace FS2Tikz;


public class Program {

    public static int Main(string[] args) {


        Argument<string> dir_arg = new("directory") {
            Description = "A JSON file that contains specific execution configuration"
        };

        Argument<string> conf_arg = new("config file") {
            Description = "A JSON file that contains specific execution configuration"
        };



        Command exec_dir = new("dir", "Execute Directory") {
            dir_arg
        };

        exec_dir.SetAction((ParseResult r) => ExecuteDirectory(r, dir_arg));


        Command exec_config = new("config", "Execute Configuration");
        exec_config.SetAction((ParseResult r) => ExecuteConfiguration(r, conf_arg));

        RootCommand rootCommand = new("Filesystem to Tikz");

        //rootCommand.Subcommands.Add(exec_config);
        rootCommand.Subcommands.Add(exec_dir);

        return rootCommand.Parse(args).Invoke();
    }


    public static int ExecuteConfiguration(ParseResult r, Argument<string> arg) {

        string? config_file = r.GetValue(arg);

        if (config_file is null) {
            Console.WriteLine("No Configuration Supplied Exiting...");
            return 1;
        }

        string json = File.ReadAllText(config_file);

        TikzFSPictureGeneratorSettings? settings = JsonSerializer.Deserialize<TikzFSPictureGeneratorSettings>(json);


        if (settings is null) {
            Console.WriteLine("No configuration settings could be parsed. Exiting...");
            return 1;
        }

        if (settings.Directtory is null) {
            Console.WriteLine("Settings do not point at a directory. Please define the Directory field in your JSON file. Exiting...");
            return 1;
        }


        TikzFSPictureGenerator gen = new(settings);
        TikzPicture? pic = gen.ParseFileSystem(settings.Directtory);

        if (pic is null) {
            Console.WriteLine("No Picure was produced");
            return 0;
        }

        string code = pic.ToTikz();

        using StreamWriter outputFile = new("out.tex");
        outputFile.Write(code);

        return 0;
    }

    public static int ExecuteDirectory(ParseResult r, Argument<string> arg) {

        string? directory = r.GetValue(arg);

        if (directory is null) {
            Console.WriteLine("No Directory Supplied Exiting...");
            return 1;
        }



        TikzFSPictureGeneratorSettings settings = new() {
            ExcludeDirectories = [
                ".git",
                //"bin",
                //"obj"
            ]
        };

        TikzFSPictureGenerator gen = new(settings);
        TikzPicture? pic = gen.ParseFileSystem(directory);

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