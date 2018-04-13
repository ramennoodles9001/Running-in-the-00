using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarterQuest : Quest {
	
	// Use this for initialization
	void Start () {
		questName = "Saving [Insert Town Name]";
		tasks.Add(false); //Talk to NPC
		tasks.Add(false); //Obtain weapon of choice (Melee, Range, Magic)
		tasks.Add(false); //Get to castle and talk to queen
		tasks.Add(false); //Kill 20 monsters
		tasks.Add(false); //Talk to queen
		id = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
