using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyMove : MonoBehaviour {

	private float speed = 5;
	private int direction = 1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		RaycastHit2D hit = Physics2D.Raycast (transform.position, new Vector2 (direction, 0));
		GetComponent<Rigidbody2D> ().velocity = new Vector2 (direction* speed, GetComponent<Rigidbody2D>().velocity.y) ;
		if (hit.distance < 0.5f) {
			if (hit.collider.tag == "Player") {
				hit.collider.gameObject.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (direction, 1) * 500);
			}
			flip ();
		}
	}

	void flip(){
		direction = -direction;
	}
}
