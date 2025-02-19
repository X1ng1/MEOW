using Godot;
using System;

public partial class TitleScreen : CanvasLayer
{
	// Called when the node enters the scene tree for the first time.

	[Signal]
	public delegate void StartGameEventHandler();

	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void OnStartButtonPressed()
	{
		Hide();
		EmitSignal(SignalName.StartGame);

	}

	private void OnExitButtonPressed()
	{
		GetTree().Quit();

	}

}
