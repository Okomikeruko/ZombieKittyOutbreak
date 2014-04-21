using UnityEngine;
using System.Collections;

public class NoticeBehavior : MonoBehaviour {
	
	public string message = "This";
	public bool win;
	public int levelCode;
	
	void OnGUI()
	{
		GameObject playerRef = GameObject.Find ("Player Info");
		PlayerInfoBehavior playerCode = playerRef.GetComponent<PlayerInfoBehavior>();
		
		playerCode.SaveData();
		
		string playerAddon = "";
		if (win)
		{
			playerAddon = System.Environment.NewLine + "Your Score: " + playerCode.playerName + " " + playerCode.levelScore + " Points!";
		}
		string levelAddon = System.Environment.NewLine + "Top Score: " + playerCode.highScorer[levelCode] + " " + playerCode.highScores[levelCode] + " Points!";
		
		int w = 300;
		int h = 120;
		int x = (Screen.width - w) / 2;
		int y = ((Screen.height - (h*3/2)) / 2);
		Rect popup = new Rect (x,y,w,h);
		GUI.Box (popup, message + playerAddon + levelAddon);
		
		string[] buttons = new string[] {"Menu", "Replay", "Next"};
		
		for(int i = 0; i < 3; i++)
		{
			if (i == 2 && !win)
			{
				GUI.enabled = false;
			}	
			if(GUI.Button (new Rect(((Screen.width-80)/2)+(100*(i-1)),(Screen.height-50)/2,80,50), buttons[i]))
			{
				GameObject main = GameObject.Find("Main Menu");
				GameBoardBehavior mainCode = main.GetComponent<GameBoardBehavior>();
				mainCode.PopUpButton(i);
				mainCode.Popped = false;
				Destroy(this);
			}
		}
	}
}
