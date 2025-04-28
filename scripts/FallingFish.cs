using Godot;
using System;

public partial class FallingFish : Node
{
	// Called when the node enters the scene tree for the first time.
	[Export]
	public PackedScene FishScene { get; set; }
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	// We also specified this function name in PascalCase in the editor's connection window.
private void OnfishTimerTimeout()
{
    // Create a new instance of the fish scene.
    Fish fish = FishScene.Instantiate<Fish>();

    // Choose a random location on Path2D.
    var fishSpawnLocation = GetNode<PathFollow2D>("Path2D/PathFollow2D");
    fishSpawnLocation.ProgressRatio = 0;

    // Set the fish's direction perpendicular to the path direction.
    float direction = fishSpawnLocation.Rotation + Mathf.Pi / 2;

    // Set the fish's position to a random location.
    fish.Position = fishSpawnLocation.Position;

    // Add some randomness to the direction.
    // direction += (float)GD.RandRange(-Mathf.Pi / 4, Mathf.Pi / 4);
    // fish.Rotation = direction;

    // Choose the velocity.
    var velocity = new Vector2((float)GD.RandRange(150.0, 250.0), 0);
    fish.LinearVelocity = velocity.Rotated(direction);

    // Spawn the fish by adding it to the Main scene.
    AddChild(fish);
}
}
