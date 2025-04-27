using Godot;
using System;

public partial class UIManager : Control
{	
	private PlayerInventory playerInventory;

	CanvasItem fireAxeImg;
	CanvasItem wetCloth;
	CanvasItem fireExtinguisherImg;

  public override void _Ready()
  {
		playerInventory = GetNode<PlayerInventory>("/root/PlayerInventory");
		playerInventory.ItemAdded += EnableItemUI;

		fireAxeImg = GetNode<CanvasItem>("./ToolContainer/FireAxeImage");
		wetCloth = GetNode<CanvasItem>("./ToolContainer/WetClothImage");
		fireExtinguisherImg = GetNode<CanvasItem>("./ToolContainer/FireExtinguisherImage");

		fireAxeImg.Visible = false;
		wetCloth.Visible = false;
		fireExtinguisherImg.Visible = false;
  }

	public void EnableItemUI(int item){
		ItemTypes type = (ItemTypes)Enum.ToObject(typeof(ItemTypes), item);
		
		switch(type){
			case ItemTypes.FireAxe:{
				fireAxeImg.Visible = true;
				break;
			}
			case ItemTypes.WetCloth:{
				wetCloth.Visible = true;
				break;
			}
			case ItemTypes.FireExtinguisher:{
				fireExtinguisherImg.Visible = true;
				break;
			}
		}
	}
}
