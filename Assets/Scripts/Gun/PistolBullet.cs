using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolBullet : MonoBehaviour {
	private GameObject gun;
	private float speed;
	private float range;
	private float time = 6;
	private Vector3 init;
	private float damage;
	// Use this for initialization
	void Start () {
		init = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		time -= Time.deltaTime;
		if (time<0){
			DestroyThis();
		}
		
		
	}
	

	public void set(GameObject gun, float speed, float range, float damage){
		this.gun = gun;
		this.speed = speed;
		this.range = range;
		this.damage = damage;
	}

	public void OnCollisionEnter(Collision other){
		if (Vector3.Distance(init,transform.position)>range){
			damage = 5;
		}
		if (other.transform.name == "Head"){
			other.gameObject.GetComponentInParent<Enemy>().takeDamage(damage*2);
		}else if(other.transform.GetComponentInParent<Enemy>()){
			other.gameObject.GetComponentInParent<Enemy>().takeDamage(damage);
		}
		
		DestroyThis();
	}

	public void DestroyThis(){
		Destroy(gameObject);
	}

	
}
