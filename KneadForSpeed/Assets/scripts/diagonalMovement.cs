using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class diagonalMovement : MonoBehaviour
{

    public GameObject signal;

	private float movementX = 3;
	private float movementZ = 3;

	private const float spawntime = 300f;
	private float spawning = 0f;
	private float faktor = 500f;
    GameObject s;

    List<GameObject> signals;

    void Start()
    {
        spawn();
    }

    void Update()
    {
        if (spawning >= spawntime)
        {
            spawn();
            spawning = 0;
        }

        spawning += Time.time;// * Time.deltaTime;
                              // Debug.Log(spawning);

    }

	void spawn()
	{
		s = Instantiate(signal);
		randPos();
		s.GetComponent<Rigidbody> ().AddForce (new Vector3 (movementX, 0, movementZ), ForceMode.Impulse);

	}

	void randPos()
	{
        //choose between the directions, signum = Vorzeichen
        //random directions
        int signum = Random.Range(0, 2) * 2 - 1; //Vorzeichen variiert zwischen -1 und 1
        movementX *= signum; //Vorzeichenwechsel

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
