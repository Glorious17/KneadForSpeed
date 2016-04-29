﻿using UnityEngine;
using System.Collections;

public class dragWire : MonoBehaviour {

	private Vector3 screenPoint;
	private Vector3 offset;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown(){
		screenPoint = Camera.main.WorldToScreenPoint (gameObject.transform.position);
		offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));

	}

	void OnMouseDrag(){
		Vector3 cursorPoint = new Vector3 (Input.mousePosition.x, Screen.height/2, screenPoint.z);
		Vector3 cursorPosition = Camera.main.ScreenToWorldPoint (cursorPoint) + offset;
		transform.position = cursorPosition;
	}
}
