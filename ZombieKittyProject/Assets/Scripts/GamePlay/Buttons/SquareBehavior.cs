using UnityEngine;
using System.Collections;

public class SquareBehavior : MonoBehaviour {
	
	public bool isFull;
	public bool isActive = false;
	
	private DrawButtonBehavior drawCodeRef;
	private BoardBehavior boardCode;
	private PlayerInfoBehavior playerCode;
	
	private int errorPenaltyMultiplier = 1;
	private int timePenaltyMultiplier = 0;
	
	void Update () {
		GameObject drawButton = GameObject.Find("DrawButton");
		drawCodeRef = drawButton.GetComponent<DrawButtonBehavior>();
		
		GameObject boardRef = GameObject.Find("board");
		boardCode = boardRef.GetComponent<BoardBehavior>();
		
		GameObject playerRef = GameObject.Find ("Player Info");
		playerCode = playerRef.GetComponent<PlayerInfoBehavior>();
	}
	
	void OnMouseDown(){
		boardCode.clicking = true;
	}
		
	void OnMouseUp(){
		boardCode.clicking = false;
	}
	
	void OnMouseOver(){
		if (boardCode.clicking && !isActive && playerCode.playing)
		{
			if (drawCodeRef.draw)
			{
				if(isFull)
				{
					this.renderer.material.color = Color.green;
					playerCode.levelScore += 10;
				}
				else
				{
					this.renderer.material.color = Color.black;
					timePenaltyMultiplier++;
					playerCode.timePenalty += 30 * timePenaltyMultiplier;
					boardCode.clicking = false;
				}
			}
			else
			{
				if (isFull)
				{
					this.renderer.material.color = Color.red;
					playerCode.lives++;
					errorPenaltyMultiplier *= 2;
					playerCode.levelScore -= 50 * errorPenaltyMultiplier;
					boardCode.clicking = false;
				}
				else
				{
					this.renderer.material.color = Color.grey;
				}
			}
			isActive = true;
		}
	}
}
