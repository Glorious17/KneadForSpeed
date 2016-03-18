using UnityEngine;
using System.Collections;

public class Touchscript : MonoBehaviour {

	void Update () 
	{
		if (Input.touchCount > 0)
		{
			Touch mytouch = Input.GetTouch (0);

			//Touch[] mytouches = Input.touches;

			if (mytouch.phase == TouchPhase.Began) {
				if (mytouch.position.y <= Screen.height / 2) {
					if (mytouch.position.x <= Screen.width / 2) {
						Debug.Log ("Unten Links");
					}
					if (mytouch.position.x > Screen.width / 2) {
						Debug.Log ("Unten Rechts");
					}
				}

				if (mytouch.position.y > Screen.height / 2) {
					if (mytouch.position.x <= Screen.width / 2) {
						Debug.Log ("Oben Links");
					}
					if (mytouch.position.x > Screen.width / 2) {
						Debug.Log ("Oben Rechts");
					}
				}
			}
		}
	}
}
