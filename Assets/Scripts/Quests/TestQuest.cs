using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestQuest : Quest {

	// Use this for initialization
	public GameObject player;
	void Start () {
		questName = "Test Quest";
		tasks.Add(false);//Equip glock10
		tasks.Add(false);//Kill 5 enemies
		tasks.Add(false);//Talk to NPC
		player = GameObject.FindGameObjectWithTag("Player");
	}

	
	


	

	
	// Update is called once per frame
	void Update () {
		if(!tasks[0]){
			if (player.GetComponent<Inventory>().equippedWeapon.id == 1){
				tasks[0] = true;	
			}
		}
	}
}
