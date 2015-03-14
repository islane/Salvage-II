using UnityEngine;
using System.Collections;

public class FlyingRobot : Robot {
	public float flyHeight;

	// Use this for initialization
	protected override void Start () 
	{
		base.Start ();
	}
	
	// Update is called once per frame
	protected override void Update () 
	{
		base.Update ();
		if (Input.GetButton("Jump"))
		{
			Vector2 height = new Vector2(0, flyHeight);
		    rigidbody2D.AddForce(height);
		}
	}
}
