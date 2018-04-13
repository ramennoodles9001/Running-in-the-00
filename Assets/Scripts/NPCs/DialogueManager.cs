using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DialogueManager : MonoBehaviour {
    public GameObject dialoguePanel;
    public GameObject dialogueName;
    public GameObject dialogueSpeech;
    public GameObject dialogueOptionPanel;
    public GameObject dialogueOption;
    public Dialogue currentDialogue;
    public GameObject player;
    
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void openDialogue(Dialogue dialogue, string name){
        closeDialogue();
        gameObject.GetComponent<Image>().enabled = true;
        foreach(Image i in gameObject.GetComponentsInChildren<Image>()){
            i.enabled = true;
        }
        foreach(Text txt in gameObject.GetComponentsInChildren<Text>()){
            txt.enabled = true;
        }
        dialogueName.GetComponent<Text>().text=name;
        currentDialogue = dialogue;
        dialoguePanel.SetActive(true);
        dialogueSpeech.GetComponent<Text>().text = dialogue.getFirstSpeech().speech;
        foreach(DialogueOption option in dialogue.getFirstSpeech().options){
            GameObject x = Instantiate(dialogueOption,dialogueOption.transform.position,Quaternion.identity,dialogueOptionPanel.transform);
            x.GetComponentInChildren<Text>().text = option.text;
            x.GetComponent<DialogueOptionButton>().text = option.text;
            x.GetComponent<DialogueOptionButton>().nextId = option.nextId;
        }
        player.GetComponent<PlayerControls>().unlockCursor();
        player.GetComponent<PlayerControls>().setAllowInput();
    }

    public void closeDialogue(){
        foreach(Transform t in dialogueOptionPanel.transform){
            Destroy(t.gameObject);
        }
        gameObject.GetComponent<Image>().enabled = false;
        foreach(Image i in gameObject.GetComponentsInChildren<Image>()){
            i.enabled = false;
        }
        foreach(Text txt in gameObject.GetComponentsInChildren<Text>()){
            txt.enabled = false;
        }
        currentDialogue = null;
        player.GetComponent<PlayerControls>().setAllowInput();
        player.GetComponent<PlayerControls>().lockCursor();
    }
    public void nextDialogue(int nextId){
        if(nextId==-1){
            currentDialogue.finishedDialogue();
            closeDialogue();
        }else{
        foreach(Transform t in dialogueOptionPanel.transform){
            Destroy(t.gameObject);
        }
        dialogueSpeech.GetComponent<Text>().text = currentDialogue.GetDialogueBox(nextId).speech;
        foreach(DialogueOption option in currentDialogue.GetDialogueBox(nextId).options){
            GameObject x = Instantiate(dialogueOption,dialogueOption.transform.position,Quaternion.identity,dialogueOptionPanel.transform);
            x.GetComponentInChildren<Text>().text = option.text;
            x.GetComponent<DialogueOptionButton>().text = option.text;
            x.GetComponent<DialogueOptionButton>().nextId = option.nextId;
        }
        }
    }
}
