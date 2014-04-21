using UnityEngine;
using System.Collections;

public class DeleteConfirmGUI : MonoBehaviour {
	
	public int id;
	public string playerName;
	public bool error = false;
	private DeletePlayerGUI deleteCode;
	
	void OnGUI()
	{
		int w = 300;
		int h = 120;
		int o = 100;
		int x = (Screen.width - w) / 2;
		int y = ((Screen.height - (h*3/2)) / 3);
		Rect popup = new Rect (x,y,w,h);
		
		string message = "Confirm: Delete " + playerName + "?" + 
			System.Environment.NewLine + "Warning: This can not be undone.";
		
		if (error)
		{
			message = "Cannot delete Player "+ playerName +  
				System.Environment.NewLine + "while " + playerName + " is logged in.";
			o = 0;
		}
		
		GUI.Box (popup, message);
		
		if (!error)
		{
			if(GUI.Button (new Rect(((Screen.width-80)/2)-o,(Screen.height-50)/3,80,50), "Confirm"))
			{
				GameObject playerRef = GameObject.Find("Player Info");
				PlayerInfoBehavior playerCode = playerRef.GetComponent<PlayerInfoBehavior>();
				playerCode.deletePlayer(id);
				
				GameObject mainMenu = GameObject.Find("Main Menu");
				deleteCode = mainMenu.GetComponent<DeletePlayerGUI>();
				deleteCode.Popped = false;
				Destroy(this);
			}
		}
		
		if(GUI.Button (new Rect(((Screen.width-80)/2)+o,(Screen.height-50)/3,80,50), "Cancel"))
		{
			GameObject mainMenu = GameObject.Find("Main Menu");
			deleteCode = mainMenu.GetComponent<DeletePlayerGUI>();
			deleteCode.Popped = false;
			Destroy(this);
		}
	}
}