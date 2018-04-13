using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerInfo : MonoBehaviour
{

    public float maxHealth=100, health = 100, stamina = 100, maxStamina =100, stamina_regen = 5, time = 1;
    private PlayerControls pc;
	private Rigidbody rb;
	public float dodgeStamina=33;
    private PlayerHUD hud;
    public Skill[] skills = new Skill[26];
    // Use this for initialization
    void Start()
    {
        skills[0] = new Mining();
        skills[0].xp = 0;
        //skills[0].skillName = "mining";
        pc = gameObject.GetComponent<PlayerControls>();
		rb = gameObject.GetComponent<Rigidbody>();
        hud = gameObject.GetComponent<PlayerHUD>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.L)){
            skills[0].addXp(25f);
            Debug.Log(skills[0]);
        }
        
        time -= Time.deltaTime;
		if (stamina < maxStamina){
        	if (time <= 0)
        	{
                //Time until stamina regens
            	time = 1;
				if(stamina+stamina_regen>maxStamina)
					stamina = maxStamina;
					else
           		stamina += stamina_regen;
                   UpdateInfo();
        	}
		}

       

    }

    public bool UseStamina(float amt)
    {
        if (stamina >= amt)
        {
            stamina -= amt;
            UpdateInfo();      
            return true;
           
        }
        else
        {
            UpdateInfo();
            return false;
        }
        
    }

    public void TakeDamage(float amt)
    {
        health -= amt;
        UpdateInfo();

    }

    public float getHealth()
    {
        return health;
    }

    public float getStamina()
    {
        return stamina;
    }

    public void UpdateInfo(){
        hud.displayHealth = health;
        hud.displayMaxHealth = maxHealth;
        hud.displayStamina = stamina;
        hud.displayMaxStamina = maxStamina;
    }
}
