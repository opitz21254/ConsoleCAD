

namespace ConsoleCad.Logic;

public static class PartExtensions {
    // Flattens a many layerd higharchical structure into 1d <List>
    // public static List<string> ReturnAllChildren(this Part part) {
    //     List<string> result = new List<string>();
    //     foreach (Part child in part.Children) {
    //         bool worked = AssignToTempParts(child, ref result);
    //     }
    //     return result;
    // }

    // Takes in a part and accuratley models a copy in temp part of all the children to the nth generation
    public static bool AssignToTempParts(Part realPart, ref List<string> res) {
        TempPart tempPart = new TempPart(realPart.Name);
        bool exit = false; //Might not need

        foreach (Part child in realPart.Children) {
            bool childCreationFinished =
                realPart.Children.Count == 0
                || tempPart.AllChildrenProcessed;

            if (childCreationFinished) {
                continue;
            }
            else {
                // Everything in ( ) just returns a string name
                var tempChild = tempPart.AddChild(child.Name);
                res.Add(child.Name);
                tempChild.ProcessPart();

                if (child.Children.Count != 0) {
                    AssignToTempParts(child, ref res);
                }
            }
        }
        return exit;
    }

    public static bool GetFirstUnaccountedForChild(string realChildName, List<TempPart> tempChildren, ref TempPart unaccontedChild) {
        if (tempChildren.FirstOrDefault(c => c.TempPartName == realChildName) != null) {
            unaccontedChild = tempChildren.FirstOrDefault(c => c.TempPartName == realChildName);
            return true;
        }
        else {
            return false;
        }
    }
}