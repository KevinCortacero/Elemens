using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour {

	private bool alive;

	public float health = 100.0f;

	// Use this for initialization
	void Start () {
		alive = true;
	}
	
	// Update is called once per frame
	void Update () {

		if (gameObject.transform.position.y < -100){
			health -= 10.0f;
		}

		if (health <= 0.0f) {
			alive = false;
		}

		if (alive == false) {
			Die();
		}
	}

	void Die(){
		SceneManager.LoadScene ("elemens_1");
	}
}
