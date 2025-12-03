

namespace ConsoleCad.Logic;

// Every part needs a root marker that all of the parts coordinates
// (stored as absolute coordinates) are baised off of.
public class TempPart {
    public string TempPartName { get; }
    public bool AllChildrenProcessed { get; private set; } = false;
    private bool hasBeenProcessed { get; set; } = false;
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

    public void ProcessPart() {
        hasBeenProcessed = true;
        bool allSiblingsProcessed = true;

        foreach (TempPart sibling in Parent.Children) {
            allSiblingsProcessed &= sibling.hasBeenProcessed;
        }
        
        Parent.AllChildrenProcessed = allSiblingsProcessed;
    }
}