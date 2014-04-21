using UnityEngine;
using System.Collections;

public class GameBoardBehavior : MonoBehaviour {
	
	public int levelCode; 
	public int[][,] clues = new int[2][,];
	static bool endBox = false;
	public bool Popped = false;
	private float PauseDuration = 0.0F;
	private float PauseStart = 0.0F;
	private float PauseEnd = 0.0F;
	
	
	private float d = 1.9f;
	
	private bool doOnce;
	private float startTime;
	private int restSeconds;
	private int roundedRestSeconds;
	private int displaySeconds;
	private int displayMinutes;
	private FiveXFive libraryCode;
	
	public int countDownSeconds;
	private bool counting = true;
	public bool timeOut = false;
	private bool doOnceTimer = true;
	public bool alive = true;
	
	private PlayerInfoBehavior playerScore;
	
	// Run On Startup
	void Start()
	{
		// Connect to Puzzle Library
		GameObject libraryRef = GameObject.Find ("Library");
		libraryCode = libraryRef.GetComponent<FiveXFive>();
		
		// Connect to Player Info
		GameObject playerRef = GameObject.Find ("Player Info");
		playerScore = playerRef.GetComponent<PlayerInfoBehavior>();

		// Launch the Puzzle generator
		launch (libraryCode.puzzle[levelCode]);

		// Set the Timer based on Puzzle Size
		countDownSeconds = (libraryCode.puzzle[levelCode].GetLength(0)*60)+1;
	}
	
	// Set Start Time when Awake
	void Awake()
	{
		startTime = Time.time;
	}
	
