namespace FS2Tikz;

/// <summary>
/// A class used to provide shift for recursive algorithms
/// </summary>
public class ShiftProvider {
    public int IndentLevel { get; set; }
    public int LastProvidedAbsoluteShift { get; private set; }

    public int GetXShift() {
        int absoluteShift = IndentLevel * 50;
        int last = LastProvidedAbsoluteShift;

        Console.Write("Last Provded Value {0} ", LastProvidedAbsoluteShift);

        if (LastProvidedAbsoluteShift == absoluteShift) {
            LastProvidedAbsoluteShift = absoluteShift;
            Console.WriteLine(" Unchanged. Will provide {0}", 0);
            return 0;
        }

        int toReturn = absoluteShift - LastProvidedAbsoluteShift;

        Console.WriteLine("Indent Level {0} Will Provide {1}", IndentLevel, toReturn);

        LastProvidedAbsoluteShift = absoluteShift;
        return toReturn;
    }

}