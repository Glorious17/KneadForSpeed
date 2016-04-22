using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Touchscript : MonoBehaviour 
{
	float speed = 0.2f;

	private int optimalPosition = 5; // ? 

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

	private int ausLicht = 5;
	private int lichtPos;
	private int lichtFarbe;
	private GameObject Licht;

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

		lichtPos = ausLicht;
		lichtFarbe = ausLicht;
		Licht = GameObject.Find ("Lights");
	}

	void calculatePoints(float distance)
	{
		if(distance > - 0.2 && distance < 0.2)      //Guter Treffer
		{
			lifebar.GetComponent<Solidity>().hit(0);
			lichtFarbe = 2;
			GUI.good ();
			Debug.Log("Good Shit");

		}else if(distance < 0.5 && distance > 0.2)  //Mittelmäßiger Treffer
		{
			lifebar.GetComponent<Solidity>().hit(1);
			lichtFarbe = 1;
			GUI.notGood ();
			Debug.Log("Meh");
		}
		else                                        //Kein Treffer
		{
			lifebar.GetComponent<Solidity>().hit(2);
			lichtFarbe = 0;
			GUI.bad ();
			Debug.Log("You suck!");
		}

	}

	void lightOn()
	{
		switch(lichtFarbe)
		{
		case 0: 
			Licht.GetComponent<Lights>().red [lichtPos] = true;
			break;
		case 1:
			Licht.GetComponent<Lights>().yellow [lichtPos] = true;
			break;
		case 2:
			Licht.GetComponent<Lights>().green [lichtPos] = true;
			break;
		}

		lichtFarbe = ausLicht;
		lichtPos = ausLicht;
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
					lichtPos = 3;
				}
				//unten rechts
				if (Input.mousePosition.x > Screen.width / 2)
				{
					moveTo(UR, center);
					signals = triggerUR.GetComponent<TriggerScript>().Signals;
					lichtPos = 1;
				}
			}

			if (Input.mousePosition.y > Screen.height / 2)
			{
				//oben links
				if (Input.mousePosition.x <= Screen.width / 2)
				{
					moveTo(OL, center);
					signals = triggerOL.GetComponent<TriggerScript>().Signals;
					lichtPos = 2;
				}
				//oben rechts
				if (Input.mousePosition.x > Screen.width / 2)
				{
					moveTo(OR, center);
					signals = triggerOR.GetComponent<TriggerScript>().Signals;
					lichtPos = 0;
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
				if (mytouches[i].phase == TouchPhase.Began) 
				{

					if (mytouches[i].position.y <= Screen.height / 2 && mytouches[i].position.x <= Screen.width / 2) 
					{
						//unten links
						moveTo(UL, center);//moving the Backe to the Center 
						signals = triggerUL.GetComponent<TriggerScript>().Signals;
						lichtPos = 3;
					}
					if (mytouches [i].position.y > Screen.height / 2 && mytouches [i].position.x <= Screen.width / 2) 
					{
						//oben links
						moveTo (OL, center);
						signals = triggerOL.GetComponent<TriggerScript>().Signals;
						lichtPos = 1;
					}
					//oben rechts
					if (mytouches[i].position.x > Screen.width / 2) 
					{
						moveTo(OR, center);
						signals = triggerOR.GetComponent<TriggerScript>().Signals;
						lichtPos = 2;
					}
					//unten rechts
					if (mytouches[i].position.x > Screen.width / 2)
					{
						moveTo(UR, center);
						signals = triggerUR.GetComponent<TriggerScript>().Signals;
						lichtPos = 0;
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

		lightOn ();
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
