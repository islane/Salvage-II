using UnityEngine;
using System;
using System.Collections;

public class JumpComponent : BaseComponent {

	
	[Header("These modify the robot's behaviour")]
	public float jumpForce;
	public float numberOfJumps;
	
	[TooltipAttribute("Adjust this based on each robot's sprite. The red line should be just below the floor collider")]
	public float groundCheckHeight;
	[TooltipAttribute("Adjust this to the width of the robot. The red line should be just wider than the floor collider")]
	public float groundCheckWidth;
	
	[Header("Only for debugging purposes")]
	public int jumpNumber;
	public int jumpTime;
	public bool isFalling;
	public bool grounded = false;
	public bool onPlatform = false;
	
	public Transform parentObjectTransform;


	// Use this for initialization
	protected override void Start () {
		base.Start ();

		parentObjectTransform = gameObject.transform.parent;
	}
	
	// Update is called once per frame
	protected override void Update () 
	{
		base.Update ();

		
		if(animator != null)
		{
			if(isEnabled)
			{
				animator.SetBool("Falling", isFalling);
				animator.SetBool ("Jumping", Convert.ToBoolean (jumpNumber));
			}
			else
			{
				animator.SetBool("Falling", false);
				animator.SetBool ("Jumping", false);
			}
		}
	}
	
	protected override void UpdateOnEnabled()
	{
		//Check to see if the robot is on the ground (or some other collider)
		grounded = TestForGround();
		
		//if the robot is on the ground, reset the jump counter
		if(grounded && Time.frameCount - jumpTime > 2)
		{
			jumpNumber = 0;
		}
		
		//Player hits the jump button
		if (Input.GetButtonDown("Jump"))
		{
			//If the player is on the ground, then no problem
			if (grounded)
			{
				Jump (jumpForce);
			}
			else //Double Jump
			{
				//If the player has jumped already, check if more jumps are available
				if(jumpNumber > 0 && jumpNumber < numberOfJumps)
				{
					Jump (jumpForce);
				}
				//If the player is in the air but didn't jump, then they fell, and they don't get the first jump
				if(jumpNumber == 0 && numberOfJumps > 1)
				{
					jumpNumber++;
					Jump (jumpForce);
				}
			}
		}

		//Falling
		isFalling = false;
		if (!grounded && jumpNumber == 0)
			isFalling = true;
		
		//If the robot gets stuck on a corner, then give it a little boost
		if (!grounded && rigidbody2D.velocity.magnitude == 0)
		{
			Debug.Log ("Stuck");
			Vector3 v = transform.position;
			v.y += 0.2f;
			transform.position = v;
		}

	}

	virtual public void Jump(float force)
	{
		rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, force);
		audio.Play ();
		battery.Drain (battery.jumpingDrain);
		jumpNumber++;
		jumpTime = Time.frameCount;
	}
	virtual public void Jump(Vector2 vector)
	{
		rigidbody2D.velocity = vector;
		audio.Play ();
		battery.Drain (battery.jumpingDrain);
		jumpNumber++;
		jumpTime = Time.frameCount;
	}
	
	virtual public bool TestForGround()
	{

		RaycastHit2D g = Physics2D.Linecast(new Vector3(transform.position.x - groundCheckWidth, transform.position.y - groundCheckHeight), 
		                                    new Vector3(transform.position.x + groundCheckWidth, transform.position.y - groundCheckHeight));
		
		transform.SetParent (parentObjectTransform);
		if(g.collider && g.collider.CompareTag ("Platform"))
		{
			transform.SetParent (g.collider.transform);
		}
		
		
		return(g.collider != null) ? true : false;
		
	}
	
	public void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawLine(new Vector3(transform.position.x - groundCheckWidth, transform.position.y - groundCheckHeight), 
		                new Vector3(transform.position.x + groundCheckWidth, transform.position.y - groundCheckHeight));
	}
}
