using UnityEngine;
using System.Collections;

public class PlayerMenuGUI : MonoBehaviour {

	void OnGUI()
	{			
		GameObject playerRef = GameObject.Find ("Player Info");
		PlayerInfoBehavior playerCode = playerRef.GetComponent<PlayerInfoBehavior>();
		
		GUIStyle Title = new GUIStyle();
		Title.fontSize = 45;
		Title.normal.textColor = Color.white;
		Title.alignment = TextAnchor.MiddleCenter;
		
		
		GUI.Label (new Rect((Screen.width/2)-200, (Screen.height/2)-300, 400, 100), "Select Player", Title);	
		
		
		for (int i = 0; i < 6; i++)
		{
			int w = 250;
			int h = 40;
			int o = 0;
			int x = Screen.width;
			int y = Screen.height;
			
			Rect buttons = new Rect((x-w)/2, ((y-((3*o)+(4*h)))/2)+((h+o)*i), w, h);
			
			string label = "Add New Player";
			
			if(i == 0 || i > 0 && playerCode.playerNameList[i] != "")
			{
				label = playerCode.playerNameList[i];
			}
			
			if (i > 0 && playerCode.playerNameList[i-1] == "")
			{
				GUI.enabled = false;
				label = "- Empty -";
			}
			
			if (GUI.Button (buttons, label))
			{
				if (label == "Add New Player")
				{
					this.gameObject.AddComponent<NewPlayerMenu>();
					NewPlayerMenu newPlayerCode = this.gameObject.GetComponent<NewPlayerMenu>();
					newPlayerCode.ID = i;
					Destroy(this);
				}
				else
				{
					playerCode.changePlayer(i);
					this.gameObject.AddComponent<MainMenuGUI>();
					Destroy(this);
				}
			}
		}
		
		GUI.enabled = true;
		
		if(GUI.Button (new Rect(((Screen.width-120)/2)+65,(Screen.height*3)/4,120,40), "Cancel"))
		{
			this.gameObject.AddComponent<MainMenuGUI>();
			Destroy(this);
		}
		
		if(GUI.Button (new Rect(((Screen.width-120)/2)-65,(Screen.height*3)/4,120,40), "Delete"))
		{
			this.gameObject.AddComponent<DeletePlayerGUI>();
			Destroy(this);
			
//			PlayerPrefs.DeleteAll ();
		}
	}
}
