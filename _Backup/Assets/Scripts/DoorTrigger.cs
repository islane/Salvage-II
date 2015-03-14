using UnityEngine;
using System.Collections;

public class DoorTrigger : MonoBehaviour {

	public GameObject DoorToOpen;

	// Use this for initialization
	void Start () {
	
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
			DoorToOpen.SetActive (false);
			//global.currentRobot = ; //TODO: Do we want to switch to the new robot automaticaly?
		}
		
	}
}
