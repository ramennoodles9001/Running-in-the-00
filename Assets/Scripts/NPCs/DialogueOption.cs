using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class DialogueOption{
	public int nextId;
	public string text;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public DialogueOption(string nextId, string text){
		this.nextId=int.Parse(nextId);
		this.text=text;
	}
}
