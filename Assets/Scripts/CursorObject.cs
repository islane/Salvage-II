using UnityEngine;
using System.Collections;

public class CursorObject : MonoBehaviour {
	public Texture2D cursor;

	int w = 32;
	int h = 32;
	Vector2 mouse;

	// Use this for initialization
	void Start () 
	{
		Screen.showCursor = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		mouse = new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y);
	}

	void OnGUI()
	{
		GUI.DrawTexture(new Rect(mouse.x, mouse.y, w, h), cursor);
	}
}
