using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Touchscript : MonoBehaviour 
{
	float speed = 0.2f;
	
	private Vector3 untenrechts = new Vector3(5, 0, - 5);
	private Vector3 obenrechts = new Vector3(5, 0, 5);
	private Vector3 untenlinks = new Vector3(-5, 0, -5);
	private Vector3 obenlinks = new Vector3(-5, 0, 5);
	
	private GameObject triggerUR;
	private GameObject triggerUL;
	private GameObject triggerOR;
	private GameObject triggerOL;

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

	void Start()
	{
		
		triggerUR = GameObject.Find("Untenrechts");
		triggerUL = GameObject.Find("Untenlinks");
		triggerOR = GameObject.Find("Obenrechts");
		triggerOL = GameObject.Find("Obenlinks");
		
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

        moveBack();

        //Mouse Positioning
        if (Input.GetMouseButtonDown(0))
        {
            if (Input.mousePosition.y <= Screen.height / 2)
            {
                //unten links
                if (Input.mousePosition.x <= Screen.width / 2)
                {

                    moveTo(UL, center);
                    List<GameObject> signals = triggerUL.GetComponent<TriggerScript>().Signals;
                    if (signals.Count > 0)
                    {
                        Vector3 sigPos = signals[0].transform.position;
                        Debug.Log(untenlinks - sigPos);
                    }
                }
                //unten rechts
                if (Input.mousePosition.x > Screen.width / 2)
                {
                    moveTo(UR, center);
                    List<GameObject> signals = triggerUR.GetComponent<TriggerScript>().Signals;
                    if (signals.Count > 0)
                    {
                        Vector3 sigPos = signals[0].transform.position;
                        Debug.Log(untenrechts - sigPos);
                    }
                }
            }

            if (Input.mousePosition.y > Screen.height / 2)
            {
                //oben links
                if (Input.mousePosition.x <= Screen.width / 2)
                {
                    moveTo(OL, center);
                    List<GameObject> signals = triggerOL.GetComponent<TriggerScript>().Signals;
                    if (signals.Count > 0)
                    {
                        Vector3 sigPos = signals[0].transform.position;
                        Debug.Log(obenlinks - sigPos);
                    }
                }
                //oben rechts
                if (Input.mousePosition.x > Screen.width / 2)
                {
                    moveTo(OR, center);
                    List<GameObject> signals = triggerOR.GetComponent<TriggerScript>().Signals;
                    if (signals.Count > 0)
                    {
                        Vector3 sigPos = signals[0].transform.position;
                        Debug.Log(obenrechts - sigPos);
                    }
                }

            }

        }


        //Touch Positioning

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

					}

					if (mytouches [i].position.y > Screen.height / 2) {
						//oben links
						if (mytouches [i].position.x <= Screen.width / 2) {
							moveTo (OL, center);
						}
					}
							
						//oben rechts
					if (mytouches[i].position.x > Screen.width / 2) {
								moveTo(OR, center);
					}
				}
			}
		}

		/**if(Input.GetKey("up"))
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
		}**/
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
