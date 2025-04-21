using Godot;
using System;
using System.Net;

public partial class Popups : Control
{
	private static PopupPanel catPopup;
	private static PopupPanel catDataPopup;
	private static Panel mouseCatcher;

	private static Label catNameLabel;
	private static Label catDescriptionLabel;

	public override void _Ready()
	{
		catPopup = GetNode<PopupPanel>("%CatInfoPopup");
		catDataPopup = GetNode<PopupPanel>("%CatDataPopup");
		mouseCatcher = GetNode<Panel>("%TransparentMouseCatcher");
		catNameLabel = GetNode<Label>("%CatName");
		catDescriptionLabel = GetNode<Label>("%CatDescription");

	}
	public static void CatInfoPopup()
	{
		catPopup.Popup();
		GD.Print("Cat Info Popup");

	}

	public static void HideCatInfoPopup()
	{
		catPopup.Hide();
		GD.Print("Cat Hide");
	}

	public static void CatDataPopup(string name, string description)
	{
		catNameLabel.Text = name;
		catDescriptionLabel.Text = description;
		mouseCatcher.Visible = true;
		catDataPopup.Popup();
		GD.Print("Cat Data Popup");

	}

	public static void HideCatDataPopup()
	{
		catDataPopup.Hide();
		GD.Print("Cat Hide");
	}

	private void OnMouseCatcherClick(InputEvent e)
	{
		if (e is InputEventMouseButton mouseButtonEvent && mouseButtonEvent.IsPressed())
		{
			HideCatInfoPopup();
			HideCatDataPopup();
			GD.Print("Mouse Clicked");
			mouseCatcher.Visible = false;
		}
		else if (e is InputEventMouseMotion)
		{
			HideCatInfoPopup();
			HideCatDataPopup();
			GD.Print("Mouse Moved");
		}
	}
}
