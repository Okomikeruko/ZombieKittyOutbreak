using UnityEngine;
using System.Collections;

public class DrawButtonBehavior : MonoBehaviour {

	public bool draw = true;
	
	void Start()
	{
		this.gameObject.renderer.material.mainTexture =  Resources.Load("hand") as Texture;
	}
	
	void Update()
	{
		if (draw)
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
		if(!draw)
		{
			draw = true;
		}
	}
}
