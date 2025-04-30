using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class UIManager : Control
{
	private PlayerInventory playerInventory;
	private PlayerHealth playerHealth;

	CanvasItem fireAxeImg;
	CanvasItem wetCloth;
	CanvasItem fireExtinguisherImg;

	public string heartFullRes = "res://Sprites/heart/heart_full.png";
	public string heartEmptyRes = "res://Sprites/heart/heart_empty.png";

	public int heartCount = 3;
	public int currentHealth = 2;
	public List<TextureRect> heartList = new List<TextureRect>();

	public override void _Ready()
	{
		playerInventory = GetNode<PlayerInventory>("/root/PlayerInventory");
		playerInventory.ItemAdded += EnableItemUI;

		playerHealth = GetNode<PlayerHealth>("/root/PlayerHealth");
		playerHealth.OnHealthChange += SyncHealthUI;

		fireAxeImg = GetNode<CanvasItem>("./ToolContainer/FireAxeImage");
		wetCloth = GetNode<CanvasItem>("./ToolContainer/WetClothImage");
		fireExtinguisherImg = GetNode<CanvasItem>("./ToolContainer/FireExtinguisherImage");

		fireAxeImg.Visible = false;
		wetCloth.Visible = false;
		fireExtinguisherImg.Visible = false;

		var children = GetNode("./HealthContainer").GetChildren().ToList();
		foreach (var child in children)
		{
			if (child is TextureRect) heartList.Add((TextureRect)child);
		}

		SyncHealthUI(playerHealth.currentHealth);
	}

	public void EnableItemUI(int item)
	{
		ItemTypes type = (ItemTypes)Enum.ToObject(typeof(ItemTypes), item);

		switch (type)
		{
			case ItemTypes.FireAxe:
				{
					fireAxeImg.Visible = true;
					break;
				}
			case ItemTypes.WetCloth:
				{
					wetCloth.Visible = true;
					break;
				}
			case ItemTypes.FireExtinguisher:
				{
					fireExtinguisherImg.Visible = true;
					break;
				}
		}
	}

	public void SyncHealthUI(int health)
	{
		currentHealth = health;

		foreach (var heart in heartList)
		{
			heart.Texture = GD.Load<Texture2D>(heartEmptyRes);
			GD.Print("Test");
		}

		for(int i = 0; i < currentHealth; i++){
			if (i >= heartList.Count()) break;
			heartList[i].Texture = GD.Load<Texture2D>(heartFullRes);
		}
	}

}
