using UnityEngine;
using System.Collections;

public class StoryMenuGUI : MonoBehaviour {

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
		
		GUI.Label (new Rect((Screen.width/2)-200, (Screen.height/2)-300, 400, 100), "Story Menu", Title);
		
		if(GUI.Button (buttons[0], "New Story"))
		{
			Debug.Log("Start New Story");
//			Destroy(this);
		}

		if(GUI.Button (buttons[1], "Continue"))
		{
			Debug.Log("Load Story from Last Save");
//			Destroy(this);
		}
		
		if(GUI.Button (buttons[2], "Tutorial"))
		{
			Debug.Log("Load Tutorial");
//			Destroy(this);
		}

		if(GUI.Button (buttons[3], "Back"))
		{
			this.gameObject.AddComponent<PlayMenuGUI>();
			Destroy(this);
		}
	}
}
