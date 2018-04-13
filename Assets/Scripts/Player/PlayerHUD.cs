using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
public class PlayerHUD : NetworkBehaviour {
	public Texture crosshair;
	private PlayerInfo pi;
	public float displayHealth{get; set;}
	public float displayMaxHealth{get; set;}
	public float displayStamina{get;set;}
	public float displayMaxStamina{get;set;}
	private Image healthBar, staminaBar;
	public Text targetHealth;
	public Transform target;
	Ray ray;
	// Use this for initialization
	void Start () {
		//Initiates stuff
		pi=gameObject.GetComponent<PlayerInfo>();
		displayHealth = pi.getHealth();
		displayMaxHealth = pi.maxHealth;
		healthBar = GameObject.Find("Healthbar").GetComponent<Image>();
		healthBar.type = Image.Type.Filled;
		healthBar.fillMethod = Image.FillMethod.Horizontal;

		displayStamina = pi.getStamina();
		displayMaxHealth = pi.maxStamina;
		staminaBar = GameObject.Find("Staminabar").GetComponent<Image>();
		staminaBar.type = Image.Type.Filled;
		staminaBar.fillMethod = Image.FillMethod.Horizontal;
		
  		
	}
	
	// Update is called once per frame
	void Update () {
		//Updates the ratio for stamina and health
		staminaBar.fillAmount = displayStamina/displayMaxStamina;
		healthBar.fillAmount = displayHealth/displayMaxHealth;
		RaycastHit hit;
		//Debug.DrawRay(new Vector3(Screen.width/2,Screen.height/2), Camera.main.transform.forward,Color.black,5);
		ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width/2,Screen.height/2));
		//If aiming at an enemy, display their health
		if (Physics.Raycast(ray, out hit)){
			if(hit.collider.gameObject.GetComponentInParent<Enemy>()){
				target = hit.collider.transform;
				targetHealth.text = hit.collider.gameObject.GetComponentInParent<Enemy>().health.ToString();
			}
		}
	}

	void OnGUI(){
		//Draws crosshair
		if(Input.GetMouseButton(1)){

		}else{
			GUI.DrawTexture(new Rect(Screen.width/2,Screen.height/2,crosshair.width,crosshair.height), crosshair);	
		}
	}
}
