using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class Item{
	public enum ItemType{
		Gun,
		Sword,
		Consumable,
		Pickaxe,
		Misc
	}
	public enum BackgroundType{
		Default
	}
	public string name;
	public int id;
	public ItemType type;
	public GameObject prefab;
	public Sprite icon;
	public bool stackable;
	public int quantity;
	public int maxstack;
	public BackgroundType backgroundType;
	public OptionPanel.Options[] options;
	public Item(string name, int id, ItemType type, GameObject prefab, Sprite icon, bool stackable, int quantity,int maxstack, BackgroundType backgroundType, OptionPanel.Options[] options){
		this.name = name;
		this.id = id;
		this.type = type;
		this.prefab = prefab;
		this.icon = icon;
		this.stackable = stackable;
		this.quantity = quantity;
		this.maxstack = maxstack;
		this.backgroundType = backgroundType;
		this.options = options;
		
	}

	public Item(Item i){
		this.name = i.name;
		this.id = i.id;
		this.type = i.type;
		this.prefab = i.prefab;
		this.icon = i.icon;
		this.stackable = i.stackable;
		this.quantity = i.quantity;
		this.maxstack = i.maxstack;
		this.backgroundType = i.backgroundType;
		this.options = i.options;
	}
}
