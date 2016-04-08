using UnityEngine;
using System.Collections;

public class StartSceneGUI : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnGUI() {
		int buttonWidth = 200, buttonHeight = 100;
		GUILayout.BeginArea(new Rect(Screen.width - Screen.width / 2 - buttonWidth / 2, Screen.height - Screen.height / 2 - buttonHeight / 2, buttonWidth, buttonHeight));
		if (GUILayout.Button("Start"))
		{
			Application.LoadLevel(0);
		}
		if (GUILayout.Button("Back to the Menu"))
		{
			Debug.Log("NADA!");
		}
		GUILayout.EndArea ();
	}
}
