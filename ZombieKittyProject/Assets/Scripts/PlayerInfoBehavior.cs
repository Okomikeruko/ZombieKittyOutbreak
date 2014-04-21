using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerInfoBehavior : MonoBehaviour {

	public int puzzleLock = 0;
	public int levelScore = 0;
	public int timePenalty = 0;
	public bool playing = true;
	public int lives = 0;
	
	public int playerID;
	public string playerName;
	public string playerCode;
	
	private string playerarray;
	
	public int[] savedScores;
	public int[] highScores;
	public string[] highScorer;
	public string[] playerNameList = new string[6];
	public string[] playerCodeList = new string[6];
	
	private FiveXFive puzzleCode;
	
	void Awake()
	{
		for (int i = 0; i < playerNameList.Length; i++)
		{
			if(PlayerPrefs.HasKey ("Player"+i.ToString("00")))
			{
				playerNameList[i] = PlayerPrefs.GetString("Player"+i.ToString("00"));
			}
		}
		
		for (int i = 0; i < playerCodeList.Length; i++)
		{
			if(PlayerPrefs.HasKey ("Player Code"+i.ToString("00")))
			{
				playerCodeList[i] = PlayerPrefs.GetString("Player Code"+i.ToString("00"));
			}
		}
				
		if(PlayerPrefs.HasKey ("Player ID"))
		{
			playerID = PlayerPrefs.GetInt ("Player ID");
			if (playerNameList[0] !="")
			{
				playerName = playerNameList[playerID];
			}
			if (playerCodeList[0] != "")
			{
				playerCode = playerCodeList[playerID];
			}
		}		
		
		if(PlayerPrefs.HasKey(playerCode+" Puzzle Lock"))
		{
			puzzleLock = PlayerPrefs.GetInt(playerCode+" Puzzle Lock");
		}
		else
		{
			puzzleLock = 0;
		}
		
		GameObject puzzles = GameObject.Find ("Library");
		puzzleCode = puzzles.GetComponent<FiveXFive>();
		savedScores = new int[puzzleCode.list.Length];
		highScores = new int[puzzleCode.list.Length];
		highScorer = new string[puzzleCode.list.Length];
	}
	
	void Update(){

		for (int i = 0; i < puzzleCode.list.Length; i++)
		{
			for (int j = 0; j < playerCodeList.Length-1; j++)
			{
				int score = PlayerPrefs.GetInt (playerCodeList[j]+" Level Score"+i.ToString("00"));
				if(highScores[i] < score)
				{
					highScores[i] = score;
					highScorer[i] = playerNameList[j];
				}
			}
		}
	}
	
	void OnGUI()
	{
		GameObject menu = GameObject.Find("Main Menu");
		NewPlayerMenu npm = menu.GetComponent<NewPlayerMenu>();
		GameBoardBehavior gbb = menu.GetComponent<GameBoardBehavior>();
		
		GameObject popup = GameObject.Find ("Notice");
		PauseBehavior p = popup.GetComponent<PauseBehavior>();
		NoticeBehavior n = popup.GetComponent<NoticeBehavior>();
		DeleteConfirmGUI d = popup.GetComponent<DeleteConfirmGUI>();
			
		bool nopop = true;
		if (p != null || n != null || d != null)
		{
			nopop = false;
		}
			
		
		if(npm == null && gbb == null && nopop)
		{
			GUIStyle PlayerInfo = new GUIStyle();
			PlayerInfo.fontSize = 30;
			PlayerInfo.alignment = TextAnchor.MiddleCenter;
			PlayerInfo.normal.textColor = Color.white;
			string playerLabel = "Player: " + playerName;
			GUI.Label (new Rect((Screen.width/2)-200, (Screen.height/2)-225, 400, 100), playerLabel, PlayerInfo);
		}
	}
	
	public void ResetScores()
	{
		levelScore = 0;
		timePenalty = 0;
		playing = true;
		lives = 0;
	}
	public void AddPlayer(string name, int id, string code)
	{
		playerName = name;
		playerCode = code;
		playerID = id;
		puzzleLock = 0;
		playerNameList[id] = name;
		playerCodeList[id] = code;

		SaveData ();
	}
	
	public void changePlayer(int id)
	{
		SaveData ();
		
		playerID = id;
		playerName = playerNameList[id];
		playerCode = playerCodeList[id];
		puzzleLock = PlayerPrefs.GetInt(playerCode+" Puzzle Lock");
		
		SaveData();
	}
	
	public void deletePlayer(int id)
	{
		PlayerPrefs.DeleteKey(playerCodeList[id]+" Puzzle Lock");
		
		for (int i = 0; i < savedScores.Length; i++)
		{
			PlayerPrefs.DeleteKey(playerCodeList[id]+" Level Score"+i.ToString("00"));
		}
		
		string[] T = new string[6];
		for (int i = 0; i < playerCodeList.Length; i++)
		{
			if (i < id)
			{
				T[i] = playerCodeList[i];
			}
			if (i > id)
			{
				T[i-1] = playerCodeList[i];
			}
		}
		T[5] = "";
		playerCodeList = T;
		
		T = new string[6];
		for (int i = 0; i < playerNameList.Length; i++)
		{
			if (i < id)
			{
				T[i] = playerNameList[i];
			}
			if (i > id)
			{
				T[i-1] = playerNameList[i];
			}
		}
		T[5] = "";
		playerNameList = T;
		
		for (int i = 0; i < playerCodeList.Length; i++)
		{
			if (playerCode == playerCodeList[i])
				playerID = i;
		}
		
		SaveData ();
	}
	
	public void SaveData()
	{
		PlayerPrefs.SetInt("Player ID", playerID);
		
		for (int i = 0; i < playerCodeList.Length; i++)
		{
			PlayerPrefs.SetString("Player Code"+i.ToString("00"), playerCodeList[i]);
		}
		
		for (int i = 0; i < playerNameList.Length; i++)
		{
			PlayerPrefs.SetString ("Player"+i.ToString("00"), playerNameList[i]);
		}
		
		PlayerPrefs.SetInt (playerCode+" Puzzle Lock", puzzleLock);
		
		for (int i = 0; i < savedScores.Length; i++)
		{
			PlayerPrefs.SetInt(playerCode+" Level Score"+i.ToString("00"), savedScores[i]);
		}
	}
}