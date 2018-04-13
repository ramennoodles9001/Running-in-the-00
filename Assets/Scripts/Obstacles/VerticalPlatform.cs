using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalPlatform : MonoBehaviour {
	public float distance, velocity=1f, stillDuration=2f;
	private float timer, distanceTraveled;
	private bool atStart, traveling;
	private Vector3 startHeight;
	// Use this for initialization
	void Start () {
		startHeight = transform.position;
		timer = stillDuration;
		atStart = true;
	}
	
	void FixedUpdates(){
		
	}

	// Update is called once per frame
	void Update () {
		//Sits for a certain amount of time
		if(!traveling){
			timer -= Time.deltaTime;
			if (timer <=0){
				distanceTraveled = 0;
				traveling = true;
				startHeight = transform.position;
			}
		}
		if (traveling){
			if (atStart){
				//Goes up
				transform.Translate(Vector3.up*Time.deltaTime*velocity);
			}else{
				//Goes down
				transform.Translate(Vector3.down*Time.deltaTime*velocity);
			}
			distanceTraveled = Vector3.Distance(transform.position,startHeight);
			//once we have travelled the distance
			if (distanceTraveled >= distance){
				traveling=false;
				atStart = !atStart;
				timer = stillDuration;
				distanceTraveled = 0;
			}
		}
	}
}
