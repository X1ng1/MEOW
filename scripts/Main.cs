using Godot;
using System;

public partial class Main : Node
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GetNode<Node2D>("Cat").Hide();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}

	private void OnHudStartGame()
	{
		GetNode<Node2D>("Cat").Show();
	}
	private void OnHudPauseGame()
	{
		GetNode<Node2D>("Cat").Hide();
	}
}
