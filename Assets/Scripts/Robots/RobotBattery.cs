using UnityEngine;
using System.Collections;

[System.Serializable]
public class RobotBattery
{
	public float max;
	public float level;
	public float standingDrain;
	public float movingDrain;
	public float jumpingDrain;
	public float specialDrain;
	
	public bool Drain(float amount)
	{
		level -= amount;
		//Debug.Log (amount);

		if (level <= 0)
			return true;
		else
			return false;

	}

	public bool isDead()
	{
		if (level <= 0)
			return true;
		else
			return false;

	}
}
