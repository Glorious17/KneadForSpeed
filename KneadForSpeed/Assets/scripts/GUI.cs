using UnityEngine;
using System.Collections;

public class GUI : MonoBehaviour {

	public static double score;
	private static int multiplicity=1;
    private static int combo = 0;

	void OnGUI() {
		GUILayout.BeginArea(new Rect(Screen.width - 105, 5, 100, 80));

		GUILayout.Label("Combo: "+ combo);
		GUILayout.EndArea();

		GUILayout.BeginArea(new Rect(Screen.width -105, 38, 100, 80));

		GUILayout.Label("Score: " + score);
		GUILayout.EndArea();

	}

	public static void good (){

		combo++;

        if(combo%10 == 0)
        {
            multiplicity += 1;
        }
        score += multiplicity * 300;

	}

	public static void medium (){

        combo++;
        score += multiplicity * 100;
	}

	public static void bad (){

        combo++;
        score += multiplicity * 50;
	}

    public static void failed()
    {
        combo = 0;
        multiplicity = 1;
    }
}
