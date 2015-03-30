using UnityEngine;
using System.Collections;

public class BatteryModule : BaseModule {

	// Use this for initialization
	void Start () {
		base.Start ();
	}
	
	// Update is called once per frame
	void Update () {
		Drain (standingDrain);
	}

	public float max;
	public float level;
	public float standingDrain;
	public float movingDrain;
	public float jumpingDrain;
	public float specialDrain;
	
	public bool Drain(float amount)
	{
		level -= amount;

		if (level <= 0)
			return true;
		else
			return false;

		//Send message for deactivation?
	}
	
	public bool isDead()
	{
		if (level <= 0)
			return true;
		else
			return false;
		
	}

}
