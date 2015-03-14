//*******************************************************************************
//	  Adapted from "PauseMenu.js" by Grady Featherstone, � Copyright 2011
//*******************************************************************************


var pauseMenuSprite : Sprite;
var pauseMenuFont : Font;
private var pauseEnabled = false;			

function Start(){
	pauseEnabled = false;
	Time.timeScale = 1;
	AudioListener.volume = 1;
	Cursor.visible = false;
}

function Update(){

	//check if pause button (escape key) is pressed
	if(Input.GetKeyDown("escape")){
	
		//check if game is already paused		
		if(pauseEnabled == true){
			//unpause the game
			pauseEnabled = false;
			Time.timeScale = 1;
			AudioListener.volume = 1;
			Cursor.visible = false;			
		}
		
		//else if game isn't paused, then pause it
		else if(pauseEnabled == false){
			pauseEnabled = true;
			AudioListener.volume = 0;
			Time.timeScale = 0;
			Cursor.visible = true;
		}
	}
}

private var showOptionsDropDown = false;

function OnGUI(){

GUI.skin.box.font = pauseMenuFont;
GUI.skin.button.font = pauseMenuFont;

	if(pauseEnabled == true){
		
		//Make a background box
		GUI.Box(Rect(Screen.width /2 - 100,Screen.height /2 - 50,250,100), "");
		
/*
		//Make Change Options button
			if(GUI.Button(Rect(Screen.width /2 - 100,Screen.height /2 - 50 ,250,50), "Options")){
			
			if(showOptionsDropDown == false){
				showOptionsDropDown = true;
			}
			else{
				showOptionsDropDown = false;
			}
		}
*/
		
/*
		if(showOptionsDropDown == true){
			if(GUI.Button(Rect(Screen.width /2 + 150,Screen.height /2 ,250,50), "Fastest")){
				QualitySettings.currentLevel = QualityLevel.Fastest;
			}
			if(GUI.Button(Rect(Screen.width /2 + 150,Screen.height /2 + 50,250,50), "Fast")){
				QualitySettings.currentLevel = QualityLevel.Fast;
			}
			if(GUI.Button(Rect(Screen.width /2 + 150,Screen.height /2 + 100,250,50), "Simple")){
				QualitySettings.currentLevel = QualityLevel.Simple;
			}
			if(GUI.Button(Rect(Screen.width /2 + 150,Screen.height /2 + 150,250,50), "Good")){
				QualitySettings.currentLevel = QualityLevel.Good;
			}
			if(GUI.Button(Rect(Screen.width /2 + 150,Screen.height /2 + 200,250,50), "Beautiful")){
				QualitySettings.currentLevel = QualityLevel.Beautiful;
			}
			if(GUI.Button(Rect(Screen.width /2 + 150,Screen.height /2 + 250,250,50), "Fantastic")){
				QualitySettings.currentLevel = QualityLevel.Fantastic;
			}
			
			if(Input.GetKeyDown("escape")){
				showOptionsDropDown = false;
			}
		}
*/

		//Make Main Menu button
		if(GUI.Button(Rect(Screen.width /2 - 100,Screen.height /2 - 50,250,50), "Quit to Main Menu")){
			Application.LoadLevel("MainMenu");
		}
		
		//Make quit game button
		if (GUI.Button (Rect (Screen.width /2 - 100,Screen.height /2,250,50), "Quit to Desktop")){
			Application.Quit();
		}
	}
}