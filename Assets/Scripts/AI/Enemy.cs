using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {
	public float health = 100;
	public GameObject bloodSplat;
	public Animator animator;
	public GameObject player;
	public NavMeshAgent agent;
	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
		player = GameObject.FindGameObjectWithTag("Player");
		agent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
		agent.SetDestination(player.transform.position);
		
	}

	public void takeDamage(float d){
		health -= d;
		Instantiate(bloodSplat,transform.position,Quaternion.identity);
		animator.SetTrigger("hit");
		if (health <= 0){
			Destroy(gameObject);
		}

	}

	void OnAnimatorMove(){
		transform.position = agent.nextPosition;
	}
}
