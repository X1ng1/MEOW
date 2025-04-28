using Godot;
using System;
using System.Collections.Generic;
public partial class Cat : CharacterBody2D
{
	[Export] public int Speed { get; set; } = 50; // How fast the cats move

	[Export] public string CatName { get; set; }
	[Export] public string CatDescription { get; set; }

	private static List<Cat> _allCats = new List<Cat>();
	private static PackedScene CatScene = (PackedScene)ResourceLoader.Load("res://scenes/Cat.tscn");
	private Random _random = new Random();
	private Vector2 _direction;
	private Vector2 _screenSize;
	private Timer _movementTimer;
	private AnimatedSprite2D _animatedSprite2D;

	public static Cat CreateCat(string name, string description)
	{
		Cat newCat = CatScene.Instantiate() as Cat;
		newCat.CatName = name;
		newCat.CatDescription = description;
		_allCats.Add(newCat);
		GD.Print(name);
		return newCat;
	}
	public static void HideCats()
	{
		_allCats.ForEach(cat => cat.Visible = false);
	}
	public static void ShowCats()
	{
		_allCats.ForEach(cat => cat.Visible = true);
	}
	public static void setCatSpeed(string catName, int speed, int catID)
	{
		GD.Print(catName);
		Cat cat = _allCats[catID];
		if (cat == null)
		{
			GD.Print("Cat not found");
			return;
		}
		if (cat != null && speed > 0)
		{
			GD.Print("should be moving");
			cat.Speed = speed;
			cat._animatedSprite2D.Play();
		}
		else
		{
			cat.Speed = 0;
			cat._animatedSprite2D.Stop();
		}
	}
	public static void setCatName(string catName, int catID)
	{
		Cat cat = _allCats[catID];
		if (cat == null)
		{
			GD.Print("Cat not found");
			return;
		}
		cat.CatName = catName;
		cat.GetNode<Label>("Name").Text = catName;
	}
	public override void _Ready()
	{
		_direction = RandomDirection(330, 30, 150, 210);
		_screenSize = GetViewportRect().Size;
		Position = new Vector2(_random.Next(0, (int)_screenSize.X), _random.Next(0, (int)_screenSize.Y));
		_movementTimer = new Timer();
		AddChild(_movementTimer);
		_movementTimer.Timeout += OnMovementTimerTimeOut;
		_movementTimer.Start();
		_animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		//GD.Print(_animatedSprite2D);
		GetNode<Label>("Name").Text = CatName;
	}

	public override void _PhysicsProcess(double delta)
	{

		if (Speed > 0)
		{
			_animatedSprite2D.Play("cat");
		}
		else
		{
			_animatedSprite2D.Stop();
		}
		Position += _direction * Speed * (float)delta;

		if (Position.X < 0 || Position.X > _screenSize.X)
		{
			_direction.X = -_direction.X;
		}
		if (Position.Y < 0 || Position.Y > _screenSize.Y)
		{
			_direction.Y = -_direction.Y;
		}
		_animatedSprite2D.FlipH = _direction.X < 0;

	}
	private void OnMovementTimerTimeOut()
	{
		_movementTimer.WaitTime = _random.Next(5, 60);
		//GD.Print(_movementTimer.WaitTime);
		_movementTimer.Start();
		//GD.Print(_movementTimer.TimeLeft);

		if (_random.Next(0, 9) == 0)
		{
			Speed = 0;
		}
		else
		{
			Speed = 50;
			_direction = RandomDirection(330, 30, 150, 210);
		}
	}

	private void OnClick(Node viewport, InputEvent @event, int shapeIdx)
	{

		if (@event is InputEventMouseButton mouseEvent && mouseEvent.Pressed && mouseEvent.ButtonIndex == MouseButton.Left)
		{
			GD.Print("Clicked on cat");
			Vector2I mousePos = (Vector2I)GetViewport().GetMousePosition();
			Popups.CatDataPopup(CatName, CatDescription, mousePos, _allCats.IndexOf(this));
			Speed = 0;
			_animatedSprite2D.Stop();
		}
	}


	private Vector2 RandomDirection(int minAngle1, int maxAngle1, int minAngle2, int maxAngle2)
	{
		float angle;
		if (_random.NextDouble() > 0.5)
		{
			angle = _random.Next(minAngle1, minAngle1 + maxAngle1);
			if (angle > 360)
			{
				angle = angle - 360;
			}
		}
		else
		{
			angle = _random.Next(minAngle2, maxAngle2);
		}
		// Convert angle to radians
		float angleInRadians = Mathf.Pi / 180f * angle;

		return new Vector2(Mathf.Cos(angleInRadians), Mathf.Sin(angleInRadians)).Normalized();
	}
}
