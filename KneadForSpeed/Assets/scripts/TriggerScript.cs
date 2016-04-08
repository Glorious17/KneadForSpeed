using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TriggerScript : MonoBehaviour
{

    List<GameObject> signals = new List<GameObject>();

    public List<GameObject> Signals   // Accessor
    {
        get
        {
            return signals;
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Signal")
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
            Destroy(col.gameObject);

            Debug.Log("signalOUT!!!");
        }
    }

}
