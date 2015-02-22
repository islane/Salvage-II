using UnityEngine;
using System.Collections;

public class Signal: Robot {
	Transform lead;
	public int max_distance = 3;
	public bool bot_active = false;

	// Use this for initialization
	void Start () 
	{
		base.Start ();
		lead = gameManager.currentRobot.transform;//Finds player tag

	}
	
	// Update is called once per frame
	void Update () 
	{

		float distance = Vector3.Distance (transform.position, lead.position);// Calculate distance between Signal object and player object

		if (distance <= max_distance)// If the player is close enough then the bot is active and 
		{
			Debug.Log("The signal is on");
			bot_active = true;
			if (Input.GetButtonDown ("Special"))
			{
				Jump(jumpForce);
			}
		} 

		else 
		{
				
		}
	
	}
}
