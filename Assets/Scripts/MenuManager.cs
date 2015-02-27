using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour {

	public Texture2D cursorTexture;

	public string levelToPlay;

	public void OnClickMenu () {
        Application.LoadLevel(levelToPlay);
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
		Application.LoadLevel(PlayerPrefs.GetString("LastLoadedLevel"));
	}
	
	// Use this for initialization
	void Start () {

		//CursorMode cursorMode = CursorMode.Auto;
		//Vector2 hotSpot = Vector2.zero;
        //Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
		//Screen.showCursor = true;

	}
	
}
