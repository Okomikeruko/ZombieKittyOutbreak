using UnityEngine;
using System.Collections;

public class PlayMenuGUI : MonoBehaviour {
	
	void OnGUI()
	{
		Rect[] buttons = new Rect[4];
		
		for (int i = 0; i < 4; i++)
		{
			int w = 120;
			int h = 40;
			int o = 12;
			int x = Screen.width;
			int y = Screen.height;
			
			buttons[i] = new Rect((x-w)/2, ((y-((3*o)+(4*h)))/2)+((h+o)*i), w, h);
		}
		GUIStyle Title = new GUIStyle();
		Title.fontSize = 45;
		Title.normal.textColor = Color.white;
		Title.alignment = TextAnchor.MiddleCenter;
		
		GUI.Label (new Rect((Screen.width/2)-200, (Screen.height/2)-300, 400, 100), "Play Menu", Title);
		
		if(GUI.Button (buttons[0], "Story Mode"))
		{
			this.gameObject.AddComponent<StoryMenuGUI>();
			Destroy(this);
		}

		if(GUI.Button (buttons[1], "Puzzle Mode"))
		{
			this.gameObject.AddComponent<PuzzleModeGUI>();
			Destroy(this);
		}
		
		if(GUI.Button (buttons[2], "Creator"))
		{
			Debug.Log("Load the Creator Menu");
//			Destroy(this);
		}

		if(GUI.Button (buttons[3], "Main Menu"))
		{
			this.gameObject.AddComponent<MainMenuGUI>();
			Destroy(this);
		}
	}
}
