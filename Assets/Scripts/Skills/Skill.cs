using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public abstract class Skill : ScriptableObject {

	public float level;
	public int maxLevel;
	public float xp;
	public string skillName;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void calculateLevel(){
		float x=0;
		float calcLevel = 1;
		//temp= ((1/8)*level*(level-1)) + 75*((Mathf.Pow(2,(level-1)/7)-1)/(1-Mathf.Pow(2f,-1f/7f)));
		//temp = Mathf.Pow(1.18f,level);
		//temp = Mathf.Log(xp,1.18f);
		//Starts at 50 xp (to be level 2)
		for(x=50; x< xp;){
			//Continiously adds this equation to get to the next level
			x +=100*Mathf.Pow(1.15f,calcLevel);
			calcLevel++;
		}
		level = calcLevel;
	}

	public void addXp(float amount){
		xp += amount;
		calculateLevel();
	}

}
