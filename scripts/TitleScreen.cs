using Godot;
using System;

public partial class TitleScreen : CanvasLayer
{

	[Signal]
	public delegate void StartGameEventHandler();

	public override void _Ready()
	{
		defaultTitleScreen();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("escapeKey") && !GetNode<Label>("Credits").Visible)
		{
			GetTree().Quit();
		}
		else if (Input.IsActionJustPressed("escapeKey") && GetNode<Label>("Credits").Visible)
		{
			defaultTitleScreen();
		}

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
	private void OnCreditButtonPressed()
	{

		GetNode<Button>("StartButton").Hide();
		GetNode<Button>("ExitButton").Hide();
		GetNode<Button>("CreditButton").Hide();
		GetNode<Button>("BackButton").Show();
		GetNode<Label>("Title").Hide();
		GetNode<Label>("Credits").Show();



	}
	private void OnBackButtonPressed()
	{
		defaultTitleScreen();
	}

	private void defaultTitleScreen()
	{
		GetNode<Button>("StartButton").Show();
		GetNode<Button>("ExitButton").Show();
		GetNode<Button>("CreditButton").Show();
		GetNode<Button>("BackButton").Hide();
		GetNode<Label>("Title").Show();
		GetNode<Label>("Credits").Hide();

	}

}
