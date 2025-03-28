using Godot;
using System;

public partial class Cat : Node2D
{

	[Export]
	public int Speed { get; set; } = 50; // How fast the cats move
	public Vector2 Direction;
	public Vector2 ScreenSize;
	private const float MIN_ANGLE1 = 325f;  // 325° to 45° range
    private const float MAX_ANGLE1 = 45f;
    private const float MIN_ANGLE2 = 135f;  // 135° to 225° range
    private const float MAX_ANGLE2 = 225f;
	private Random random = new Random();
	private Timer MovementTimer;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		ScreenSize = GetViewportRect().Size;
		Position = new Vector2(random.Next(0, (int)ScreenSize.X), random.Next(0, (int)ScreenSize.Y));
		OnMovementTimerTimeOut();
		MovementTimer = new Timer();
		AddChild(MovementTimer);
		MovementTimer.WaitTime = 20;
		MovementTimer.Timeout += OnMovementTimerTimeOut;
		MovementTimer.Start();



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

		if (random.Next(0, 7) == 0)
		{
			Speed = 0;
		}
		else
		{
			Speed = 50;
			float angle;
			if (random.NextDouble() > 0.5)
			{
				angle = (float)(random.NextDouble() * (MAX_ANGLE1 - MIN_ANGLE1) + MIN_ANGLE1);
			}
			else
			{
				angle = (float)(random.NextDouble() * (MAX_ANGLE2 - MIN_ANGLE2) + MIN_ANGLE2);
			}

			// Convert angle to radians
			float angleInRadians = Mathf.Pi / 180f * angle;

			Direction = new Vector2(Mathf.Cos(angleInRadians), Mathf.Sin(angleInRadians)).Normalized();
		}
		GD.Print(Speed);
	}
}
