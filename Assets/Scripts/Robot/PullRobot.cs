using UnityEngine;
using System.Collections;


public class PullRobot : Robot {
	public float max_dis;
	private GameObject pullObject;

	// Use this for initialization
	void Start () {
		base.Start();
		pullObject = GameObject.FindGameObjectWithTag ("PullObject");
	}
	
	// Update is called once per frame
	void Update () {
		base.Update();

		float distance = Vector2.Distance (transform.position, pullObject.transform.position);// Calculate distance between pull object and player object

		if (distance <= max_dis)// If the player is close enough then the robot can pull the object
		{
			Debug.Log("You are close enough to pull");
			if (Input.GetButtonDown ("Special"))
			{
				transform.parent = pullObject.transform;
			}
		} 
		
		else 
		{
			
		}
	}
}