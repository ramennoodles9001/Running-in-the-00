using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DialogueOptionButton : MonoBehaviour {
	public string text;
	public int nextId;
	
	// Use this for initialization
	void Start () {
		Button btn = GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void TaskOnClick(){
		GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<DialogueManager>().nextDialogue(nextId);
	}
}
