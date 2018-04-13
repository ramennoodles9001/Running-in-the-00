using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Quest : MonoBehaviour{
	public List<bool> tasks = new List<bool>();
	public bool active = false;
	public bool completed = false;
	public string questName;
	public int id;
	public Quest(){
		
	}

	public void BeginQuest(){
		active = true;
		Debug.Log("Quest started: " + questName);
	}
	public void EndQuest(){
		active = false;
		completed = true;
		Reward();
		Debug.Log("Quest done");
	}

	public void Reward(){
		Debug.Log("Quest Reward");
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
