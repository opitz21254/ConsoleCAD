
using System.Security;

namespace ConsoleCad.Logic;

// baby mobile Philosophy
// Objects can not be rotated or angled, only moved up, down, left, right, front, or back
// Moving a parent moves all of the children, relative to how much the parent moved
// Moving a child does not move a parent
// Their can be multiple matriarch or root parts in a single project
public class ProjRunner {
    public string ProjectName { get; private set; }
    public List<Part> Parts { get; private set; }
     public List<Viewer> Viewers { get; } = new List<Viewer>();
    public Dictionary<string, Part> PartDictionary { get; private set; }

    public ProjRunner(string projectName, List<Part> parts = null) {
        ProjectName = projectName;

        Parts = parts ?? new List<Part>();

        PartDictionary = new Dictionary<string, Part>();
        foreach (var part in Parts) {
            PartDictionary[part.PartName] = part;
        }
    }
}

