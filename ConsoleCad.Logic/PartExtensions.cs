

namespace ConsoleCad.Logic;

public static class PartExtensions {
    // Flattens a many layerd higharchical structure into 1d <List>
    public static List<string> ReturnAllChildren(this Part part) {
        List<string> result = new List<string>();
        foreach (Part child in part.Children) {
            AssignToTempParts(child, ref result);
        }
        return result;
    }

    // Takes in a part and accuratley models a copy in temp part of all the children to the nth generation
    private static bool AssignToTempParts(Part realPart, ref List<string> res) {
        TempPart tempPart = new TempPart(realPart.Name);
        bool exit = false; //Might not need

        foreach (Part child in realPart.Children) {
            bool completedParent =
                realPart.Children.Count == 0
                || tempPart.Children.Count == realPart.Children.Count
                || tempPart.AllChildrenProcessed;

            if (!completedParent) {
                break;
            }
            else {
                string newChildName = child.Name;
                tempPart.AddChild(newChildName);
                TempPart workingChild = tempPart.Children.FirstOrDefault(c => c.TempPartName == newChildName);
                workingChild.ProcessPart();
                if (child.Children.Count != 0) {
                    AssignToTempParts(child, ref res);
                }
            }
        }
        return exit;
    }
}