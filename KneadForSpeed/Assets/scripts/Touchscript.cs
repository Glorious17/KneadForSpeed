using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Touchscript : MonoBehaviour 
{
	float speed = 0.2f;

	private int optimalPosition = 5; // ? 

	private Vector3 untenrechts = new Vector3(5, 0, - 5); //Positionen der Zielpunkte (zur Evaluierung des Scores)
	private Vector3 obenrechts = new Vector3(5, 0, 5);
	private Vector3 untenlinks = new Vector3(-5, 0, -5);
	private Vector3 obenlinks = new Vector3(-5, 0, 5);
	
	private GameObject triggerUR; //Triggerboxen
	private GameObject triggerUL;
	private GameObject triggerOR;
	private GameObject triggerOL;

	public GameObject OL; //Gameobjects der Backen
	public GameObject OR;
	public GameObject UL;
	public GameObject UR;
	public GameObject center;

	public GameObject oOL; //Gameobjects der Ursprungspositionen der Backen (old ObenLinks etc.)
	public GameObject oOR;
	public GameObject oUL;
	public GameObject oUR;

    private GameObject lifebar;

	private float abstand = 2.5f;

	public Camera cam;

    private List<GameObject> signals; //storage for signals on the field

    void Start()
	{
        signals = new List<GameObject>();
		triggerUR = GameObject.Find ("Untenrechts");
		triggerUL = GameObject.Find ("Untenlinks");
		triggerOR = GameObject.Find ("Obenrechts");
		triggerOL = GameObject.Find ("Obenlinks");

        lifebar = GameObject.Find("Lifebar");
	}

	void calculatePoints(float distance)
	{


		if(distance > - 0.2 && distance < 0.2)      //Guter Treffer
		{

            lifebar.GetComponent<Solidity>().hit(0);
			Debug.Log("Good Shit");

		}else if(distance < 0.5 && distance > 0.2)  //Mittelmäßiger Treffer
		{
            lifebar.GetComponent<Solidity>().hit(1);

            Debug.Log("Meh");
		}
		else                                        //Kein Treffer
		{
            lifebar.GetComponent<Solidity>().hit(2);

            Debug.Log("You suck!");
		}


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
					moveTo(UL, center); //moving the Backe to the Center
                    signals = triggerUL.GetComponent<TriggerScript>().Signals;
                }
				//unten rechts
				if (Input.mousePosition.x > Screen.width / 2)
				{
					moveTo(UR, center);
                    signals = triggerUR.GetComponent<TriggerScript>().Signals;
                }
			}

			if (Input.mousePosition.y > Screen.height / 2)
			{
				//oben links
				if (Input.mousePosition.x <= Screen.width / 2)
				{
					moveTo(OL, center);
                    signals = triggerOL.GetComponent<TriggerScript>().Signals;
                }
				//oben rechts
				if (Input.mousePosition.x > Screen.width / 2)
				{
					moveTo(OR, center);
                    signals = triggerOR.GetComponent<TriggerScript>().Signals;
                }

			}
                      
            if (signals.Count > 0) //only when the triggerbox contains at least one signal 
            {
                Vector3 sigPos = signals[0].transform.position;
                float distance = optimalPosition + sigPos.x; //bitte noch mit Kommentar versehen

                Debug.Log(distance);
                calculatePoints(distance); //score evaulation

                GameObject go = signals[0]; //first signal which entered the trigger box is looked at
                signals.Remove(signals[0]); //removing Object from List
                Destroy(go); //deleting Object from scene
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
							moveTo(UL, center);//moving the Backe to the Center 
                            signals = triggerUL.GetComponent<TriggerScript>().Signals;
                        }

					}
					if (mytouches [i].position.y > Screen.height / 2) {
						//oben links
						if (mytouches [i].position.x <= Screen.width / 2) {
							moveTo (OL, center);
                            signals = triggerOL.GetComponent<TriggerScript>().Signals;
                        }
					}
					//oben rechts
					if (mytouches[i].position.x > Screen.width / 2) {
						moveTo(OR, center);
                        signals = triggerOR.GetComponent<TriggerScript>().Signals;
                    }
					//unten rechts
					if (mytouches[i].position.x > Screen.width / 2)
					{
						moveTo(UR, center);
                        signals = triggerUR.GetComponent<TriggerScript>().Signals;
                    }

                    if (signals.Count > 0) //only when the triggerbox contains at least one signal 
                    {
                        Vector3 sigPos = signals[0].transform.position;
                        float distance = optimalPosition + sigPos.x; //bitte noch mit Kommentar versehen

                        Debug.Log(distance);
                        calculatePoints(distance); //score evaulation

                        GameObject go = signals[0]; //first signal which entered the trigger box is looked at
                        signals.Remove(signals[0]); //removing Object from List
                        Destroy(go); //deleting Object from scene
                    }
                }
			}
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

		OL.transform.rotation = OL.transform.rotation;
		UL.transform.rotation = UL.transform.rotation;
		OR.transform.rotation = OR.transform.rotation;
		UR.transform.rotation = UR.transform.rotation;
	}
}
