using Godot;
using System;
using System.Runtime.Serialization;

public partial class FallingFish : Node
{
	// Called when the node enters the scene tree for the first time.
	[Export]
	public PackedScene FishScene { get; set; }
	[Export]
	public int CatSpeed { get; set; } = 1000;
	[Export]
	public int CatAcceleration { get; set; } = 400;
	[Export]
	public int CatFriction = 500;
	private Area2D _catBowl;
	private Vector2 _gamePanelSize;
	private Vector2 _gamePanelPosition;
	private Panel _gamePanel;
	private Vector2 _startPosition;
	private int _score = 0;


	// private Vector2 _velocity = Vector2.Zero;
	public override void _Ready()
	{
		Panel panel = GetNode<Panel>("Panel");
		_gamePanel = GetNode<Panel>("%GamePanel");
		_catBowl = GetNode<Area2D>("%CatBowl");
		_startPosition = GetNode<Marker2D>("%CatStartPosition").Position;
		Callable.From(() => // This will be called after the scene tree is ready and the nodes are initialized
	   {
		   _gamePanelSize = _gamePanel.Size;
		   _gamePanelPosition = _gamePanel.Position;
	   }).CallDeferred();


	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		var inputVelocity = Vector2.Zero;
		if (Input.IsActionPressed("move_right"))
		{
			inputVelocity.X += 1;
		}
		if (Input.IsActionPressed("move_left"))
		{
			inputVelocity.X -= 1;
		}
		
		inputVelocity = inputVelocity.Normalized() * CatSpeed;
		

		// if (velocity != Vector2.Zero)
		// {
		//     // Accelerate towards the desired direction
		//     _velocity = _velocity.MoveToward(velocity * CatSpeed, CatAcceleration * (float)delta);
		// }
		// else
		// {
		//     // No input: apply friction (slow down gradually)
		//     _velocity = _velocity.MoveToward(Vector2.Zero, CatFriction * (float)delta);
		// }

		Vector2 minPos = _gamePanelPosition;
		Vector2 maxPos = _gamePanelPosition + _gamePanelSize;

		// Clamp the position of the cat bowl to the panel size
		_catBowl.Position += inputVelocity * (float)delta;
		//GD.Print(_catBowl.Position);
		//_catBowl.Position += _velocity * (float)delta;


		_catBowl.Position = new Vector2(
			x: Mathf.Clamp(_catBowl.Position.X, 0, _gamePanelSize.X),
			y: Mathf.Clamp(_catBowl.Position.Y, minPos.Y, maxPos.Y)
		);

	}

	private void OnFishTimerTimeout()
	{
		// Create a new instance of the fish scene.
		Fish fish = FishScene.Instantiate<Fish>();
		fish.FilpFish(GD.Randf() > 0.5, GD.Randf() > 0.5);

		// Choose a random location on Path2D.
		var fishSpawnLocation = GetNode<PathFollow2D>("%FishFallingPath/PathFollow2D");
		fishSpawnLocation.ProgressRatio = GD.Randf();

		// Set the fish's position to a random location.
		fish.Position = fishSpawnLocation.Position;

		// Choose the velocity.
		var velocity = new Vector2(0, (float)GD.RandRange(150.0, 250.0));
		fish.LinearVelocity = velocity;

		// Spawn the fish by adding it to the Main scene.
		_gamePanel.AddChild(fish);
	}

	private void FishEnteredExitZone(Node body)
	{
		if (body is Fish fish)
		{
			// Remove the fish from the scene.
			fish.QueueFree();
		}
		else
		{
			GD.Print("Not a fish");
		}
	}
	private void CaughtFishZone(Node body)
	{
		if (body is Fish fish)
		{
			// Remove the fish from the scene.
			fish.QueueFree();
			updateScore(_score += 10);
		}
		else
		{
			GD.Print("Not a fish");
		}
	}
	public void updateScore(int score)
	{
		// Update the score label.
		Label scoreLabel = GetNode<Label>("%ScoreLabel");
		scoreLabel.Text = "Score: " + score;
	}
	public void StartGame()
	{
		// Start the fish timer.
		_catBowl.Position = _startPosition;
		GetNode<Timer>("%FishTimer").Start();
		_score = 0; // Reset the score label.

	}
	
}
