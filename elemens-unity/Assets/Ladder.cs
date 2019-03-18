using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour {

	private GameObject player;
	private bool canClimb = false;
	public float speed = 1f;


	void OnTriggerEnter2D(Collider2D col){
		print ("enter !!!!");
		if(col.gameObject.tag == "Player"){
			canClimb = true;
			player = col.gameObject;
		}
	}

	void OnTriggerExit2D(Collider2D col){
		if(col.gameObject.tag == "Player"){
			canClimb = false;
			player.GetComponent<Rigidbody2D>().gravityScale = 10f;
		}
	}
	
	// Update is called once per frame
	void Update () {
		print (this.canClimb);
		if (this.canClimb) {
			float inputY = Input.GetAxis ("Vertical");
			if (inputY != 0) {
				player.GetComponent<Rigidbody2D>().gravityScale = 0f;
				player.transform.Translate (new Vector3 (0, 1f, 0) * speed * inputY * Time.deltaTime);
			}
		}
	}
}
