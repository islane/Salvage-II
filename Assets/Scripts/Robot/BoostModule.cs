using UnityEngine;
using System.Collections;

public class BoostModule : BaseModule {

	public float jumpBoost;

	// Use this for initialization
	protected override void Start () {
		base.Start();
	}
	
	// Update is called once per frame
	protected override void Update () {
		
		//If the player continues to hold the jump button, then they jump a little higher
		if(Input.GetButton ("Jump"))
		{
			//Only apply force if the bot is in the upward part of the jump
			if (rigidbody2D.velocity.y > 0)
			{
				movementVector += new Vector2(0.0f, jumpBoost);
				
			}
			
		}

	}
}
