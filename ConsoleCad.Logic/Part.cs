
namespace ConsoleCad.Logic;

// Every part needs a root marker that all of the parts coordinates
// (stored as absolute coordinates) are baised off of.
public class Part {
    public string Name { get; }
    public Transform LocalTransform { get; private set; }

    public Part Parent { get; private set; }
    public List<Part> Children { get; } = new();

    private readonly Dictionary<string, Marker> markerLookup = new();
    public IReadOnlyDictionary<string, Marker> Markers => markerLookup;

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
        Children.Add(child);
        return child;
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