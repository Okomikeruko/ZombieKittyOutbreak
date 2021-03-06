﻿using UnityEngine;
using System.Collections;

public class PuzzleModeGUI : MonoBehaviour {

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
		
		GUI.Label (new Rect((Screen.width/2)-200, (Screen.height/2)-300, 400, 100), "Puzzles", Title);
		
		if(GUI.Button (buttons[0], "Easy"))
		{
			this.gameObject.AddComponent<EasyMenuGUI>();
			Destroy(this);
		}

		if(GUI.Button (buttons[1], "Intermediate"))
		{
			Debug.Log("Load the 10 x 10 Menu");
//			Destroy(this);
		}
		
		if(GUI.Button (buttons[2], "Advanced"))
		{
			Debug.Log("Load the Creator Menu");
//			Destroy(this);
		}

		if(GUI.Button (buttons[3], "Back"))
		{
			this.gameObject.AddComponent<PlayMenuGUI>();
			Destroy(this);
		}
	}
}
