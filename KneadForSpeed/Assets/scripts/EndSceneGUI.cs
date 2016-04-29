using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EndSceneGUI : MonoBehaviour {

    private List<int> wire = new List<int>();

    public GameObject wire_thin, wire_medium, wire_thick, wire_fat, invisible;


    // Use this for initialization
    void Start () {
        wire = GameObject.Find("Main Camera").GetComponent<Touchscript>().Feedback;

        plotWire();
    }


    void OnGUI() {
		int buttonWidth = 200, buttonHeight = 100;
		GUILayout.BeginArea (new Rect (Screen.width-Screen.width/2 - buttonWidth/2, Screen.height - Screen.height / 2 - buttonHeight / 2, buttonWidth, buttonHeight));
		
		if (GUILayout.Button("Back to the Menu"))
		{
			Application.LoadLevel(1);
		}
		GUILayout.EndArea ();
	}

    public void plotWire()
    {
        Vector3 wirePos = new Vector3(-6, 0, 0); //Startposition of first wire part

		foreach (int part in wire)
        {
            switch (part) //reading out value of wire evaluation (1 = super, 2= good, 3 = meh, 4 = baaad)
            {
				case 1:
					GameObject spawnThin = (GameObject)Instantiate (wire_thin, wirePos, Quaternion.Euler (0, 0, -90));
					spawnThin.transform.parent = invisible.transform;    
					break;
				case 2:
					GameObject spawnMedium = (GameObject)Instantiate (wire_medium, wirePos, Quaternion.Euler (0, 0, -90));
					spawnMedium.transform.parent = invisible.transform;
					break;
                case 3:
					GameObject spawnThick = (GameObject)Instantiate (wire_thick, wirePos, Quaternion.Euler (0, 0, -90));
					spawnThick.transform.parent = invisible.transform;
					break;
				case 4:
					Instantiate (wire_fat, wirePos, Quaternion.Euler (0, 0, -90));
					Debug.Log ("Ups, das sollte nicht passieren O.O");
					break;
                default:
                    Debug.Log("Out of Bounds");
                    break;
            }
            wirePos.x += 1; //translating the next wire part
        }
    }
}
