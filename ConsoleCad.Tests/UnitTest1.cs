namespace ConsoleCad.Tests;

using System.Security;
using ConsoleCad.Logic;

[TestFixture]
/*
public class ConsoleCadTests {
	private ProjRunner proj;
	
    [SetUp]
	public void Setup() {
		private List<Parts> startingParts = new List<Parts> {
			var chairRootMarker = new Marker(2, -5, 0);
			new Part("Chair", chairRootMarker),
			var musicstandRootMarker = new Marker(-3, 40, 0);
			new Part("MusicStand", musicstandRootMarker),
			var lampRootMarker = new Marker(30, 30, 0);
			new Part("Lamp", lampRootMarker
			// Violinist is a child of chair
			// var violinistRootMarker = new Marker(2, -5, 70);
			// new Part("Violinist", { 2, -5, 70 })
		};

		// Starting
		var locationViewer = new Marker(-30, -30, 70);
		var depthCanSee1 = int 10;
		var depthCanSee2 = int 20
		proj = new ProjRunner("Seated Violinist", startingParts);
		proj.CreateViewer("Albert", locationViewer, depthCanSee1);
		proj.CreateViewer("Bob", locationViewer, depthCanSee2);
	}

	[Test]
	bool renderstart = proj.Start(wednesday);
	Assert.True(renderstart);
	Assert.Equal(2, game.Viewers[0].VisibleParts.Count);
	Assert.Equal(3, game.Viewers[1].VisibleParts.Count);

	int p = 0;

	p = 0;
	Assert.Equal(0, game.CurrentViewer);
	var tuesday = new Transform(0, 0, 3);
	bool movepart = game.MovePartWithChildren(tuesday); // Raise Chair
	Assert.True(movepart);
	var newLocation = new Marker(-30, -30, 73);
	Assert.Equal(newLocation, proj.Players[p].Hand.Count);

	//Check that the player cannot draw two cards in a trun
	drawcard = game.DrawCard(p);
	Assert.False(drawcard);
	Assert.Equal(6, game.Players[p].Hand.Count);

	//Check that only the active player can draw a card
	drawcard = game.DrawCard(0);
	Assert.False(drawcard);
	Assert.Equal(5, game.Players[1].Hand.Count);

	//Play a goblin card
	ICard? goblinCard = game.Players[0].Hand.FirstOrDefault(c => c.Name == "Goblin");
	Assert.NotNull(goblinCard);
	bool cardPlayed = game.PlayCard(goblinCard);
	Assert.True(cardPlayed);
	Assert.Single(game.Players[0].Board);

	//Creature can't attack firt turn
	ICreature? gobby = game.Players[0].Board.FirstOrDefault(c => c.Name == "Goblin");
	Assert.NotNull(gobby);
	bool creatureAttacked = game.Attack(gobby, game.Players[1]);
	Assert.False(creatureAttacked);
	game.EndTurn();

	//Player 1
	p = 1;
	Assert.Equal(1, game.CurrentPlayer);
	drawcard = game.DrawCard(p);
	Assert.True(drawcard);

	goblinCard = game.Players[1].Hand.FirstOrDefault(c => c.Name == "Goblin");
	Assert.NotNull(goblinCard);
	cardPlayed = game.PlayCard(goblinCard);
	Assert.True(cardPlayed);
	game.EndTurn();

	//Player 0
	p = 0;
	drawcard = game.DrawCard(p);
	Assert.True(drawcard);

	gobby = game.Players[0].Board.FirstOrDefault(c => c.Name == "Goblin");
	Assert.NotNull(gobby);
	creatureAttacked = game.Attack(gobby, game.Players[1]);
	Assert.True(creatureAttacked);
	Assert.Equal(9, game.Players[1].Life);

	ICard? trollCard = game.Players[0].Hand.FirstOrDefault(c => c.Name == "Troll");
	Assert.NotNull(trollCard);
	cardPlayed = game.PlayCard(trollCard);
	Assert.True(cardPlayed);
	game.EndTurn();

	//Player 1
	p = 1;
	drawcard = game.DrawCard(p);
	Assert.True(drawcard);

	gobby = game.Players[1].Board.FirstOrDefault(c => c.Name == "Goblin");
	Assert.NotNull(gobby);
	var gobby2 = game.Players[0].Board.FirstOrDefault(c => c.Name == "Goblin");
	Assert.NotNull(gobby2);
	creatureAttacked = game.Attack(gobby, gobby2);
	Assert.True(creatureAttacked);
	Assert.Single(game.Players[0].Board);
	Assert.Empty(game.Players[1].Board);

	goblinCard = game.Players[1].Hand.FirstOrDefault(c => c.Name == "Goblin");
	Assert.NotNull(goblinCard);
	cardPlayed = game.PlayCard(goblinCard);
	Assert.True(cardPlayed);
	game.EndTurn();

	

	//There Can only be one viewer at a time, 
	// so multiple people dont make conflicting edits
	[Test]
    public void ActiveViewerCanMoveRootPart()
    {
        proj.SetCurrentViewer("Alice");
        var transform = new Transform(0, 0, 10);
        bool moved = proj.MovePartWithChildren(chair, transform);
        Assert.IsTrue(moved);
        Assert.AreEqual(10, chair.Position.Z);
        Assert.AreEqual(10, violinist.Position.Z); // child moves with root
    }

    [Test]
    public void MovingChildMovesOnlySubtree()
    {
        proj.SetCurrentViewer("Alice");
        var transform = new Transform(0, 0, 5);
        bool moved = proj.MovePartWithChildren(music, transform);
        Assert.IsTrue(moved);
        Assert.AreEqual(5, music.Position.Z);
        Assert.AreEqual(5, paperclip.Position.Z);
        Assert.AreEqual(0, musicStand.Position.Z); // root unaffected
    }

    [Test]
    public void NonActiveViewerCannotMoveParts()
    {
        proj.SetCurrentViewer("Alice");
        proj.SetCurrentViewer("Bob"); // Bob is now active
        var transform = new Transform(0, 0, 10);
        bool moved = proj.MovePartWithChildren(chair, transform);
        Assert.IsFalse(moved);
        Assert.AreEqual(0, chair.Position.Z); // unchanged
    }

    [Test]
    public void MovingOneRootDoesNotAffectOtherRoots()
    {
        proj.SetCurrentViewer("Alice");
        var transform = new Transform(0, 0, 5);
        proj.MovePartWithChildren(chair, transform);
        Assert.AreEqual(5, chair.Position.Z);
        Assert.AreEqual(0, musicStand.Position.Z); // independent root
    }
    }
}
//*/