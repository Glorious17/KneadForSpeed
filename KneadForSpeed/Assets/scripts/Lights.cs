using UnityEngine;
using System.Collections;

public class Lights : MonoBehaviour {

	public Light[] myLights;

	private float faktor = 6f;

	public bool[] red;
	public bool[] yellow;
	public bool[] green;

	private int maxLight = 4;

	private float lampDuration = 0.5f;

	void Start () 
	{
		red = new bool[maxLight];
		yellow = new bool[maxLight];
		green = new bool[maxLight];


		//Light position
		float aspectRatio = (float)Screen.width/(float)Screen.height;

		//rechte Lichter
		myLights[0].transform.position += new Vector3 ((aspectRatio-1)*faktor, 0, 0);
		myLights[1].transform.position += new Vector3 ((aspectRatio-1)*faktor, 0, 0);

		//linke Lichter
		myLights[2].transform.position -= new Vector3 ((aspectRatio-1)*faktor, 0, 0);
		myLights[3].transform.position -= new Vector3 ((aspectRatio-1)*faktor, 0, 0);


		for (int i = 0; i < maxLight; i++) 
		{
			myLights [i].enabled = false;
		}
	}

	// Update is called once per frame
	void Update () 
	{
		for (int i = 0; i < maxLight; i++) 
		{
			if (red [i] == true) 
			{
				red [i] = false;
				myLights[i].color = Color.red;
				myLights[i].enabled = true;
				StartCoroutine (Wait (red[i], myLights[i]));
			}
				
			if (yellow [i] == true) 
			{
				yellow [i] = false;
				myLights[i].color = Color.yellow;
				myLights [i].enabled = true;
				StartCoroutine (Wait (yellow[i], myLights[i]));

			}

			if (green [i] == true) 
			{
				green [i] = false;
				myLights[i].color = Color.green;
				myLights [i].enabled = true;
				StartCoroutine (Wait (green[i], myLights[i]));
			}
		}
	}

	IEnumerator Wait(bool color, Light myLight)
	{
		yield return new WaitForSeconds (lampDuration);
		if (color != true) 
		{
			myLight.enabled = false;
		}
	}
}
