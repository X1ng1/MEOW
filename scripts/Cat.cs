using Godot;
using System;

public partial class Cat : Node2D
{

	[Export]
	public int Speed { get; set; } = 50; // How fast the cats move
	public Vector2 Direction;

	public Vector2 ScreenSize;
	private const float MinAngle1 = 325f;  // 325° to 45° range
    private const float MaxAngle1 = 45f;
    private const float MinAngle2 = 135f;  // 135° to 225° range
    private const float MaxAngle2 = 225f;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		ScreenSize = GetViewportRect().Size;
		Position = GetViewportRect().Size / 2;
		OnMovementTimerTimeOut();
		


	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Position += Direction * Speed * (float)delta;
		
		if (Position.X < 0 || Position.X > ScreenSize.X || Position.Y < 0 || Position.Y > ScreenSize.Y)
		{
			Direction = -Direction;
		 }
	}

	private void OnMovementTimerTimeOut()
	{
		GD.Print("sss");
		Random random = new Random();
		float angle = 0f;
		if (random.NextDouble() > 0.5)
		{
			angle = (float)(random.NextDouble() * (MaxAngle1 - MinAngle1) + MinAngle1);
		}
		else
		{
			angle = (float)(random.NextDouble() * (MaxAngle2 - MinAngle2) + MinAngle2);
		}

		// Convert angle to radians
		float angleInRadians = (Mathf.Pi / 180f) * angle;

		Direction = new Vector2(Mathf.Cos(angleInRadians), Mathf.Sin(angleInRadians)).Normalized();
	}
}
