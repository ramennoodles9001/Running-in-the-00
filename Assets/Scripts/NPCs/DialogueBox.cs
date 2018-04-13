using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class DialogueBox{
	public string speech;
	public int id;
	public DialogueOption[] options;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public DialogueBox(string id, string speech, DialogueOption[] options){
		this.speech = speech;
		this.id = int.Parse(id);
		this.options = options;

	}
}
