using UnityEngine;
using System.Collections;

public class BolzenRotation : MonoBehaviour {

	public float speed;
	
	// Update is called once per frame
	void Update () 
	{
		transform.Rotate(0, speed*Time.deltaTime, 0);
	}
}
