using Godot;
using System;
using System.ComponentModel.DataAnnotations;

public partial class Hud : CanvasLayer
{

	[Signal]
	public delegate void StartGameEventHandler();
	[Signal]
	public delegate void PauseGameEventHandler();
	private static Control control;
	private static Button backButton;
	private static VBoxContainer mainMenu;
	public override void _Ready()
	{
		control = GetNode<Control>("Control");
		mainMenu = control.GetNode<VBoxContainer>("MainMenu");
		backButton = control.GetNode<Button>("BackButton");
		defaultTitleScreen();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("escapeKey") && !control.GetNode<Label>("Credits").Visible && mainMenu.Visible)
		{
			GetTree().Quit();
		}
		else if (Input.IsActionJustPressed("escapeKey") && control.GetNode<Label>("Credits").Visible)
		{
			defaultTitleScreen();
		}
		else if (Input.IsActionJustPressed("escapeKey") && !mainMenu.Visible)
		{
			defaultTitleScreen();
			EmitSignal(SignalName.PauseGame);

		}

	}

	private void OnStartButtonPressed()
	{
		backButton.Show();
		EmitSignal(SignalName.StartGame);
		GD.Print("Start Game");

	}

	private void OnExitButtonPressed()
	{
		GetTree().Quit();

	}
	private void OnCreditButtonPressed()
	{
		mainMenu.Hide();
		// buttonContainer.GetNode<Button>("StartButton").Hide();
		// buttonContainer.GetNode<Button>("ExitButton").Hide();
		// buttonContainer.GetNode<Button>("CreditButton").Hide();
		// mainMenu.GetNode<Label>("Title").Hide();
		backButton.Show();
		control.GetNode<Label>("Credits").Show();



	}
	private void OnBackButtonPressed()
	{
		defaultTitleScreen();
		EmitSignal(SignalName.PauseGame);
	}

	public static void defaultTitleScreen()
	{

		// buttonContainer.GetNode<Button>("StartButton").Show();
		// buttonContainer.GetNode<Button>("ExitButton").Show();
		// buttonContainer.GetNode<Button>("CreditButton").Show();
		backButton.Hide();
		// mainMenu.GetNode<Label>("Title").Show();
		// control.GetNode<Label>("Credits").Hide();
		mainMenu.Show();
		control.GetNode<Label>("Credits").Hide();

	}
	public static void HideMainMenu()
	{
		mainMenu.Hide();
		control.GetNode<Label>("Credits").Hide();

	}

}
