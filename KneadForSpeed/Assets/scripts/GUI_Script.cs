using UnityEngine;
using System.Collections;

public class GUI_Script : MonoBehaviour {

	public static double score;
	private static int multiplicity;
    private static int combo;

	public bool isPaused = false;

	private float screenWidth = Screen.width;
	private float screenHeight = Screen.height;
	public GUIStyle schrift = new GUIStyle();
	public GUIStyle buttons = new GUIStyle();
	private GUIStyle pauseButton = new GUIStyle();

	void Start(){
		score = 0;
		multiplicity = 1;
		combo = 0;
	}


	void OnGUI() {
		schrift.fontSize = (int)screenWidth / 38;
		buttons.fixedWidth = screenWidth / 6;
		buttons.fixedHeight = screenHeight / 6;
		buttons.fontSize = (int)screenWidth / 30;
		pauseButton.fixedWidth = screenWidth / 6;
		pauseButton.fixedHeight = screenHeight / 4;

		Vector2 contentOffset = buttons.contentOffset;
		contentOffset.y = buttons.fixedHeight/3.3333f;
		buttons.contentOffset = contentOffset;
		 
		GUILayout.BeginArea (new Rect(Screen.width /2.35f, Screen.height / 2.5f, 200, 200));
		if (!isPaused) {	
			if (GUILayout.Button ("", pauseButton)) {
				isPaused = true;
				Time.timeScale = 0;
			}
		} else {
			GUILayout.Label("", schrift);
			GUILayout.Label("", schrift);
			GUILayout.Label("    Pausiert", schrift);
		}
		GUILayout.EndArea ();
		
		GUILayout.BeginArea (new Rect(Screen.width / 2 - (buttons.fixedWidth/2), Screen.height / 3, 300, (int)Screen.height));
		if (isPaused) {
			if(GUILayout.Button("Weiter", buttons)){
				isPaused = false;
				Time.timeScale = 1;
			}
			GUILayout.Label ("");
			if(GUILayout.Button("Menü", buttons)){
				Application.LoadLevel(1);
				Time.timeScale = 1;
			}

		}
		GUILayout.EndArea ();


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

        combo = 0;
		//beim verfehlen des Signals erhält man keine Punkte
        //score += multiplicity * 50;
	}

    public static void failed()
    {
        combo = 0;
        multiplicity = 1;
    }
}
