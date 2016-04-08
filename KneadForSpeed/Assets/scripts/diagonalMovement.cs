using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class diagonalMovement : MonoBehaviour
{

	public GameObject signal;

	private float movementX = 3f;
	private float movementZ = 3f;

	private float spawntime = 3f;
	private float spawnSpeed = 3f;
	//private float faktor = 500f;

	GameObject s;

	private double randomSpawn = 100;
	//private float spwaning = 0f;
	private float zweiSpawnHaeufigkeit = 25f;
	private bool zweiterSpawn = false;
	List<GameObject> signals;

	void Start()
	{
		signals = new List<GameObject>();
		//spawn();
	}

	void Update()
	{
		if (Time.realtimeSinceStartup >= spawntime)
		{
			if (randomSpawn < zweiSpawnHaeufigkeit) {
				spawn ();
				zweiterSpawn = true;
				spawn ();
				spawntime = Time.realtimeSinceStartup + spawnSpeed;
				zweiterSpawn = false;
			} else {
				spawn ();
				Debug.Log (Time.realtimeSinceStartup);
				spawntime = Time.realtimeSinceStartup + spawnSpeed;
			}

		}

		//spawning += Time.time;// * Time.deltaTime;
		// Debug.Log(spawning);

		//deleting obejcts which are out of bounds
		if (signals.Count > 0)
		{
			foreach (GameObject go in signals)
			{//borders are -7 and 7 yet, dynamic borders are preferred
				if (go.transform.position.x <= -7 || go.transform.position.x >= 7)
				{
					signals.Remove(go); //remove GameObject from List
					Destroy(go); //destroy GameObject
					break; //size of list has been changed and demands a "break"
				}
			}
		}
	}

	void spawn()
	{
		randomSpawn = Random.Range (0F, 199F);
		s = Instantiate(signal);
		randPos();
		s.GetComponent<Rigidbody> ().AddForce (new Vector3 (movementX, 0, movementZ), ForceMode.Impulse);

		signals.Add(s); // adding Object to an List
	}

	void randPos()
	{
		int signum = Random.Range (0, 2) * 2 - 1; //Vorzeichen variiert zwischen -1 und 1;
		//choose between the directions, signum = Vorzeichen
		//random directions
		if (zweiterSpawn == false) {
			//signum = Random.Range (0, 2) * 2 - 1; //Vorzeichen variiert zwischen -1 und 1
			movementX *= signum; //Vorzeichenwechsel
		} else {
			movementX *= -1;
		}

		signum = Random.Range(0, 2) * 2 - 1;
		movementZ *= signum;
		/*int rand = Random.Range (0, 4);

		switch (rand) {
		case 0:
			movementX = Screen.width / faktor;
			movementZ = Screen.height / faktor;
			break;

		case 1:
			movementX = Screen.width / faktor * (-1);
			movementZ = Screen.height / faktor;
			break;

		case 2:
			movementX = Screen.width / faktor;
			movementZ = Screen.height / faktor * (-1);
			break;

		case 3:
			movementX = Screen.width / faktor * (-1);
			movementZ = Screen.height / faktor * (-1);
			break;*/
	}

}
