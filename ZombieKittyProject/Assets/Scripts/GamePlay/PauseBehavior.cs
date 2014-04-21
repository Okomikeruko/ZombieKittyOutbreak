using UnityEngine;
using System.Collections;

public class PauseBehavior : MonoBehaviour {

	void OnGUI()
	{
		int w = 300;
		int h = 120;
		int x = (Screen.width - w) / 2;
		int y = ((Screen.height - (h*3/2)) / 2);
		Rect popup = new Rect (x,y,w,h);
		GUI.Box (popup, "Pause");
		
		string[] buttons = new string[] {"Menu", "Restart", "Resume"};
		
		for(int i = 0; i < 3; i++)
		{
			if(GUI.Button (new Rect(((Screen.width-80)/2)+(100*(i-1)),(Screen.height-50)/2,80,50), buttons[i]))
			{
				GameObject main = GameObject.Find("Main Menu");
				GameBoardBehavior mainCode = main.GetComponent<GameBoardBehavior>();
				mainCode.PauseButtonMenu(i);
				mainCode.Popped = false;
				Destroy(this);
			}
		}
	}
}
