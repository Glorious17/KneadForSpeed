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
    private static List<int> feedback = new List<int>(); //statisch, da das Skript auch in der Main Camera von der EndScene geladen wird und somit diese Liste erhalten bleibt
    //speichert Feedback werte ab

    public List<int> Feedback //Accessor for the feedback List
    {
        get
        {
            return feedback;
        }
    }

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
            feedback.Add(1);
			lichtFarbe = 2;
			GUI.good ();
			Debug.Log("Good Shit");

		}else if(distance < 0.5 && distance > 0.2 || distance > -0.5 && distance < -0.2)  //Mittelmäßiger Treffer
		{
			lifebar.GetComponent<Solidity>().hit(1);
            feedback.Add(2);
            lichtFarbe = 1;
			GUI.medium ();
			Debug.Log("Meh");
		}
		else if(distance < 1.0 && distance > 0.5)   //Kein Treffer
		{
            lifebar.GetComponent<Solidity>().hit(2);
            feedback.Add(3);
            lichtFarbe = 0;
            GUI.bad();
            Debug.Log("That was Bad");
        }
        else
        {
            lifebar.GetComponent<Solidity>().hit(3);
            feedback.Add(3);
			lichtFarbe = 3;
            GUI.failed();
            Debug.Log("You Suck!");
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
		case 3:
			Licht.GetComponent<Lights>().grey [lichtPos] = true;
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
                    moveTo(triggerUL.GetComponent<TriggerScript>().getCurrentBolzen()); //moving the Backe to the Center
                    signals = triggerUL.GetComponent<TriggerScript>().Signals;
					lichtPos = 3;
				}
				//unten rechts
				if (Input.mousePosition.x > Screen.width / 2)
				{
                    moveTo(triggerUR.GetComponent<TriggerScript>().getCurrentBolzen());
                    signals = triggerUR.GetComponent<TriggerScript>().Signals;
					lichtPos = 1;
				}
			}

			if (Input.mousePosition.y > Screen.height / 2)
			{
				//oben links
				if (Input.mousePosition.x <= Screen.width / 2)
				{
                    moveTo(triggerOL.GetComponent<TriggerScript>().getCurrentBolzen());
                    signals = triggerOL.GetComponent<TriggerScript>().Signals;
					lichtPos = 2;
				}
				//oben rechts
				if (Input.mousePosition.x > Screen.width / 2)
				{
                    moveTo(triggerOR.GetComponent<TriggerScript>().getCurrentBolzen());
                    signals = triggerOR.GetComponent<TriggerScript>().Signals;
					lichtPos = 0;
				}
			}

			if (signals.Count > 0) //only when the triggerbox contains at least one signal 
			{
				Vector3 sigPos = signals[0].transform.position;
				float distance = optimalPosition - Mathf.Abs(sigPos.x); //bitte noch mit Kommentar versehen

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
                        moveTo(triggerUL.GetComponent<TriggerScript>().getCurrentBolzen());//moving the Backe to the Center 
                        signals = triggerUL.GetComponent<TriggerScript>().Signals;
						lichtPos = 3;
					}
					if (mytouches [i].position.y > Screen.height / 2 && mytouches [i].position.x <= Screen.width / 2) 
					{
						//oben links
                        moveTo(triggerOL.GetComponent<TriggerScript>().getCurrentBolzen());
						signals = triggerOL.GetComponent<TriggerScript>().Signals;
						lichtPos = 2;
					}
					//oben rechts
					if (mytouches[i].position.x > Screen.width / 2 && mytouches [i].position.y > Screen.height / 2) 
					{
                        moveTo(triggerOR.GetComponent<TriggerScript>().getCurrentBolzen());
						signals = triggerOR.GetComponent<TriggerScript>().Signals;
						lichtPos = 0;
					}
					//unten rechts
					if (mytouches[i].position.x > Screen.width / 2 && mytouches [i].position.y <= Screen.height / 2)
					{
                        moveTo(triggerUR.GetComponent<TriggerScript>().getCurrentBolzen());
						signals = triggerUR.GetComponent<TriggerScript>().Signals;
						lichtPos = 1;
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
			lightOn ();
		}

		lightOn (); //Nur für die Maus
	}


	public void moveTo(GameObject current)
	{
		while(current.transform.position != center.transform.position && 
			Vector3.Distance(current.transform.position, center.transform.position) > abstand)
		{
			current.transform.position = Vector3.MoveTowards (current.transform.position, center.transform.position, speed);
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
