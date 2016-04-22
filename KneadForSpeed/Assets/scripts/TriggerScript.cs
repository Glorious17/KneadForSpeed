using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TriggerScript : MonoBehaviour {

    private List<GameObject> signals = new List<GameObject>();
    private GameObject lifebar;

    void Start()
    {
        lifebar = GameObject.Find("Lifebar");
    }

    public List<GameObject> Signals   // Accessor mit einem getter
    {
        get
        {
			return signals;
        }
    }

    void OnTriggerEnter(Collider col) //wenn Signale den Trigger betreten, werden sie der Liste beigefügt
    {
        if(col.gameObject.tag == "Signal")
        {
            signals.Add(col.gameObject);
        }
    }

    void OnTriggerExit(Collider col) //wenn die Signale den Trigger verlassen, werden sie gelöscht und aus der Liste entfernt
    {
        if (col.gameObject.tag == "Signal")
        {
            lifebar.GetComponent<Solidity>().hit(3);
            signals.Remove(col.gameObject);
			Destroy (col.gameObject);
        }
    }

}
