using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInfo : MonoBehaviour {
	public Item item;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void setItem(Item item){
		//Used to set the reference of this item when picking it up or using it again
		this.item = new Item(item);
	}

	public void Equip(){
		//Tells our item that it has been equipped (Can use item specific scripts to do something with it Ex: Pistol script)
		BroadcastMessage("Equipped", SendMessageOptions.DontRequireReceiver);
	}
}
