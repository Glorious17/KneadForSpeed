using UnityEngine;
using System.Collections;

public class GUI : MonoBehaviour {

	public static int score;
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
        score += multiplicity * 4;

	}

	public static void medium (){

        combo++;
        score += multiplicity * 2;
	}

	public static void bad (){

        combo++;
        score += multiplicity * 1;
	}

    public static void failed()
    {
        combo = 0;
        multiplicity = 1;
    }
}