	// Puzzle Generator Code
	void launch (bool[,] levelData) {
		PauseDuration = 0.0F;
		doOnce = true;
		doOnceTimer = true;
		alive = true;
		
		// Create the Board Reference.		
		GameObject board = new GameObject();
		board.name = "board";
		board.transform.position = new Vector3 (0,0,0);
		board.AddComponent("BoardBehavior");
		
		// Create the Draw Button
		GameObject draw = GameObject.CreatePrimitive(PrimitiveType.Cube);
		draw.name = "DrawButton";
		draw.transform.position = new Vector3 (-2.303032F, -3, 0);
		draw.transform.localScale = new Vector3 (0.5F, 0.5F, 0.2F);
		draw.transform.Rotate(0, 0, 180);
		draw.AddComponent<DrawButtonBehavior>();
		draw.transform.parent = board.transform;
		
		
		// Create the X Button
		GameObject xButton = GameObject.CreatePrimitive(PrimitiveType.Cube);
		xButton.name = "XButton";
		xButton.transform.position = new Vector3 (-1.556281F, -3, 0);
		xButton.transform.localScale = new Vector3 (0.5F, 0.5F, 0.2F);
		xButton.transform.Rotate(0, 0, 180);
		xButton.AddComponent<XButtonBehavior>();
		xButton.transform.parent = board.transform;
		
		// Create the field of clickable cells within the puzzle.
		for (int i = 0; i < levelData.GetLength(0); i++)
		{
			for (int j = 0; j < levelData.GetLength(1); j++)
			{
				GameObject space = GameObject.CreatePrimitive(PrimitiveType.Cube);
				space.name = "square" + i.ToString("00") + "_" + j.ToString("00");
				space.tag = "square";
				space.transform.localScale = new Vector3 (0.5f,0.5f,0.5f);
				float y = levelData.GetLength(0)-i;
				float x = j-((levelData.GetLength(1)-1)/2);
				space.transform.localPosition = new Vector3 (x/d, y/d, 0);
				space.renderer.material.color = new Color (0.8F,0.8F,0.8F,1.0F);
				space.AddComponent("SquareBehavior");
				space.transform.parent = board.transform;
				SquareBehavior spaceCode = space.GetComponent<SquareBehavior>();
				spaceCode.isFull = levelData[i, j];
			}
		}
		
		// Create the Life Indicators
		for (int i = 0; i < 4; i++)
		{
			GameObject newLife = GameObject.CreatePrimitive(PrimitiveType.Cube);
			newLife.name = "Life" + i.ToString("00");
			newLife.transform.position = new Vector3 (3, (5-i)/d, 0);
			newLife.transform.localScale = new Vector3 (0.5F, 0.5F, 0.2F);
			newLife.transform.Rotate(0, 0, 180);
			newLife.transform.parent = board.transform;
			newLife.renderer.material.color = Color.white;
			newLife.renderer.material.mainTexture =  Resources.Load("cat") as Texture;
		}
		
		// Populate the Clues[0] array.
		int h = (int)Mathf.Ceil(levelData.GetLength(1)/2.0F); //3
		clues[0] = new int[levelData.GetLength(0),h]; //[5,3]
		for (int j = 0; j < levelData.GetLength(0); j++) //j = 0...4
		{	
		h = (int)Mathf.Ceil(levelData.GetLength(1)/2.0F)-1;
			for (int i = levelData.GetLength(1)-1; i >= 0; i--) //i = 4...0
			{
				if (h >= 0)
				{
					if (levelData[j,i])
					{
						clues[0][j,h]++;
					}
					else
					{
						if(clues[0][j,h]>0)
						{
							h--;
						}
					}
				}
			}
		}
		
		// Populate the Clues[1] array.
		h = (int)Mathf.Ceil(levelData.GetLength(0)/2.0F);
		clues[1] = new int[levelData.GetLength(1),h];
		for (int j = 0; j < levelData.GetLength(1); j++)
		{
		h = (int)Mathf.Ceil(levelData.GetLength(0)/2.0F)-1;
			for (int i = levelData.GetLength(0)-1; i >= 0; i--)
			{
				if(h >= 0)
				{
					if (levelData[i,j])
					{
						clues[1][j,h]++;
					}
					else
					{
						if(clues[1][j,h]>0)
						{
							h--;
						}
					}
				}
			}
		}
	
		// Generate the numbers from the Clues[0] & Clues[1] arrays
		for (int k = 0; k < 2; k++)
		{
			for (int i = 0; i < clues[k].GetLength(0); i++) //0...4
			{
				for (int j = (int)Mathf.Ceil(levelData.GetLength(0)/2.0F)-1; j >= 0; j--) //2..0
				{					
					if(clues[k][i,j]>0)
					{
						GameObject number = new GameObject("Text3D");
						TextMesh numberClue = number.AddComponent<TextMesh>();
						Font fontResource = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
						numberClue.text = clues[k][i,j].ToString();
						numberClue.tag = "clue";
						numberClue.font = fontResource;
						numberClue.fontSize = 80;
						numberClue.anchor = TextAnchor.MiddleCenter;
						numberClue.renderer.material = fontResource.material;
						number.transform.localScale = new Vector3 (0.05f,0.05f,0.02f);
						float y = levelData.GetLength(0);
						float x = levelData.GetLength(1);
						if (x%2 == 0)
						{
							x--;
						}
						float o = Mathf.Ceil(levelData.GetLength(0)/2.0F); 
						if (k == 0)
						{
							number.transform.localPosition = new Vector3 ((-x+j)/d, (y-i)/d, 0);
						}
						else
						{
							number.transform.localPosition = new Vector3 ((-x+o+i)/d, ((y+o)-j)/d, 0);
						}
						number.transform.parent = board.transform;
					}
				}
			}
		}
	}
	
	// Check for Victory or Defeat every frame.
	void Update()
	{
		if (!Popped)
		{	
			// Test if all the lives are gone.
			for (int i = 0; i < 4; i++)
			{
				if(playerScore.lives > i)
				{
					GameObject life = GameObject.Find("Life" + i.ToString ("00"));
					life.renderer.material.color = Color.red;
				}
				if(playerScore.lives == 4)
				{
					alive = false;
					playerScore.playing = false;
				}
			}
			
			GameObject[] numberclues = GameObject.FindGameObjectsWithTag("clue");
			foreach (GameObject clue in numberclues)
			{
				clue.renderer.enabled = true;
			}
			
			// Test if all cells are "active".
			GameObject[] field = GameObject.FindGameObjectsWithTag("square");
			SquareBehavior[] fieldCode = new SquareBehavior[field.Length];
			for (int i = 0; i < field.Length; i++)
			{
				fieldCode[i] = field[i].GetComponent<SquareBehavior>(); 
			}
			bool finished = true;
			for (int i = 0; i < fieldCode.Length; i++)
			{
				if (!fieldCode[i].isActive)
				{
					finished = false;
				}
			}
			if(finished)
			{
				endBox = true;
				counting = false;
				
				// Add Timer Score to Level Score
				if (doOnceTimer)
				{
					playerScore.levelScore += (roundedRestSeconds * 10);
					doOnceTimer = false;
				}
				playerScore.savedScores[levelCode] = playerScore.levelScore;
			}
		}
		else
		{
			GameObject[] numberclues = GameObject.FindGameObjectsWithTag("clue");
			foreach (GameObject clue in numberclues)
			{
				clue.renderer.enabled = false;
			}
		}
	}
	
