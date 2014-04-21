using UnityEngine;
using System.Collections;

public class XButtonBehavior : MonoBehaviour {
	
	private DrawButtonBehavior drawCodeRef;
	
	void Start()
	{
		this.gameObject.renderer.material.mainTexture =  Resources.Load("gun") as Texture;
	}
	
	void Update()
	{	
		GameObject drawButton = GameObject.Find("DrawButton");
		drawCodeRef = drawButton.GetComponent<DrawButtonBehavior>();
		
		if (!drawCodeRef.draw)
		{
			this.renderer.material.color = new Color (1.0F, 1.0f, 1.0f, 1.0f);
		}
		else
		{
			this.renderer.material.color = new Color (0.5F, 0.5f, 0.5f, 1.0f);
		}	
	}
	
	void OnMouseUpAsButton()
	{
		if(drawCodeRef.draw)
		{
			drawCodeRef.draw = false;
		}
	}
}
