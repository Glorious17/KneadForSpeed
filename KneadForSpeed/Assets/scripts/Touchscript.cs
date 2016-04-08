using UnityEngine;
using System.Collections;

public class Touchscript : MonoBehaviour 
{
	float speed = 0.2f;

	public GameObject OL;
	public GameObject OR;
	public GameObject UL;
	public GameObject UR;
	public GameObject center;

	private GameObject oOL;
	private GameObject oOR;
	private GameObject oUL;
	private GameObject oUR;

	private float abstand = 2.5f;

	public Camera cam;

	private Vector3 untenrechts = new Vector3(5, 0, - 5);

	private GameObject triggerUR = GameObject.Find("Untenrechts");

	void Start()
	{
		//Speichert Anfangspositionen
		oOL = new GameObject();
		oOR = new GameObject();
		oUL = new GameObject();
		oUR = new GameObject();

		oOL.transform.position = new Vector3(OL.transform.position.x,OL.transform.position.y,OL.transform.position.z);
		oOR.transform.position = new Vector3(OR.transform.position.x,OR.transform.position.y,OR.transform.position.z);
		oUL.transform.position = new Vector3(UL.transform.position.x,UL.transform.position.y,UL.transform.position.z);
		oUR.transform.position = new Vector3(UR.transform.position.x,UR.transform.position.y,UR.transform.position.z);
	}


	void Update () 
	{
		//countinuous moveBack
		moveBack();

		if (Input.touchCount > 0)
		{

			//Touch mytouch = Input.GetTouch (0);

			Touch[] mytouches = Input.touches;

			for(int i = 0; i < Input.touchCount; i++)
			{
				if (mytouches[i].phase == TouchPhase.Began) {

					if (mytouches[i].position.y <= Screen.height / 2) {
						//unten links
						if (mytouches[i].position.x <= Screen.width / 2) 
						{
							moveTo(UL, center);
						}

						if (mytouches[i].position.x > Screen.width / 2) 
						{
							moveTo(UR, center);

							GameObject firstSig = triggerUR.GetComponent<TriggerScript>().Signals;
							Vector3 sigPos = firstSig.transform.position;
							Debug.Log(untenrechts - sigPos);
						}
					}

					if (mytouches[i].position.y > Screen.height / 2) {
						//oben links
						if (mytouches[i].position.x <= Screen.width / 2)
						{
							moveTo(OL, center);
						}

						if (mytouches[i].position.x > Screen.width / 2) 
						{
							//oben rechts
							if (mytouches[i].position.x > Screen.width / 2) 
							{
								moveTo(OR, center);
							}
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

		if(Input.GetKey("up"))
		{
			moveTo(OR, center);
		}
		if(Input.GetKey("down"))
		{
			moveTo(OL, center);
		}
		if(Input.GetKey("left"))
		{
			moveTo(UR, center);
		}
		if(Input.GetKey("right"))
		{
			moveTo(UL, center);
		}
	}		

	void moveTo(GameObject current, GameObject target)
	{
		while(current.transform.position != target.transform.position && 
			Vector3.Distance(current.transform.position, target.transform.position) > abstand)
		{
			current.transform.position = Vector3.MoveTowards (current.transform.position, target.transform.position, speed);
		}
	}

	void moveBack()
	{
		OL.transform.position = Vector3.MoveTowards (OL.transform.position, oOL.transform.position, speed);
		UL.transform.position = Vector3.MoveTowards (UL.transform.position, oUL.transform.position, speed);
		OR.transform.position = Vector3.MoveTowards (OR.transform.position, oOR.transform.position, speed);
		UR.transform.position = Vector3.MoveTowards (UR.transform.position, oUR.transform.position, speed);
	}
}
