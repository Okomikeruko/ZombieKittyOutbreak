using UnityEngine;
using System.Collections;

public class DeletePlayerGUI : MonoBehaviour {
	
	public bool Popped = false;
		
	
	void OnGUI()
	{	
		
		GameObject playerRef = GameObject.Find ("Player Info");
		PlayerInfoBehavior playerCode = playerRef.GetComponent<PlayerInfoBehavior>();
		
		GUIStyle Title = new GUIStyle();
		Title.fontSize = 45;
		Title.alignment = TextAnchor.MiddleCenter;
		
		
		GUI.Label (new Rect((Screen.width/2)-200, (Screen.height/2)-300, 400, 100), "Delete Player", Title);	
		
		
		for (int i = 0; i < 6; i++)
		{
			int w = 250;
			int h = 40;
			int o = 0;
			int x = Screen.width;
			int y = Screen.height;
			
			Rect buttons = new Rect((x-w)/2, ((y-((3*o)+(4*h)))/2)+((h+o)*i), w, h);
			
			string label = "- Empty -";
			
			if(i == 0 || i > 0 && playerCode.playerNameList[i] != "")
			{
				label = "Delete " + playerCode.playerNameList[i];
			}
			
			if (i > 0 && playerCode.playerNameList[i] == "" || Popped)
			{
				GUI.enabled = false;
			}
			
			if (GUI.Button (buttons, label))
			{
				Popped = true;
				GameObject Popup = GameObject.Find("Notice");
				DeleteConfirmGUI popupCode = Popup.AddComponent<DeleteConfirmGUI>();
				popupCode.id = i;
				popupCode.playerName = playerCode.playerNameList[i];
				if (playerCode.playerName == playerCode.playerNameList[i])
				{
					popupCode.error = true;
				}
			}
		}
		
		if(!Popped)
		{
			GUI.enabled = true;
		}
		
		if(GUI.Button (new Rect(((Screen.width-120)/2),(Screen.height*3)/4,120,40), "Back"))
		{
			this.gameObject.AddComponent<PlayerMenuGUI>();
			Destroy(this);
		}
	}
}
