using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Touchscript : MonoBehaviour 
{
	float speed = 0.2f;

	private int optimalPosition = 5;

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

	public GameObject oOL;
	public GameObject oOR;
	public GameObject oUL;
	public GameObject oUR;

	private float abstand = 2.5f;

	public Camera cam;

	void calculatePoints(float distance)
	{


		if(distance > - 0.2 && distance < 0.2)      //Guter Treffer
		{
			Debug.Log("Good Shit");
		}else if(distance < 0.5 && distance > 0.2)  //Mittelmäßiger Treffer
		{
			Debug.Log("Meh");
		}
		else                                        //Kein Treffer
		{
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

					moveTo(UL, center);
					List<GameObject> signals = triggerUL.GetComponent<TriggerScript>().Signals;


					if (signals.Count > 0)
					{
						Vector3 sigPos = signals[0].transform.position;
						float distance = optimalPosition + sigPos.x;

						Debug.Log(distance);
						calculatePoints(distance);

						GameObject go = signals[0];
						signals.Remove(signals[0]);
						Destroy(go);
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
						float distance = optimalPosition - sigPos.x;


						Debug.Log(distance);
						calculatePoints(distance);


						GameObject go = signals[0];
						signals.Remove(signals[0]);
						Destroy(go);
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
						float distance = optimalPosition + sigPos.x;

						Debug.Log(distance);
						calculatePoints(distance);


						GameObject go = signals[0];
						signals.Remove(signals[0]);
						Destroy(go);
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
						float distance = optimalPosition - sigPos.x;

						Debug.Log(distance);
						calculatePoints(distance);


						GameObject go = signals[0];
						signals.Remove(signals[0]);
						Destroy(go);
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
							List<GameObject> signals = triggerUL.GetComponent<TriggerScript>().Signals;
							if (signals.Count > 0)
							{
								Vector3 sigPos = signals[0].transform.position;
								Debug.Log(untenlinks - sigPos);
								GameObject go = signals[0];
								signals.Remove(signals[0]);
								Destroy(go);
							}
						}

					}

					if (mytouches [i].position.y > Screen.height / 2) {
						//oben links
						if (mytouches [i].position.x <= Screen.width / 2) {
							moveTo (OL, center);
							List<GameObject> signals = triggerOL.GetComponent<TriggerScript>().Signals;
							if (signals.Count > 0)
							{
								Vector3 sigPos = signals[0].transform.position;
								Debug.Log(obenlinks - sigPos);
								GameObject go = signals[0];
								signals.Remove(signals[0]);
								Destroy(go);
							}
						}
					}

					//oben rechts
					if (mytouches[i].position.x > Screen.width / 2) {
						moveTo(OR, center);
						List<GameObject> signals = triggerOR.GetComponent<TriggerScript>().Signals;
						if (signals.Count > 0)
						{
							Vector3 sigPos = signals[0].transform.position;
							Debug.Log(obenrechts - sigPos);
							GameObject go = signals[0];
							signals.Remove(signals[0]);
							Destroy(go);
						}
					}

					//unten rechts
					if (mytouches[i].position.x > Screen.width / 2)
					{
						moveTo(UR, center);
						List<GameObject> signals = triggerUR.GetComponent<TriggerScript>().Signals;
						if (signals.Count > 0)
						{
							Vector3 sigPos = signals[0].transform.position;
							Debug.Log(untenrechts - sigPos);
							GameObject go = signals[0];
							signals.Remove(signals[0]);
							Destroy(go);
						}
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

		OL.transform.rotation = OL.transform.rotation;
		UL.transform.rotation = UL.transform.rotation;
		OR.transform.rotation = OR.transform.rotation;
		UR.transform.rotation = UR.transform.rotation;
	}
}
