using UnityEngine;
using System.Collections;

public class diagonalMovement : MonoBehaviour
{

    public GameObject signal;

	private float movementX = 0;
	private float movementZ = 0;

	private const float spawntime = 300f;
	private float spawning = 0f;
	private float faktor = 500f;

    void Start()
    {
		spawn();
    }
		
    void Update()
    {		
		if (spawning >= spawntime) 
		{
			spawn ();
			spawning = 0;
		}	

		spawning += Time.time;// * Time.deltaTime;
		Debug.Log (spawning);

    }

	void spawn()
	{
		GameObject kugel = Instantiate(signal);
		zufallPosition();
		kugel.GetComponent<Rigidbody> ().AddForce (new Vector3 (movementX, 0, movementZ), ForceMode.Impulse);
	}

	void zufallPosition()
	{
		int rand = Random.Range (0, 4);

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
			break;
		}
	}
		
}
