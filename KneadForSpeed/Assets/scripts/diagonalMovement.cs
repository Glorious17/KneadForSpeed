using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class diagonalMovement : MonoBehaviour
{
	public GameObject signal;

	private float movementX = 3f;
	private float movementZ = 3f;
    private float patternMovementX = 3f;
    private float patternMovementZ = 3f;

    private double randomSpawn = 100;
    private float zweiSpawnHaeufigkeit = 25f;
    private int randomPattern;
    private int randomizer;
    private float patternTime = 4;
    private int patternCounter = 0;
    private bool zweiterSpawn = false;
    private bool specialPattern = false;
    private float spawntime = 3f;
    private float spawnSpeed = 5f;
    private float fasterSpawnTime = 0;
    private float fastesSpeed = 0.4f; //Umso kleiner diese Zahl ist, umso schneller ist die maximale Geschwindigkeit der Spawns
	//private float faktor = 500f;

	GameObject s;


    void Start()
    {
        //spawn();
    }

    void Update()
    {
        if (!specialPattern)
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
                if (Random.Range(0, 100) < 10)
                    specialPattern = true;
            }
            randomizer = Random.Range(0, 1001);
        }
        else
        {
            if(patternCounter == 0)
                randomPattern = Random.Range(0, 2);

            if (Time.realtimeSinceStartup >= patternTime)
            {
                pattern();
                patternTime = Time.realtimeSinceStartup;
                patternTime += 0.4f;
            }
            
        }
        
        if (fasterSpawnTime >= 10)
        {
            spawnSpeed /= 1.2f;
            if(spawnSpeed < fastesSpeed)
            {
                spawnSpeed = fastesSpeed;
            }
            fasterSpawnTime = 0;
        }
        fasterSpawnTime += Time.deltaTime;

        //spawning += Time.time;// * Time.deltaTime;
                              // Debug.Log(spawning);

    }

	void spawn()
	{
		randomSpawn = Random.Range (0F, 199F);
		s = Instantiate(signal);
		randPos();
		s.GetComponent<Rigidbody> ().AddForce (new Vector3 (movementX, 0, movementZ), ForceMode.Impulse);
	}

    void petternSpawn()
    {
        s = Instantiate(signal);
        s.GetComponent<Rigidbody>().AddForce(new Vector3(movementX, 0, movementZ), ForceMode.Impulse);
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

    void pattern()
    {
        switch (randomPattern)
        {
            case 0:
                patternCircle();
                break;
            case 1:
                petternMultiSpawn();
                break;
        }
        if (patternCounter >= 20)
        {
            specialPattern = false;
            patternCounter = 0;
        }
    }

    void petternMultiSpawn()
    {
        if (randomizer < 250)
        {
            movementX *= 1;
            movementZ *= 1;
        }
        else if (randomizer < 500)
        {
            movementX *= -1;
            movementZ *= 1;
        }
        else if (randomizer < 750)
        {
            movementX *= -1;
            movementZ *= -1;
        }
        else if (randomizer < 1000)
        {
            movementX *= 1;
            movementZ *= -1;
        }
        patternCounter++;
        petternSpawn();
    }

    void patternCircle()
    {
        if (patternCounter%2==0)
            patternMovementX *= -1;
        else
            patternMovementZ *= -1;

        movementX = patternMovementX;
        movementZ = patternMovementZ;
        patternCounter++;
        Debug.Log(patternCounter);
        petternSpawn();
    }
}
