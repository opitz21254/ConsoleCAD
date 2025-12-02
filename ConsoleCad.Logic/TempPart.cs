using System.Diagnostics;

namespace ConsoleCad.Logic;

// Every part needs a root marker that all of the parts coordinates
// (stored as absolute coordinates) are baised off of.
public class TempPart {
    public string TempPartName { get; }
    public List<string> ChildrenAccountedFor { get; private set; } = new List<string>();
    public TempPart Parent { get; private set; }
    public List<TempPart> Children { get; } = new();

    public TempPart(string tempPartName) {
        TempPartName = tempPartName;
    }

    public TempPart AddChild(string name) {
        var child = new TempPart(name) {
        Parent = this
        };
        Children.Add(child);
        return child;
    }

    public bool ProcessPart() {
        Parent.ChildrenAccountedFor.Add(TempPartName);
        return true;
    }
}