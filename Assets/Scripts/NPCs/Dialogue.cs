using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
[System.Serializable]
public class Dialogue : MonoBehaviour {
	public List<DialogueBox> dialogues= new List<DialogueBox>();
	public string pathToResources = "/Assets/Dialogue/Quests";
	public string XMLFile = "/StarterQuest.xml"; 
	
	// Use this for initialization
	void Start () {
		ReadFile();
	}
	
	// Update is called once per frame
	void Update () {
		
	} 

	public void ReadFile(){
		
		XmlDocument doc = new XmlDocument();
		doc.Load(pathToResources+XMLFile);
		XmlNodeList xmlNodes = doc.SelectNodes("doc");
		
		foreach(XmlNode n in xmlNodes){
			foreach(XmlNode node in n.ChildNodes){
			Debug.Log(node.Name);
			XmlElement element = (XmlElement)node;

			List<DialogueOption> options= new List<DialogueOption>();
			foreach(XmlNode option in node.SelectNodes("o")){
				XmlElement e = (XmlElement)option;
				options.Add(new DialogueOption(e.GetElementsByTagName("nextId")[0].InnerText,e.GetElementsByTagName("c")[0].InnerText));
			}
			DialogueBox db = new DialogueBox(element.GetElementsByTagName("id")[0].InnerText, element.GetElementsByTagName("s")[0].InnerText,options.ToArray());
			dialogues.Add(db);
			}
		}
		
	}

	public DialogueBox getFirstSpeech(){
		foreach(DialogueBox x in dialogues){
			if (x.id==0){
				return x;
			}
		}
		return null;
	}

	public DialogueBox GetDialogueBox(int id){
		foreach(DialogueBox x in dialogues){
			if (x.id==id){
				return x;
			}
		}
		return null;
	}

	public void finishedDialogue(){
		if(gameObject.GetComponent<QuestGiver>()){
			gameObject.GetComponent<QuestGiver>().startQuest();
		}
	}

}
