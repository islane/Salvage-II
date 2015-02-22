﻿using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

	public bool isWinning = false;//open the right door and you win the game
	public Rigidbody2D rigidBody;

	void Start(){
		//rigidBody = gameObject.GetComponent<Rigidbody2D>();
	}

	public void Open(){
		gameObject.SetActive(false);
	}

	public void Close(){
		gameObject.SetActive(true);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		Robot robot = other.gameObject.GetComponent<Robot>();
		
		//Trigger if the object is the player controlled robot
		if (robot != null && robot.IsControlledCharacter())
		{
			Application.LoadLevel("WinScene");
			//global.currentRobot = ; //TODO: Do we want to switch to the new robot automaticaly?
		}
		
	}
}
