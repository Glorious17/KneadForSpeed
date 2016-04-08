using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class diagonalMovement : MonoBehaviour
{

	public GameObject signal;

	private float movementX = 3f;
	private float movementZ = 3f;

    private double randomSpawn = 100;
    private float zweiSpawnHaeufigkeit = 25f;
    private bool zweiterSpawn = false;
    private float spawntime = 3f;
    private float spawnSpeed = 10f;
    private float fasterSpawnTime = 0;
    private float fastesSpeed = 0.5f; //Umso kleiner diese Zahl ist, umso schneller ist die maximale Geschwindigkeit
	//private float faktor = 500f;
    GameObject s;

    void Start()
    {
        //spawn();
    }

    void Update()
    {
        if (Time.realtimeSinceStartup >= spawntime)
        {
            if (randomSpawn < zweiSpawnHaeufigkeit)
            {
                spawn();
                zweiterSpawn = true;
                spawn();
                spawntime = Time.realtimeSinceStartup + spawnSpeed;
                zweiterSpawn = false;
            }
            else {
                spawn();
                Debug.Log(Time.realtimeSinceStartup);
                spawntime = Time.realtimeSinceStartup + spawnSpeed;
            }
        }
        if (fasterSpawnTime >= 10)
        {
            spawnSpeed /= 2;
            if (spawnSpeed < fastesSpeed)
            {
                spawnSpeed = fastesSpeed;
            }
            fasterSpawnTime = 0;
            Debug.Log("fasterSpawnTime = 0");

            fasterSpawnTime += Time.deltaTime;

            //spawning += Time.time;// * Time.deltaTime;
            // Debug.Log(spawning);
        }
    }

	void spawn()
	{
		randomSpawn = Random.Range (0F, 199F);
		s = Instantiate(signal);
		randPos();
		s.GetComponent<Rigidbody> ().AddForce (new Vector3 (movementX, 0, movementZ), ForceMode.Impulse);
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
