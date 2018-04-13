using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour {

	public Dialogue dialogue;
	public GameObject player;
	public DialogueManager dm;
	public string NPCName;
	// Use this for initialization
	void Start () {
		dialogue = gameObject.GetComponent<Dialogue>();
		player = GameObject.FindGameObjectWithTag("Player");
		dm = GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<DialogueManager>();
	}
	
	// Update is called once per frame

	public void Interact(){
		if(dialogue!=null)
		dm.openDialogue(dialogue, NPCName);
	}

}
