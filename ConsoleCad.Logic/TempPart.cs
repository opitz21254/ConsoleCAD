using System.Diagnostics;

namespace ConsoleCad.Logic;

// Every part needs a root marker that all of the parts coordinates
// (stored as absolute coordinates) are baised off of.
public class TempPart {
    public string TempPartName { get; }
    public bool AccountedFor = false;
    public List<TempPart> Children { get; } = new();

    public TempPart(string tempPartName) {
        TempPartName = tempPartName;
    }

    public TempPart AddChild(string name) {
        var child = new TempPart(name);
        Children.Add(child);
        HasChildren = true;
        return child;
    }

    public bool ProcessPart() {
        AccountedFor = true;
        return true;
    }
}