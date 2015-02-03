using UnityEngine;
using System.Collections;

[System.Serializable]
public class RobotBattery
{
	public float max = 16f;
	public float level = 16f;
	public float standingDrain = 0.0f;
	public float movingDrain = 0.2f;
	public float jumpingDrain = 0.4f;
	public float specialDrain = 0.5f;
	
	public bool Drain(float amount)
	{
		level -= amount;
		
		if (level <= 0)
			return true;
		else
			return false;
		
	}
}
