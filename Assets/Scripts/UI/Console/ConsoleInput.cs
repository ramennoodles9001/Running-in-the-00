using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ConsoleInput : MonoBehaviour {
	public InputField input;
	public Console console;
	// Use this for initialization
	void Start () {
		input = GetComponent<InputField>();
		console = GetComponentInParent<Console>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Return)){
			console.Command(input.text);
			input.text = "";
			
		}
	}
}
