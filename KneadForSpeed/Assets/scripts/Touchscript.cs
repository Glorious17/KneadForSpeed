using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Touchscript : MonoBehaviour {

    private Vector3 untenrechts = new Vector3(5, 0, - 5);
	private Vector3 obenrechts = new Vector3(5, 0, 5);
	private Vector3 untenlinks = new Vector3(-5, 0, -5);
	private Vector3 obenlinks = new Vector3(-5, 0, 5);

	private GameObject triggerUR;
	private GameObject triggerUL;
	private GameObject triggerOR;
	private GameObject triggerOL;

    public Camera cam;

	void Start(){
		triggerUR = GameObject.Find("Untenrechts");
		triggerUL = GameObject.Find("Untenlinks");
		triggerOR = GameObject.Find("Obenrechts");
		triggerOL = GameObject.Find("Obenlinks");
	}
	void Update () 
	{
		if (Input.GetMouseButtonDown (0)) {
			if (Input.mousePosition.y <= Screen.height / 2) {
				//unten links
				if (Input.mousePosition.x <= Screen.width / 2) {
					List<GameObject> signals = triggerUL.GetComponent<TriggerScript> ().Signals;
					if (signals.Count > 0) {
						Vector3 sigPos = signals [0].transform.position;
						Debug.Log (untenlinks - sigPos);
					}
				}
				//unten rechts
				if (Input.mousePosition.x > Screen.width / 2) {
					
					List<GameObject> signals = triggerUR.GetComponent<TriggerScript> ().Signals;
					if (signals.Count > 0) {
						Vector3 sigPos = signals [0].transform.position;
						Debug.Log (untenrechts - sigPos);
					}
				}
			}

			if (Input.mousePosition.y > Screen.height / 2) {
				//oben links
				if (Input.mousePosition.x <= Screen.width / 2) {
					
					List<GameObject> signals = triggerOL.GetComponent<TriggerScript> ().Signals;
					if (signals.Count > 0) {
						Vector3 sigPos = signals [0].transform.position;
						Debug.Log (obenlinks - sigPos);
					}				
				}
				//oben rechts
				if (Input.mousePosition.x > Screen.width / 2) {

					List<GameObject> signals = triggerOR.GetComponent<TriggerScript> ().Signals;
					if (signals.Count > 0) {
						Vector3 sigPos = signals [0].transform.position;
						Debug.Log (obenrechts - sigPos);
					}
				}
			
			}
				
		}

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
							List<GameObject> signals = triggerUR.GetComponent<TriggerScript> ().Signals;
							if (signals.Count > 0) {
								Vector3 sigPos = signals [0].transform.position;
								Debug.Log (untenrechts - sigPos);
							}
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
