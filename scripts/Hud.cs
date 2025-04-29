using Godot;
using System;
using System.ComponentModel.DataAnnotations;

public partial class Hud : CanvasLayer
{

	[Signal]
	public delegate void StartGameEventHandler();
	[Signal]
	public delegate void PauseGameEventHandler();
	private static Control _control;
	private static Button _backButton;
	private static VBoxContainer _mainMenu;
	public override void _Ready()
	{
		_control = GetNode<Control>("Control");
		_mainMenu = _control.GetNode<VBoxContainer>("MainMenu");
		_backButton = _control.GetNode<Button>("BackButton");
		defaultTitleScreen();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("escapeKey") && !_control.GetNode<Label>("Credits").Visible && _mainMenu.Visible)
		{
			GetTree().Quit();
		}
		else if (Input.IsActionJustPressed("escapeKey") && _control.GetNode<Label>("Credits").Visible)
		{
			defaultTitleScreen();
		}
		else if (Input.IsActionJustPressed("escapeKey") && !_mainMenu.Visible)
		{
			defaultTitleScreen();
			EmitSignal(SignalName.PauseGame);

		}

	}

	private void OnStartButtonPressed()
	{
		_backButton.Show();
		EmitSignal(SignalName.StartGame);
		GD.Print("Start Game");

	}

	private void OnExitButtonPressed()
	{
		GetTree().Quit();

	}
	private void OnCreditButtonPressed()
	{
		_mainMenu.Hide();
		// buttonContainer.GetNode<Button>("StartButton").Hide();
		// buttonContainer.GetNode<Button>("ExitButton").Hide();
		// buttonContainer.GetNode<Button>("CreditButton").Hide();
		// mainMenu.GetNode<Label>("Title").Hide();
		_backButton.Show();
		_control.GetNode<Label>("Credits").Show();



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
		_backButton.Hide();
		// mainMenu.GetNode<Label>("Title").Show();
		// control.GetNode<Label>("Credits").Hide();
		_mainMenu.Show();
		_control.GetNode<Label>("Credits").Hide();

	}
	public static void HideMainMenu()
	{
		_mainMenu.Hide();
		_control.GetNode<Label>("Credits").Hide();

	}

}
