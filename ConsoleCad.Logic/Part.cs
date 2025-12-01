using System.Dynamic;

namespace ConsoleCad.Logic;

// Every part needs a root marker that all of the parts coordinates
// (stored as absolute coordinates) are baised off of.
public class Part {
    public string PartName { get; }

    public Transform LocalTransform { get; private set; }

    public Part Parent { get; private set; }
    public List<Part> Children { get; } = new();

    private readonly Dictionary<string, Marker> markerLookup = new();
    public IReadOnlyDictionary<string, Marker> Markers => markerLookup;

    public Part(string partName, Transform transform) {
        PartName = partName;
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

    public Part AddChild(string name, Marker localOffset) {
        var child = new Part(name, new Transform(localOffset)) {
            Parent = this
        };
        Children.Add(child);
        return child;
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

    public Marker GetWorldMarker(string markerName) {
        var local = markerLookup[markerName];
        return WorldTransform.Apply(local);
    }

    public bool DuplicatePart(string markerName) {
         if (!markerLookup.TryGetValue(markerName, out var original))
             return false;

         // Create a new name
         string newName = markerName + "_copy";
         int n = 1;

         // Ensure unique name
         while (markerLookup.ContainsKey(newName))
             newName = $"{markerName}_copy{n++}";

        // Make the duplicate
        markerLookup[newName] = original;

        return true;
    }
}