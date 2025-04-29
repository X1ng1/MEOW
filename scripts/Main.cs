using Godot;
using System;

public partial class Main : Node
{
	[Export]
    private PackedScene CatsScene { get; set; }
	public PackedScene FishScene { get; set; }
	private TextureRect background;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		background = GetNode<TextureRect>("Background");
		background.Texture = ResourceLoader.Load<Texture2D>("res://assets/pictures/tempTitleScreen.png");
		Hud.defaultTitleScreen();
		for (int i = 0; i < 10; i++)
		{
			var new_cat = Cat.CreateCat("cats" + i ,"sd");
			AddChild(new_cat);

		}
		Cat.HideCats();

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}

	private void OnHudStartGame()
	{
		Hud.HideMainMenu();
		Cat.ShowCats();
		background.Texture = ResourceLoader.Load<Texture2D>("res://assets/pictures/Home.jpg");
	}
	private void OnHudPauseGame()
	{
		Cat.HideCats();
		background.Texture = ResourceLoader.Load<Texture2D>("res://assets/pictures/tempTitleScreen.png");
	}
}
