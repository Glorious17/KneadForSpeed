using UnityEngine;
using System.Collections;

public class Solidity : MonoBehaviour {

    private float strength = 100;
    private float failpoints;
    private float scale;

	// Use this for initialization
	void Start () {
        scale = gameObject.transform.localScale.x;
    }

    public void hit(int i) //input 0 = perfekt; 1 = ok; 2 = fail;
    {
        switch (i)
        {
            case 0:
                failpoints = 0;
                strength += 5;

                if (strength > 100)
                    strength = 100;
                break;
            case 1:
                failpoints += 0.2f;
                strength -= failpoints;
                break;
            case 2:
                failpoints += 1;
                strength -= failpoints;
                break;
        }
        transform.localScale = new Vector3(scale * (strength / 100), 1, 0.5f);

        // Debug, falls die Skalierung zu groß wird
        if (transform.localScale.x < 0)
            transform.localScale = new Vector3(0, 0, 0);

        Debug.Log("LocalScale: " + transform.localScale.x + " Strength: " + strength);
    }

    public float getStrength()
    {
        return strength;
    }
}
