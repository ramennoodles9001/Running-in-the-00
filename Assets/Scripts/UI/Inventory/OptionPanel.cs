using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Reflection;
public class OptionPanel : MonoBehaviour {
	//private List<Options> options;
	public GameObject option;
	public Inventory inv;
	private float maxDistance;
	public bool inPlace = false;
	private int id;
	//List of options
	public enum Options{
		Equip,
		Use,
		Cancel,
		Drop
	}
	// Use this for initialization
	void Start () {
		inPlace = false;
		transform.position = Input.mousePosition;
		inv = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
		maxDistance = 100f;
		inPlace = true;
	}
	
	// Update is called once per frame
	void Update () {
		//Deletes the optionspanel if our mouse moves to far away
		if (Vector2.Distance(Input.mousePosition, transform.position)>maxDistance && inPlace){
			Destroy(gameObject);
		}
	}
	//Adds another option to the parent panel (OptionsPanel - > [List of options])
	public void addOption(Options newOption){
		
		GameObject temp = Instantiate(option,transform);
		temp.GetComponentInChildren<Text>().text = newOption.ToString();
		temp.GetComponentInChildren<Button>().onClick.AddListener(() => Clicked(temp.GetComponentInChildren<Text>()));
	}
	//What happens when any option is clicked
	//Converted into a method since all options are named by text
	public void Clicked(Text x){
		MethodInfo mi = this.GetType().GetMethod(x.text);
		mi.Invoke(this,null);
	}
	public void Equip(){
		inv.Equip(id);
	}
	public void Use(){
	}
	public void Cancel(){
		inv.destroyOption();
	}
	public void setID(int id){
		this.id = id;
	}
	public void Drop(){
		inv.Drop(id);
	}
}
