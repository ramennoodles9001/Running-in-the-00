using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestList : MonoBehaviour {

	public Quest[] quests;

	// Use this for initialization
	void Start () {
		quests = gameObject.GetComponents<Quest>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public Quest BeginQuest(int id){
		for(int x=0;x<quests.Length;x++){
			if(quests[x].id==id){
				quests[x].BeginQuest();
			}
		}
		return null;
	}

	
}
