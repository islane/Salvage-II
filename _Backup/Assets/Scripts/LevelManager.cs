using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class LevelManager : MonoBehaviour {
	
	//Assign this in the inspector to tell GameManager which robot to start the level with.
	public Robot currentRobot;
	
	//A list of activated robots
	List<Robot> robotList;
	//The index of the current robot in the list
	int robotIndex;
	// Key = name of the button used to switch to a robot, Value = robotIndex
	static Dictionary<string,int> RobotKeys;
	
	// Use this for initialization
	void Start () {

		//GameManager.Instance.lastLoadedLevel = Application.loadedLevelName;
		PlayerPrefs.SetString("LastLoadedLevel", Application.loadedLevelName);
		
		if (currentRobot == null)
			Debug.Log ("Please assign a starting robot to the GameManager in the inspector.");
		
		RobotKeys = new Dictionary<string, int>();
		RobotKeys.Add ("Robot1", 0);
		RobotKeys.Add ("Robot2", 1);
		RobotKeys.Add ("Robot3", 2);
		RobotKeys.Add ("Robot4", 3);
		
		//robotList = new List<Robot>(GameObject.FindObjectsOfType<Robot>());
		robotList = new List<Robot>();
		robotList.Add (currentRobot);
		robotIndex = 0;
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if(Input.GetButtonDown("NextRobot"))
		{
			robotIndex++;
			
			if (robotIndex >= robotList.Count)
				robotIndex = 0;
			
			SelectRobot (robotIndex);
		}
		
		if(Input.GetButtonDown("PreviousRobot"))
		{
			robotIndex--;
			
			if(robotIndex < 0)
				robotIndex = robotList.Count - 1;
			
			SelectRobot(robotIndex);
		}
		
		//Check each button in the RobotKeys list
		foreach(KeyValuePair<string, int> pair in RobotKeys)
		{
			if(Input.GetButtonDown(pair.Key))
			{
				SelectRobot(pair.Value);
			}
		}
		
		//A way to test having no robot selected
		if (Input.GetKeyDown (KeyCode.T))
		{
			if (currentRobot != null) 
				currentRobot.SetAsCurrent (false);
			currentRobot = null;
		}
		
		if(currentRobot != null)
			currentRobot.SetAsCurrent (true);
	}
	
	//For switching to an activated robot
	public bool SelectRobot(int index)
	{
		//make sure the index value is valid
		if(index < robotList.Count)
			robotIndex = index;
		else 
			return (false);
		
		//deselect the current robot if there is one
		if(currentRobot != null)
			currentRobot.SetAsCurrent (false);
		
		//select the new robot
		currentRobot = robotList[robotIndex];
		currentRobot.SetAsCurrent (true);
		
		return (true);
	}
	
	// Call when a robot is found to wake it up
	public void ActivateRobot(Robot robot)
	{
		//Make sure the robot isn't already on the list.
		if (robotList.Contains (robot))
			return;
		
		robotList.Add (robot);
		
		//TODO: popup message "Robot x is now activated! Press x to switch to it."
		
	}
}
