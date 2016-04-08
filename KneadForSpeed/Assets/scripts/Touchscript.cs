using UnityEngine;
using System.Collections;

public class Touchscript : MonoBehaviour {

    private Vector3 untenrechts = new Vector3(5, 0, - 5);

    private GameObject triggerUR = GameObject.Find("Untenrechts");
    public Camera cam;
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
                        //unten links
						if (mytouches[i].position.x <= Screen.width / 2) {
							Debug.Log ("Unten Links");
						}
                        //unten rechts
						if (mytouches[i].position.x > Screen.width / 2) {
                            GameObject firstSig = triggerUR.GetComponent<TriggerScript>().Signals;
                            Vector3 sigPos = firstSig.transform.position;
                            Debug.Log(untenrechts - sigPos);
						}
					}
                   
					if (mytouches[i].position.y > Screen.height / 2) {
                        //oben links
                        if (mytouches[i].position.x <= Screen.width / 2) {
							Debug.Log ("Oben Links");
						}
                        //oben rechts
						if (mytouches[i].position.x > Screen.width / 2) {
							Debug.Log ("Oben Rechts");
						}
					}
				}

                //erster Versuch, Signale bei Berührung zu löschen
                Ray touchRay = cam.ScreenPointToRay(mytouches[i].position);

                RaycastHit hit;
                if (Physics.Raycast(touchRay, out hit) && hit.collider.gameObject.tag == "Signal")
                    Destroy(hit.collider.gameObject);

            }
		}
	}
}