	// Control the User Interface.
	void OnGUI () 
	{	
		if (Popped)
		{
			GUI.enabled = false;
		}
		
		// Pause Button
		Rect pauseButton = new Rect (Screen.width-120, Screen.height-120, 60, 60);
		
		if (GUI.Button (pauseButton, "Pause"))
		{
			PauseButton("Paused", true);
		}
		
		// Score, High Score and Counter
		if (counting && !timeOut && alive && !Popped)
		{
			float guiTime = Time.time - (startTime + PauseDuration);
			restSeconds = (int)(countDownSeconds - (guiTime));
		}
		roundedRestSeconds = Mathf.CeilToInt (restSeconds) - playerScore.timePenalty;
		displaySeconds = roundedRestSeconds % 60;
		displayMinutes = roundedRestSeconds / 60;
		string text;
		if (roundedRestSeconds > 0)
		{ 
			text = string.Format("{0:00}:{1:00}", displayMinutes, displaySeconds); 
		}
		else
		{
			text = "00:00";
		}
		GUI.Label (new Rect (400,25,100,30), "Time: " + text);
		GUI.Label (new Rect (400,50,100,30), "Score: "+ playerScore.levelScore.ToString());
		GUI.Label (new Rect (400,75,100,30), "High Score: " + playerScore.highScores[levelCode]);
		if(roundedRestSeconds <= 0)
		{
			timeOut = true;
			endBox = true;
			playerScore.playing = false;
		}
		
		// Launch Popup and and test if last of puzzles. 
		if(endBox || !alive)
		{
			if (playerScore.playing)
			{
				if (doOnce)
				{
					if (levelCode < libraryCode.puzzle.Length)
					{
						GameObject playerInfo = GameObject.Find ("Player Info");
						PlayerInfoBehavior playerCode = playerInfo.GetComponent<PlayerInfoBehavior>();
						if (playerCode.puzzleLock == levelCode)
						{
							playerCode.puzzleLock++;
						}
					}
					doOnce = false;
					PopUp ("Congradulations! You solved it!", true);
				}
			}
			else
			{
				if (doOnce)
				{
					if (!alive)
					{
						PopUp ("You've murdered too many Healthy Cats", false);
					}
					else
					{
						PopUp ("You've been bitten by too many Zombie Cats!", false);
					}
					doOnce = false;
				}
			}
		}
	}
	
	void PopUp(string message, bool win)
	{
		Popped = true;
		GameObject noticeRef = GameObject.Find ("Notice");
		NoticeBehavior noticeCode = noticeRef.AddComponent<NoticeBehavior>();
		noticeCode.message = message;
		noticeCode.win = win;
		noticeCode.levelCode = levelCode;
	}
	
	void PauseButton(string message, bool win)
	{
		Popped = true;
		PauseStart = Time.time;
		GameObject noticeRef = GameObject.Find ("Notice");
		noticeRef.gameObject.AddComponent<PauseBehavior>();
	}
	
	void ClearLevel()
	{
		GameObject all = GameObject.Find ("board");
		Destroy (all);
		endBox = false;
		playerScore.ResetScores();
	}
	
	void loadMenu()
	{
		this.gameObject.AddComponent<EasyMenuGUI>();
		Destroy(this);
	}
	
	public void PopUpButton(int x)
	{
		switch(x)
		{
			case 0:
				ClearLevel ();
				loadMenu ();
				break;
			case 1:
				ClearLevel ();
				launch (libraryCode.puzzle[levelCode]);
				counting = true;
				startTime = Time.time;
				break;
			case 2:
				ClearLevel ();
			if(levelCode < (libraryCode.puzzle.Length - 1))
			{
				levelCode++;
				launch (libraryCode.puzzle[levelCode]);
				counting = true;
				startTime = Time.time;
			}
			else
			{
				loadMenu();
			}
				break;
		}	
	}
	
		public void PauseButtonMenu(int x)
	{
		switch(x)
		{
			case 0:
				ClearLevel ();
				loadMenu ();
				break;
			case 1:
				ClearLevel ();
				launch (libraryCode.puzzle[levelCode]);
				counting = true;
				startTime = Time.time;
				break;
			case 2:
				PauseEnd = Time.time;
				PauseDuration += (PauseEnd - PauseStart);
				break;
		}	
	}
}