﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSystem : MonoBehaviour {

	private GameObject target;

	public float xMin;
	public float xMax;
	public float yMin;
	public float yMax;

	// Use this for initialization
	void Start () {
		target = GameObject.FindGameObjectWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		float x = Mathf.Clamp (target.transform.position.x, xMin, xMax);
		float y = Mathf.Clamp (target.transform.position.y, yMin, yMax);

		gameObject.transform.position = new Vector3 (x, y, gameObject.transform.position.z);
	}
}
