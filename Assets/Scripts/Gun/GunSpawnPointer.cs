 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GunSpawnPointer : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		Aim();
		
	}

	public void Aim(){
		
		//Always aims at the center of the screen
		float screenX = Screen.width/2;
		float screenY = Screen.height/2;
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(new Vector3(screenX,screenY));
  		if (Physics.Raycast(ray, out hit))
  		transform.LookAt(hit.point);

		  //DESTROY ON COLLISION
	}
}
