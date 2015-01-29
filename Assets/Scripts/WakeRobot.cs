using UnityEngine;
using System.Collections;

public class WakeRobot : MonoBehaviour {

	public Robot robotToWake;
	Global global;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		robotToWake.activated = true;
		//global.currentRobot = ;
		
	}
}
