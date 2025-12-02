using System.Diagnostics.Tracing;
using System.Dynamic;
using System.Security.Cryptography.X509Certificates;

namespace ConsoleCad.Logic;

// Every part needs a root marker that all of the parts coordinates
// (stored as absolute coordinates) are baised off of.
public class Part {
    public string Name { get; }
    public int NthChild { get; set; } = 0;
    public Transform LocalTransform { get; private set; }

    public Part Parent { get; private set; }
    public List<Part> Children { get; } = new();

    private readonly Dictionary<string, Marker> markerLookup = new();
    public IReadOnlyDictionary<string, Marker> Markers => markerLookup;

    public List<TempPart> childsChildren;

    public Part(string partName, Transform transform) {
        Name = partName;
        LocalTransform = transform;
    }

    // Recurrsive
    public Transform WorldTransform {
        get {
            if (Parent == null)
                return LocalTransform;
            return Parent.WorldTransform + LocalTransform;
        }
    }

    public Part AddChild(string name, Transform localOffset) {
        var child = new Part(name, localOffset) {
            Parent = this
        };
        child.NthChild = Parent.Children.Count() + 1;
        Children.Add(child);
        return child;
    }

    // Flattens a many layerd higharchical structure into 1d <List>
    public bool ReturnAllChildren(out List<string> partsChildren) {
        foreach (Part child in Children) {
            childsChildren = new List<TempPart>();
            AssignToTempParts(child, ref childsChildren);
            return true;
        }
    }

    public bool AssignToTempParts(Part realPart, ref List<TempPart> tempChildren) {
        TempPart tempPart = new TempPart(realPart.Name);

        bool exit = false; //Might not need
        int n = 0;

        do {
            if (realPart.Children.Count == 0 || tempPart.Children.Count == realPart.Children.Count) {
                break;
            }
            else {
                string realChildName = realPart.Children[n].Name;
                TempPart tempChildPart = new TempPart("");

                if (GetFirstUnaccountedForChild(realChildName, tempChildren, ref tempChildPart)) {
                    Part realPartToPassOn = new Part();
                    TryGetChildByName(tempChildPart.TempPartName, childToPassOn)
                    AssignToTempParts( );
                    tempPart.Children[n].ProcessPart();
                }
            }
        } while (exit != true);
        return true;
    }

    public bool GetFirstUnaccountedForChild(string realChildName, List<TempPart> tempChildren, ref TempPart unaccontedChild) {
        if (tempChildren.FirstOrDefault(c => c.TempPartName == realChildName) != null) {
            unaccontedChild = tempChildren.FirstOrDefault(c => c.TempPartName == realChildName);
            return true;
        }
        else {
            return false;
        }
    }

    public bool TryGetChildByName(string childName, out Part child) {
        child = Children.FirstOrDefault(c => c.Name == childName);
        return child != null;
    }

    public bool TryGetMarker(string markerName, out Marker tempMarker) {
        return markerLookup.TryGetValue(markerName, out tempMarker);
    }

    
    public bool AddMarker(string markerName, double x, double y, double z) {
        if (markerLookup.ContainsKey(markerName))
            return false;

        markerLookup[markerName] = new Marker(x, y, z);
        return true;
    }

    public bool DeleteMarker(string markerName) {
        return markerLookup.Remove(markerName);
    }

    public Marker GetAbsoluteCoordinates(string markerName) {
        var local = markerLookup[markerName];
        return WorldTransform.Apply(local);
    }
}