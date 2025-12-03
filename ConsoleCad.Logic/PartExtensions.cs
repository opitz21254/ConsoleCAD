

namespace ConsoleCad.Logic;

public static class PartExtensions {
    // Flattens a many layerd higharchical structure into 1d <List>
    // I was thinking about this backwards. I may not even need this code.
    public static IEnumerable<string> ReturnAllChildren(this Part part) {
        // Return the parent’s own name
        yield return part.Name;

        // Walk each direct child
        foreach (Part rootPart in part.Children) {
            yield return rootPart.Name;
            foreach (var descendant in AssignToTempParts(rootPart)) {
                yield return descendant;
            }
        }
    }

    // Takes a TempPart object and creates its children. Parent was already modeled in TempPart hiarchey
    // tempPart is unfinished and does not necessaraly have all children created
    private static IEnumerable<string> AssignToTempParts(Part parentPart) {
        
        TempPart tempParent = new TempPart(parentPart.Name);

        foreach (Part child in parentPart.Children) {
            bool childCreationFinished =
                parentPart.Children.Count == 0
                || tempParent.AllChildrenProcessed;

            if (childCreationFinished)
                continue;

            TempPart tempChild = tempParent.AddChild(child.Name);
            tempChild.ProcessPart();

            yield return child.Name;

            if (child.Children.Count != 0) {
                foreach (string name in AssignToTempParts(child))
                    yield return name;
            }
        
        }
    }
}