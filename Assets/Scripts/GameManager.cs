using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public Robot currentRobot;
	public Robot robotSmall;
	public Robot robotJump;
	public Robot robotPush;


	//public 
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetKeyDown (KeyCode.Alpha1))
		{
			if(robotSmall.activated)
			{
				if (currentRobot != null) 
					currentRobot.SetAsCurrent (false);
				currentRobot = robotSmall;
			}
		}
		if(Input.GetKeyDown (KeyCode.Alpha2))
		{
			if(robotJump.activated)
			{
				if (currentRobot != null) 
					currentRobot.SetAsCurrent (false);
				currentRobot = robotJump;
			}
		}
		if (Input.GetKeyDown (KeyCode.Alpha3))
		{
			if(robotPush.activated){
				if (currentRobot != null) 
					currentRobot.SetAsCurrent (false);
				currentRobot = robotPush;
			}
		}

		if (Input.GetKeyDown (KeyCode.T))
		{
			if (currentRobot != null) 
				currentRobot.SetAsCurrent (false);
			currentRobot = null;
		}

		if(currentRobot != null)
			currentRobot.SetAsCurrent (true);
	}
}
