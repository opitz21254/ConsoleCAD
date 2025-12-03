using NUnit.Framework;
using ConsoleCad.Logic;

namespace ConsoleCad.Tests {
    [TestFixture]
    public class ConsoleCadTests {
        private Part desk;
        private Part drawer;
        private Part drawerHandle;
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

            // Add markers to parts (relative coordinates)
            desk.AddMarker("Bottom", 0, 0, 1);
            drawer.AddMarker("Front", 0, 0, 0);
            drawerHandle.AddMarker("Grip", 0, 0, 0);
        }

        // --------------------------------------------------------
        // -1. Does something
        // --------------------------------------------------------
        [Test]
        public void DoesSomething() {
            List<string> result = new List<string>();
            TempPart tempDesk = new TempPart("Desk");

            bool success = PartExtensions.AssignToTempParts(desk, tempDesk, ref result);
            success.Equals(true);
        }

        // --------------------------------------------------------
        // 0. ReturnAllChildren returns a string List to nth gen
        // --------------------------------------------------------
        [Test]
        public void ReturnAllChildrenWorks() {
            List<string> desksChildren = desk.ReturnAllChildren();
            var ans = new List<string> { "Desk", "Drawer", "Handle" };
            Assert.That(desksChildren, Is.EqualTo(ans));
        }
        /*

        // --------------------------------------------------------
        // 1. Root world coordinates should match transform offset
        // --------------------------------------------------------
        [Test]
        public void RootHasCorrectWorldCoordinates()
        {
            var world = desk.WorldTransform.Offset;
            Assert.Equals(2, world.X);
            Assert.Equals(-5, world.Y);
            Assert.Equals(0, world.Z);
        }

        // --------------------------------------------------------
        // 2. Child inherits parent transform
        // --------------------------------------------------------
        [Test]
        public void ChildInheritsParentTransform()
        {
            var handleWorld = drawerHandle.WorldTransform.Offset;

            Assert.Equals(2, handleWorld.X);   // chair.X
            Assert.Equals(-5 + 5, handleWorld.Y); // chair.Y + offset
            Assert.Equals(0 + 10 + 2, handleWorld.Z);
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
