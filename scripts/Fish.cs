using Godot;
using System;

public partial class Fish : RigidBody2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		this.ZIndex = 1;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	// We also specified this function name in PascalCase in the editor's connection window.
	private void OnVisibleOnScreenNotifier2DScreenExited()
	{
		QueueFree();
	}
	public void FilpFish(bool H, bool V)
	{
		Sprite2D fish = GetNode<Sprite2D>("Sprite2D");
		fish.FlipH = H;
		fish.FlipV = V;
	}
}
