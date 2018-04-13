using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ore : MonoBehaviour {
	private float xp;
	public PlayerInfo playerInfo;
	public float amount;
	public float currentAmount;
	public int ticks;
	public int currentTicks;
	public bool depleted;
	public enum OreType {
		Bronze,
		Iron,
	}
	public OreType oreDropped;
	// Use this for initialization
	void Start () {
		currentTicks = ticks;
		currentAmount = amount;
		depleted = false;
		playerInfo = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInfo>();
		//Sets xp based on ore
		switch(oreDropped){
			case OreType.Bronze:
				xp = 1;
				break;
			case OreType.Iron:
				xp = 5;
				break;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Mined(){
		foreach(Skill x in playerInfo.skills){
			if(x.skillName.ToLower()=="mining"){
				x.addXp(xp);
				break;
			}
		}
		currentTicks = ticks;
		currentAmount-= 1;
		if(currentAmount<=0){
			depleted = true;
		}
	}

	public void Action(Pickaxe pickaxe){
		if (pickaxe.tier >= getOreTier() && !depleted){
			float x = Random.Range(0,100)/100f;
			if(x < pickaxe.efficiency){
				currentTicks -=1;
			}
		}
		if(currentTicks<=0){
			Mined();
		}
	}

	public int getOreTier(){
		switch(oreDropped){
			case OreType.Bronze:
				return 1;
			case OreType.Iron:
				return 2;
			default:
				return 99;
		}
		
	}
}
