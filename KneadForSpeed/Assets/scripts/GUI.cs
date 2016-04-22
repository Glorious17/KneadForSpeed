using UnityEngine;
using System.Collections;

public class GUI : MonoBehaviour {

	public static int score;
	private static int multiplicity=0;

	void OnGUI() {
		GUILayout.BeginArea(new Rect(Screen.width - 105, 5, 100, 80));

		GUILayout.Label("Multiplikator: "+ multiplicity);
		GUILayout.EndArea();

		GUILayout.BeginArea(new Rect(Screen.width -105, 38, 100, 80));

		GUILayout.Label("Score: " + score);
		GUILayout.EndArea();

	}

	public static void good (){

		multiplicity++;
		if (score == 0) {
			score++;
		}else
			score= score+multiplicity*score;

	}

	public static void notGood (){
		if (multiplicity > 0)
			score = score + multiplicity*score;
		else
			score++;
	}

	public static void bad (){
		multiplicity = 0;
	}
}
