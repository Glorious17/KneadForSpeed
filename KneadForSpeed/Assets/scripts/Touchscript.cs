using UnityEngine;
using System.Collections;

public class Touchscript : MonoBehaviour {

	void Update () 
	{
		if (Input.touchCount > 0)
		{
			//Touch mytouch = Input.GetTouch (0);

			Touch[] mytouches = Input.touches;

			for(int i = 0; i < Input.touchCount; i++)
			{
				if (mytouches[i].phase == TouchPhase.Began) {
					if (mytouches[i].position.y <= Screen.height / 2) {
						if (mytouches[i].position.x <= Screen.width / 2) {
							Debug.Log ("Unten Links");
						}
						if (mytouches[i].position.x > Screen.width / 2) {
							Debug.Log ("Unten Rechts");
						}
					}

					if (mytouches[i].position.y > Screen.height / 2) {
						if (mytouches[i].position.x <= Screen.width / 2) {
							Debug.Log ("Oben Links");
						}
						if (mytouches[i].position.x > Screen.width / 2) {
							Debug.Log ("Oben Rechts");
						}
					}
				}
			}
		}
	}
}
