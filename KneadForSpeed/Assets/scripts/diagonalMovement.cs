using UnityEngine;
using System.Collections;

public class diagonalMovement : MonoBehaviour
{

    public GameObject signal;

    GameObject s;

    public float movementX = 5, movementZ = 5;

    // Use this for initialization
    void Start()
    {

        s = Instantiate(signal);

        //s.transform.position = new Vector3(s.transform.position.x, s.transform.position.y, s.transform.position.z + 5*Time.deltaTime);
    }

    // Update is called once per frame
    void Update()
    {

        s.transform.position = new Vector3(s.transform.position.x + movementX * Time.deltaTime, s.transform.position.y, s.transform.position.z + movementZ * Time.deltaTime);

    }
}
