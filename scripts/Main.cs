using Godot;
using System;

public partial class Main : Node
{
	[Export]
    public PackedScene CatsScene { get; set; }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		for (int i = 0; i < 10; i++)
		{
			var new_cat = Cat.CreateCat("cat" + i, "sjda");
			AddChild(new_cat);

			// Node catInstance = CatsScene.Instantiate();

			// if (catInstance is Cat cat)
			// {
			// 	cat.CatName = $"Mob_{i}";
			// 	cat.CatDescription = $"This is mob number {i}";
			// }

			// AddChild(catInstance);
		}

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}

	private void OnHudStartGame()
	{
	}
	private void OnHudPauseGame()
	{

	}
}
