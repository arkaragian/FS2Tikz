namespace FS2Tikz;

public class NodeNameGenerator {
    private int _sequence;

    public NodeNameGenerator() {
    }

    public string GenerateName(string? hint) {
        if (hint is null) {
            return $"node-{_sequence++}";
        }

        bool isValid = TikzNode.IsValidTikzNodeName(hint);

        if (isValid) {
            return $"{hint}-{_sequence++}";
        } else {
            return $"node-{_sequence++}";
        }

    }
}