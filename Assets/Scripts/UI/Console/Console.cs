using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using UnityEngine.UI;

public class Console : MonoBehaviour {

	public Transform player;
	public Inventory inv;
	public ItemDatabase idb;
	public string cmd;
	public string[] args;
	public string[] commands;
	public Text output;
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player").transform;
		inv = player.GetComponent<Inventory>();
		idb = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ItemDatabase>();
		commands = new string[]{"log",
								"hi",
								"item"};
		gameObject.SetActive(false);
	}
	
	
	void Update () {
		
	}
	//Used when calling a command
	public void Command(string input){
		if (input == "" || input==" "){
			input = "hi";
		}
		//Splits the command into different arguments based on spaces
		args = input.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries);
		//Our command is the first argument
		cmd = args[0];
		//Loops through the commands array to find which command was called
		for(int x = 0; x<commands.Length;x++){
			if (cmd == commands[x]){
				//Search up 'Reflection' on google to find out what this does
				//Basically convers the string into a method *Make sure the method name is the same as the command! ex: "log" = public void log(){}*
				MethodInfo mi = this.GetType().GetMethod(cmd);
				//Calls the reflected method
				mi.Invoke(this,null);
			}
		}
	}
	//Writes to console
	public void consoleLog(string x){
		output.text += x + "\n";
	}
	//Formats arguments to write to console
	public void log(){
		string temp = "";
		for(int i =1; i<args.Length;i++){
			temp+= args[i] + " ";
		}
		output.text += temp + "\n";
	}
	//Testing?
	public void hi(){

	}
	//Spawns item by parsing the string into ints or by searching by name
	public void item(){
		int id;
		if (Int32.TryParse(args[1], out id)){
			Debug.Log("Id");
			if(args.Length>2){
				inv.AddItem(new Item(idb.itemdatabase[Int32.Parse(args[1])]), Int32.Parse(args[2]));
				consoleLog("Spawned " + args[2] + " " + idb.itemdatabase[Int32.Parse(args[1])].name);
			}else{
				inv.AddItem(new Item(idb.itemdatabase[Int32.Parse(args[1])]));
				consoleLog("Spawned "+ 1 + " " + idb.itemdatabase[Int32.Parse(args[1])].name);
			}
		}else{
			foreach(Item i in idb.itemdatabase){
				if (i.name.ToLower()==args[1].ToLower()){
					if(args.Length>2){
						inv.AddItem(new Item(i),Int32.Parse(args[2]));
						consoleLog("Spawned "+args[2] + " " + i.name);
					}else{
						inv.AddItem(new Item(i));
						consoleLog("Spawned "+ 1 + " " + i.name);
					}
				}
			}
		}
	}
	
}
