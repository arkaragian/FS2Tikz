namespace FS2Tikz;

/// <summary>
/// A class that creates node names sequentially.
/// </summary>
public class NodeNameGenerator {
    private int _sequence;

    public NodeNameGenerator() {
    }

    public string GenerateName(string? hint) {
        if (hint is null) {
            return $"node-{_sequence++}";
        }

        bool isValid = TikzUtils.IsValidTikzNodeName(hint);

        if (isValid) {
            return $"{hint}-{_sequence++}";
        } else {
            return $"node-{_sequence++}";
        }

    }
}