using NUnit.Framework;
using ConsoleCad.Logic;

namespace ConsoleCad.Tests {
    [TestFixture]
    public class ConsoleCadTests {
        private Part desk;
        private Part drawer;
        private Part drawerHandle;
        private Part drawerGrip;
        private Part lamp;
        private Transform up3 = new Transform(0, 0, 3);

        [SetUp]
        public void Setup() {
            // Root parts
            desk = new Part("Desk", new Transform(2, -5, 0));
            lamp = new Part("Lamp", new Transform(30, 30, 0));

            // Drawer is child of chair
            drawer = desk.AddChild("Drawer", new Transform(0, 0, 10));

            // Handle is child of drawer
            drawerHandle = drawer.AddChild("Handle", new Transform(0, 5, 2));

            drawerGrip = drawerHandle.AddChild("Grip", new Transform(0, 0, 0));

            // Add markers to parts (relative coordinates)
            desk.AddMarker("Bottom", 0, 0, 1);
            drawer.AddMarker("Front", 0, 0, 0);
            drawerHandle.AddMarker("Grip", 0, 0, 0);
        }

        // --------------------------------------------------------
        // 0. ReturnAllChildren returns a string List to nth gen
        // --------------------------------------------------------
        [Test]
        public void DoesSomething() {
            IEnumerable<string> result = PartExtensions.ReturnAllChildren(desk);
            var desksChildren = new List<string> { "Desk", "Drawer", "Handle", "Grip" };
            Assert.That(result.ToList(), Is.EqualTo(desksChildren));
        }

        // --------------------------------------------------------
        // 1. Root world coordinates should match transform offset
        // --------------------------------------------------------
        [Test]
        public void RootHasCorrectWorldCoordinates()
        {
            // Offset is a property of Transform.cs that is just a marker storing
            // the "root location" of the part.
            Marker deskOffset = desk.GetAbsoluteCoordinates().Offset;;
            Assert.That(deskOffset.X, Is.EqualTo(2));
            Assert.That(deskOffset.Y, Is.EqualTo(-5));
            Assert.That(deskOffset.Z, Is.EqualTo(0));
        }

        // --------------------------------------------------------
        // 2. Child inherits parent transform
        // --------------------------------------------------------
        [Test]
        public void ChildInheritsParentTransform()
        {
            var handleWorld = drawerHandle.GetAbsoluteCoordinates().Offset;

            Assert.That(handleWorld.X, Is.EqualTo(2 + 0 + 0));   // chair.X
            Assert.That(handleWorld.Y, Is.EqualTo(-5 + 0 + 5)); // chair.Y + offset
            Assert.That(handleWorld.Z, Is.EqualTo(0 + 10 + 2));
        }

        // --------------------------------------------------------
        // 3. Moving a root moves entire subtree
        // --------------------------------------------------------
        [Test]
        public void MovingRootMovesChildren()
        {
            desk.Move(up3);

            var handle = drawerHandle.WorldTransform.Offset;

            Assert.Equals(0 + 3, handle.Z);   // entire subtree raised
        }

        /*
        // --------------------------------------------------------
        // 4. Moving a child does NOT affect parent or siblings
        // --------------------------------------------------------
        [Test]
        public void MovingChildDoesNotMoveParent()
        {
            drawer.Move(up3);

            // Parent chair should not change
            Assert.Equals(0, desk.WorldTransform.Offset.Z);

            // But drawer + handle should
            Assert.Equals(10 + 3, drawer.WorldTransform.Offset.Z);
            Assert.Equals(12 + 3, drawerHandle.WorldTransform.Offset.Z);
        }

        // --------------------------------------------------------
        // 5. Root parts do not affect each other
        // --------------------------------------------------------
        [Test]
        public void RootsAreIndependent()
        {
            desk.Move(up3);
            Assert.Equals(0, lamp.WorldTransform.Offset.Z);
        }

        // --------------------------------------------------------
        // 6. Marker world coordinates are computed correctly
        // --------------------------------------------------------
        [Test]
        public void MarkerWorldCoordinatesAreCorrect()
        {
            var worldSeat = desk.GetAbsoluteCoordinates("Seat");
            // Seat is (0,0,1) relative to chair, which is (2,-5,0)
            Assert.Equals(2, worldSeat.X);
            Assert.Equals(-5, worldSeat.Y);
            Assert.Equals(1, worldSeat.Z);
        }
    */
    }
}
