using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;



public class GateSwitch : MonoBehaviour {

	public enum GateSwitchBehaviour
	{
		OPEN,
		CLOSE,
		TOGGLE
	}

	public List<Gate> gatesToTrigger;

	public GateSwitchBehaviour behavior = GateSwitchBehaviour.OPEN;

	//To prevent the trigger from being activated multiple times by multiple colliders on the same object
	bool isTriggered = false;


	 Action<Gate> BehaviorMethod;

	// Use this for initialization
	void Start () {

		if (behavior == GateSwitchBehaviour.OPEN)
			BehaviorMethod = Open;
		else if (behavior == GateSwitchBehaviour.CLOSE)
			BehaviorMethod = Close;
		else if (behavior == GateSwitchBehaviour.TOGGLE)
			BehaviorMethod = Toggle;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void Open(Gate gate)
	{
		gate.Open ();
	}

	void Close(Gate gate)
	{
		gate.Close ();
	}

	void Toggle(Gate gate)
	{
		gate.Toggle ();
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (isTriggered)
			return;
		isTriggered = true;

		Robot robot = other.gameObject.GetComponent<Robot>();

		//Trigger if the object is the player controlled robot
		if (robot != null && robot.IsControlledCharacter());
		{
			foreach (Gate g in gatesToTrigger)
			{
				BehaviorMethod(g);
			}
		}
		
	}

	void OnTriggerExit2D(Collider2D other)
	{
		isTriggered = false;

	}
}
