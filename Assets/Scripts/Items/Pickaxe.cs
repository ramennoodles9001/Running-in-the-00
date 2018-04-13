using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickaxe : Equippable {
	private PlayerControls playerControls;
	//Efficiency is the chance to mine an ore
	[Range(0.0f,1.0f)]
	public float efficiency = 0;
	//Tier decides what ore the pickaxe can mine
	[Range(0,10)]
	public int tier;
	// Use this for initialization
	void Start () {
		playerControls = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControls>();
	}
	
	// Update is called once per frame
	void Update () {
		
		if(Input.GetMouseButtonDown(0) && playerControls.allowInput && isEquipped){
			RaycastHit hit;
			//Debug.DrawRay(Camera.main.transform.position,Camera.main.transform.TransformDirection(Vector3.forward),Color.black,15F);
			if(Physics.Raycast(Camera.main.transform.position,Camera.main.transform.TransformDirection(Vector3.forward), out hit, 10f)){
				if(hit.transform.gameObject.GetComponent<Ore>() != null){
					hit.transform.gameObject.GetComponent<Ore>().Action(this);
				}
			}
		}
	}
}
