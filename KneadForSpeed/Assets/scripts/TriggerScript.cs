using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TriggerScript : MonoBehaviour {

    List<GameObject> signals = new List<GameObject>();

    public GameObject Signals   // Accessor
    {
        get
        {
            return signals[0];
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Signal")
        {
            signals.Add(col.gameObject);
            Debug.Log("signalINNNNN!!!");
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Signal")
        {
            signals.Remove(col.gameObject);
            Debug.Log("signalOUT!!!");
        }
    }

}
