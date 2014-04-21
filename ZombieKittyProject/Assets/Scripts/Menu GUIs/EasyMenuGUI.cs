using UnityEngine;
using System.Collections;

public class EasyMenuGUI : MonoBehaviour {
	
	private string levelLabel;
	
	void OnGUI()
	{
		GameObject library = GameObject.Find ("Library");
		FiveXFive puzzles = library.GetComponent<FiveXFive>();
		
		GameObject playerInfo = GameObject.Find ("Player Info");
		PlayerInfoBehavior playerCode = playerInfo.GetComponent<PlayerInfoBehavior>();
		int puzzleLock = playerCode.puzzleLock;
		
		Rect[] buttons = new Rect[puzzles.list.Length];
		
		for (int i = 0; i < puzzles.list.Length; i++)
		{
			int w = 60;
			int h = 60;
			int o = 20;
			int a = 5;
			int x = Screen.width;
			int y = Screen.height;
			buttons[i] = new Rect((x/2-((o*2)+(w*2.5f))+((w+o)*(i%a))), (y/2-(h+(o/2))+(h+o)*(i/a)), w, h);
		}
		GUIStyle Title = new GUIStyle();
		Title.fontSize = 45;
		Title.normal.textColor = Color.white;
		Title.alignment = TextAnchor.MiddleCenter;
		
		GUI.Label (new Rect((Screen.width/2)-200, (Screen.height/2)-300, 400, 100), "Easy Puzzles", Title);
		
		for (int i = 0; i < puzzles.list.Length; i++)
		{
			if (i > puzzleLock)
			{
				GUI.enabled = false;
			}
						
			if (i < puzzleLock)
			{
				levelLabel = puzzles.list[i];
			}
			else
			{
				levelLabel = "?";
			}
						
			if(GUI.Button (buttons[i], levelLabel))
			{
				this.gameObject.AddComponent<GameBoardBehavior>();
				GameBoardBehavior puzzleCode = this.gameObject.GetComponent<GameBoardBehavior>();
				puzzleCode.levelCode = i;
				Destroy(this);
			}	
		}
		
		Rect back = new Rect (Screen.width-120, Screen.height-120, 60, 60);
		GUI.enabled = true;
		
		if (GUI.Button (back, "Back"))
		{
			this.gameObject.AddComponent<PuzzleModeGUI>();
			Destroy(this);
		}
	}
}
