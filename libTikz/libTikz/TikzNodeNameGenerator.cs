namespace libTikz;

/// <summary>
/// A class that creates node names sequentially.
/// </summary>
public class TikzNodeNameGenerator {
    private int _sequence;

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
