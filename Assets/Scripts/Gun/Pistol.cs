using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
//Semi-automatic
public class Pistol : Equippable
{

    public float rpm;
    public float damage = 60, bullet_speed = 90;
    public AudioClip[] ac = new AudioClip[2];
    public float recoil=3, compensation=0f;
    public float zoom=10;
    public float range = 5;
    public float magazineSize=5, magazine=5, reserve=30;

    private float rps;
    private bool canshoot;
    private float timeLeft;
	private GameObject pistolBullet;
	public GameObject bullet;
	public Transform bulletSpawn;
	private Transform player;
    private Animator anim;
    
    private AudioSource audioSource;
    private PlayerControls playerControls;
    private float recoilRate, compensationRate, div;
    // Use this for initialization
    void Start()
    {
        canshoot = true;
        rps = 60/rpm ;
		pistolBullet = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameInfo>().pistolBullet;
		player = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = ac[0];
        playerControls = player.GetComponent<PlayerControls>();
        recoilRate =playerControls.getRecoilRate();
        compensationRate = playerControls.getCompensationRate();
        //Sets compensation to negatives
        if (compensation >0){
            compensation *= -1;
        }
        div = recoilRate/compensationRate;
        if (Mathf.Abs(compensation) >= recoil/div){
            Debug.LogError("Compensation is to big, recoil will probably be inversed. Please change the compensation to be below: " + recoil/div + " for the weapon: " + transform.name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //ADS
        if (Input.GetMouseButton(1)){
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView,playerControls.getDefaultFOV()-zoom,Time.deltaTime*8);
            anim.SetBool("aiming", true);
            playerControls.setExternalSpeed(0.5F);
            
        }else{
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView,playerControls.getDefaultFOV(),Time.deltaTime*8);
            anim.SetBool("aiming", false);
            playerControls.setExternalSpeed(1F);
        }
        if (Input.GetKeyDown(KeyCode.R) && isEquipped){
            if (reserve >= magazineSize){
                reserve -= magazineSize-magazine;
                magazine = magazineSize;
            }else{
                if (magazine == 0){
                    magazine = reserve;
                    reserve = 0;
                }else{
                    if (reserve>= magazineSize-magazine){
                        reserve -= magazineSize-magazine;
                        magazine = magazineSize;
                    }else{
                        magazine = reserve;
                        reserve = 0;
                    }
                }
            }
        }


        if (Input.GetMouseButtonDown(0) && player.GetComponent<PlayerControls>().allowInput && isEquipped)
        {
            Shoot();
        }

        //Allows for a delay for shooting, derived from rpm
        if (!canshoot)
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft < 0)
            {
                canshoot = true;
                playerControls.recovery(compensation);
            }
        }

    }

    void Shoot()
    {
        if (canshoot  && magazine > 0)
        {  
            playerControls.Recoil(recoil);
			//anim.SetTrigger("shoot");
            
            audioSource.Play();
			bullet = Instantiate(pistolBullet,bulletSpawn.position,bulletSpawn.rotation);
			bullet.GetComponent<PistolBullet>().set(gameObject, bullet_speed, range, damage);
			//bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward*5000);
            bullet.GetComponent<Rigidbody>().velocity=bullet.transform.forward*bullet_speed;
            canshoot = false;
            timeLeft = rps;
            magazine--;
            
        }
    }
}
