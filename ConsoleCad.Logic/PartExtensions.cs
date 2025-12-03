
namespace ConsoleCad.Logic;

public static class PartExtensions {
    // Flattens a many-layered hierarchy into a 1D sequence of names.
    public static IEnumerable<string> ReturnAllChildren(this Part part) {
        // Yield the parent’s own name
        yield return part.Name;

        // Walk through all descendants
        foreach (var name in YieldAllChildren(part)) {
            yield return name;
        }
    }

    private static IEnumerable<string> YieldAllChildren(Part parent) {
        foreach (Part child in parent.Children) {
            yield return child.Name;

            // Why is the Recurrsive method call inside the foreach loop signature?
            // Think of: foreach (var x in GetAllFiles(path)) { }
            foreach (string everythingThatChildsSubtreeProduces in YieldAllChildren(child))
                yield return everythingThatChildsSubtreeProduces;
        }
    }
}

