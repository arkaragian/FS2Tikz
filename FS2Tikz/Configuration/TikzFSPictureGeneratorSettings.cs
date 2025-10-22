namespace FS2Tikz.Configuration;

public class TikzFSPictureGeneratorSettings {
    /// <summary>
    /// The directory that will be indexed
    /// </summary>
    public string? InputDirectory { get; set; }

    /// <summary>
    /// The output location for the files
    /// </summary>
    public string? OutputDirectory { get; set; }

    /// <summary>
    /// The name of the standalone file
    /// </summary>
    public required string OutputFileName { get; set; }

    /// <summary>
    /// The name of the standalone wrapper file
    /// </summary>
    public required string OutputWrapperName { get; set; }

    /// <summary>
    /// Files with those names will be excluded from the generated tree
    /// </summary>
    public List<string>? ExcludeFiles { get; set; }

    /// <summary>
    /// Directories with those names will be excluded from the generated tree
    /// </summary>
    public List<string>? ExcludeDirectories { get; set; }

    /// <summary>
    /// Files with those extensions will be excluded from the tree
    /// </summary>
    public List<string>? ExcludeExtensions { get; set; }
}