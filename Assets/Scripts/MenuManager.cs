using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour {

	public Texture2D cursorTexture;

	public void OnClickMenu () {
        Application.LoadLevel("Level001");
	}

	public void OnClickQuit () {
        Application.Quit();
	}

	public void OnClickCredits () {
        Application.LoadLevel("Credits");
	}

	public void OnClickMain () {
        Application.LoadLevel("MainMenu");
	}


	public void OnClick2Menu () {
        Application.LoadLevel("IntroScene");
	}

	public void OnClick2Continue () {
        Application.LoadLevel("Level001");
	}

	// Use this for initialization
	void Start () {
		//CursorMode cursorMode = CursorMode.Auto;
		//Vector2 hotSpot = Vector2.zero;
        //Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
		//Screen.showCursor = true;

	}
	
}
