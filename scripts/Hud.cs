using Godot;
using System;

public partial class Hud : CanvasLayer
{

	[Signal]
	public delegate void StartGameEventHandler();
	private PanelContainer titleScreen;
	private VBoxContainer vBoxContainer;
	private VBoxContainer buttonContainer;
	public override void _Ready()
	{
		titleScreen = GetNode<PanelContainer>("TitleScreen");
		vBoxContainer = titleScreen.GetNode<VBoxContainer>("VBoxContainer");
		buttonContainer = vBoxContainer.GetNode<VBoxContainer>("ButtonContainer");
		defaultTitleScreen();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("escapeKey") && !titleScreen.GetNode<Label>("Credits").Visible && titleScreen.Visible)
		{
			GetTree().Quit();
		}
		else if (Input.IsActionJustPressed("escapeKey") && titleScreen.GetNode<Label>("Credits").Visible)
		{
			defaultTitleScreen();
		}
		else if (Input.IsActionJustPressed("escapeKey") && !titleScreen.Visible)
		{
			titleScreen.Show();
		}

	}

	private void OnStartButtonPressed()
	{
		GetNode<PanelContainer>("TitleScreen").Hide();
		EmitSignal(SignalName.StartGame);

	}

	private void OnExitButtonPressed()
	{
		GetTree().Quit();

	}
	private void OnCreditButtonPressed()
	{

		buttonContainer.GetNode<Button>("StartButton").Hide();
		buttonContainer.GetNode<Button>("ExitButton").Hide();
		buttonContainer.GetNode<Button>("CreditButton").Hide();
		vBoxContainer.GetNode<Label>("Title").Hide();
		titleScreen.GetNode<Button>("BackButton").Show();
		titleScreen.GetNode<Label>("Credits").Show();



	}
	private void OnBackButtonPressed()
	{
		defaultTitleScreen();
	}

	private void defaultTitleScreen()
	{
		buttonContainer.GetNode<Button>("StartButton").Show();
		buttonContainer.GetNode<Button>("ExitButton").Show();
		buttonContainer.GetNode<Button>("CreditButton").Show();
		titleScreen.GetNode<Button>("BackButton").Hide();
		vBoxContainer.GetNode<Label>("Title").Show();
		titleScreen.GetNode<Label>("Credits").Hide();

	}

}
