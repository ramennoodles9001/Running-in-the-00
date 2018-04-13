using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : MonoBehaviour {
	public GameObject gamemanager;
	public int questID;
	// Use this for initialization
	void Start () {
		gamemanager=GameObject.FindGameObjectWithTag("GameManager");

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void startQuest(){
		gamemanager.GetComponent<QuestList>().BeginQuest(questID);
	}
}
