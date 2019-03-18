using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialog : MonoBehaviour {

	public Transform player;
	public Transform npc;
	public GameObject button;
	public GameObject background;

	public TextMeshProUGUI textDisplay;
	public string[] sentences;
	private int index;
	public float typingSpeed;
	private double distance;

	private bool writing = false;
	private bool newSentence = true;

	
	IEnumerator type(){
		foreach(char letter in sentences[index].ToCharArray()){
			textDisplay.text += letter;
			yield return new WaitForSeconds (typingSpeed);
		}
	}

	void Update(){
		distance = (player.position - npc.position).magnitude;
		if (distance < 2 && newSentence) {
			StartCoroutine (type ());
			print (distance);
			writing = true;
			newSentence = false;
			background.SetActive (true);
		}

		if (textDisplay.text == sentences[index]){
			writing = false;
			button.SetActive (true);
		}
	}

	public void NextSentence(){
		button.SetActive (false);
		textDisplay.text = "";
		if (index < sentences.Length - 1) {
			index++;
			textDisplay.text = "";
			newSentence = true;
		}
		else
			background.SetActive (false);
	}
}
