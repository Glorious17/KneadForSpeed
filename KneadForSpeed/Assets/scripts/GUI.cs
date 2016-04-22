using UnityEngine;
using System.Collections;

public class GUI : MonoBehaviour {

	public static int score;
	private static int hits = 0;
	private static int multiplicity = 0;

	void OnGUI() {
		GUILayout.BeginArea(new Rect(Screen.width - 105, 5, 100, 80));

		GUILayout.Label("Multiplikator: "+ multiplicity);
		GUILayout.EndArea();

		GUILayout.BeginArea(new Rect(Screen.width -105, 38, 100, 80));

		GUILayout.Label("Score: " + score);
		GUILayout.EndArea();

	}

	public static void good (){
		
		hits++;
		if (hits % 3 == 0)
			multiplicity++;
		score += multiplicity;
	}

	public static void notGood (){
		score += multiplicity;
	}

	public static void bad (){
		multiplicity = 1;
		hits = 0;
	}
}
