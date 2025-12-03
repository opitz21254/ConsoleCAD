

namespace ConsoleCad.Logic;

public static class PartExtensions {
    // Flattens a many layerd higharchical structure into 1d <List>
    public static List<string> ReturnAllChildren(this Part part, bool isRootWorld = false) {
        List<string> result = new List<string>();
        if (!isRootWorld) {
            result.Add(part.Name);
        }
        else {
            result.Add("RootWorld");
        }
        foreach (Part rootPart in part.Children) {
            TempPart tempParent = new TempPart(rootPart.Name);
            result.Add(rootPart.Name);
            Transform zeroVector = new Transform(0, 0, 0);
            AssignToTempParts(rootPart, tempParent, zeroVector, ref result);
        }
        return result;
    }

    public static List<string> Move(this Part part, Transform transform, bool isRootWorld = false) {
        List<string> result = new List<string>();
        if (!isRootWorld) {
            result.Add(part.Name);
        }
        else {
            result.Add("RootWorld");
        }
        foreach (Part rootPart in part.Children) {
            TempPart tempParent = new TempPart(rootPart.Name);
            result.Add(rootPart.Name);
            AssignToTempParts(rootPart, tempParent, transform, ref result);
        }
        return result;
    }

    // Takes a TempPart object and creates its children. Parent was already modeled in TempPart hiarchey
    // tempPart is unfinished and does not necessaraly have all children created
    public static bool AssignToTempParts(Part parentPart, TempPart tempParent, Transform transform, ref List<string> res) {
        bool exit = false;

        foreach (Part child in parentPart.Children) {
            bool childCreationFinished =
                parentPart.Children.Count == 0
                || tempParent.AllChildrenProcessed;

            if (childCreationFinished) {
                continue;
            }
            else {
                var tempChild = tempParent.AddChild(child.Name);
                res.Add(child.Name);
                tempChild.ProcessPart();

                if (child.Children.Count != 0) {
                    AssignToTempParts(child, tempChild, transform, ref res);
                }
            }
        }
        return exit;
    }

}