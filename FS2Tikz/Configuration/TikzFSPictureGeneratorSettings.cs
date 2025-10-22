namespace FS2Tikz.Configuration;

public class TikzFSPictureGeneratorSettings {
    /// <summary>
    /// The directory that will be indexed
    /// </summary>
    public string? Directtory { get; set; }

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