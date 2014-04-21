using UnityEngine;
using System.Collections;
using System.Text;

public class NewPlayerMenu : MonoBehaviour {
	
	public int ID = 0;
	
	private string[][,] keyboard = new string[2][,];
	private string playerName = "";
	private int shift = 1;
	
	void Start()
	{
	keyboard[0] = new string[,]	
	{
		{"q","w","e","r","t","y","u","i","o","p","←Backspace"},
		{"a","s","d","f","g","h","j","k","l","SHIFT",""},
		{"z","x","c","v","b","n","m","Submit","","",""}
	};
	keyboard[1] = new string[,]	
	{
		{"Q","W","E","R","T","Y","U","I","O","P","←Backspace"},
		{"A","S","D","F","G","H","J","K","L","shift",""},
		{"Z","X","C","V","B","N","M","Submit","","",""}	
	};
	}
	
	void OnGUI()
	{
		GUIStyle Title = new GUIStyle();
		Title.fontSize = 45;
		Title.alignment = TextAnchor.MiddleCenter;
		Title.normal.textColor = Color.white;
		GUI.Label (new Rect((Screen.width/2)-200, (Screen.height/2)-300, 400, 100), "New Player Name", Title);
		
		
		GUIStyle nameStyle = new GUIStyle();
		nameStyle.fontSize = 30;
		nameStyle.alignment = TextAnchor.MiddleCenter;
		nameStyle.normal.textColor = Color.white;
		GUI.Label(new Rect((Screen.width-100)/2,(Screen.height/2)-200, 100, 50), playerName, nameStyle);
		
		for (int i = 0; i < keyboard[shift].GetLength(0); i++)
		{
			for (int j = 0; j < keyboard[shift].GetLength(1); j++)
			{
				if (keyboard[shift][i,j] != "")
				{
					int w = 30;
					int l = 40;
					int x = (Screen.width/2)+((j-((keyboard[shift].GetLength(1)/2)-(i-1)))*w);
					int y = (Screen.height/2)+((i-(keyboard[shift].GetLength(0)/2))*l);
					
					if (keyboard[shift][i,j] == "shift" || keyboard[shift][i,j] == "SHIFT" || keyboard[shift][i,j] == "Submit")
					{
						w *= 2;
					}
					if (keyboard[shift][i,j] == "←Backspace")
					{
						w = w * 7 / 2;
					}
					
					Rect button = new Rect (x, y, w, l);
					if(GUI.Button (button, keyboard[shift][i,j]))
					{
						switch (keyboard[shift][i,j])
						{
						case "shift":
							shift = 0;
							break;
						case "SHIFT":
							shift = 1;
							break;
						case "←Backspace":
							if (playerName.Length > 0)
							{
								playerName = playerName.Remove(playerName.Length - 1);
							}
							break;
						case "Submit":
							GameObject playerRef = GameObject.Find ("Player Info");
							PlayerInfoBehavior playerCode = playerRef.GetComponent<PlayerInfoBehavior>();
							playerCode.AddPlayer(playerName, ID, RandomString (8));
							this.gameObject.AddComponent<MainMenuGUI>();
							Destroy(this);
							break;
						default:
							if (playerName.Length < 12)
							{
								playerName += keyboard[shift][i,j];
							}
							shift = 0;
							break;
						}
					}
				}
			}
		}
		
		
		if(GUI.Button (new Rect(((Screen.width-120)/2),(Screen.height*3)/4,120,40), "Cancel"))
		{
			this.gameObject.AddComponent<PlayerMenuGUI>();
			Destroy(this);
		}		
	}
	
	public string RandomString(int size)
	{
		string input = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
		string output = "";
		
		for (int i = 0; i < size; i++)
		{
			int j = input.Length - 1;
			char ch = input[Random.Range(0, j)];
			output += ch;
		}
		return output;
	}
}
