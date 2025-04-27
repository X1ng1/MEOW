using Godot;
using System;
using System.Net;

public partial class Popups : Control
{
	private static PopupPanel catPopup;
	private static PopupPanel catDataPopup;
	private static Panel mouseCatcher;

	private static LineEdit catName;
	private static Label catDescriptionLabel;

	private static int catID;

	public override void _Ready()
	{
		catPopup = GetNode<PopupPanel>("%CatInfoPopup");
		catDataPopup = GetNode<PopupPanel>("%CatDataPopup");
		mouseCatcher = GetNode<Panel>("%TransparentMouseCatcher");
		mouseCatcher.Hide();
		catName = GetNode<LineEdit>("%CatName");
		catDescriptionLabel = GetNode<Label>("%CatDescription");

	}


	public static void CatDataPopup(string name, string description, Vector2I mousePos, int catId)
	{
		Rect2I rect = new Rect2I(mousePos, Vector2I.Zero);
		catName.Text = name;
		catDescriptionLabel.Text = description;
		// mouseCatcher.Visible = true;
		catDataPopup.Popup(rect);
		catID = catId;

	}

	public static void HideCatDataPopup()
	{
		catDataPopup.Hide();
		GD.Print("Cat Hide");
	}

	public static void CatDataPopupClosed()
	{
		GD.Print("Popup closed");
		Cat.setCatSpeed(catName.Text, 50, catID);
	}

	private void CatNameChange(string name)
	{
		catName.Text = name;
		Cat.setCatName(name, catID);
		
	}

	// private void OnMouseCatcherClick(InputEvent e)
	// {
	// 	GD.Print("Mouse Catcher movement");
	// 	if (e is InputEventMouseButton mouseButtonEvent && mouseButtonEvent.IsPressed() && mouseButtonEvent.ButtonIndex == MouseButton.Left)
	// 	{
	// 		GD.Print("Mouse Clicked");
	// 		HideCatDataPopup();
	// 		mouseCatcher.Visible = false;
	// 		Cat.setCatSpeed(catName.Text, 50);
	// 	}
	// }
}
