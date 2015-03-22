using UnityEngine;
using System.Collections;
using System;

public interface ITarget
{
	int targetID{get;set;}
	void Trigger();
}

public interface ITrigger
{
	int triggerID{get;set;}
}

public class Robot : Entity, ITarget
{
	[Header("These modify the robot's behaviour")]
	public float walkingSpeed;
	public float walkingAcceleration;
	public float jumpForce;
	public float jumpBoost;
	public float numberOfJumps;
	public bool facingRight = true;
	public bool onPlatform = false;

	[TooltipAttribute("Adjust this based on each robot's sprite. The red line should be just below the floor collider")]
	public float groundCheckHeight;
	[TooltipAttribute("Adjust this to the width of the robot. The red line should be just wider than the floor collider")]
	public float groundCheckWidth;

	public RobotBattery battery;

	[Header("Only for debugging purposes")]
	public int jumpNumber;
	public int jumpTime;
	public bool isFalling;
	public bool grounded = false;

	public float animationSpeed = 1;


	public bool isActivated = false;
	public bool isControlledCharacter = false;


	protected Animator animator;
	protected int batteryDrain;
	protected Vector2 movementVector;

	public int targetID {get;set;}

	public Transform parentObjectTransform;
	//public static Robot SelectedRobot;

	// Use this for initialization
	protected override void Start () 
	{
		base.Start ();

		animator = gameObject.GetComponent<Animator>();

		if (!facingRight)
		{
			Turn ();
			facingRight = false;
		}

		parentObjectTransform = gameObject.transform.parent;
	}
	
	// Update is called once per frame
	protected override void Update () 
	{
		base.Update ();


		if(isControlledCharacter)
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

			//If the player continues to hold the jump button, then they jump a little higher
			if(Input.GetButton ("Jump"))
			{
				//Only apply force if the bot is in the upward part of the jump
				if (rigidbody2D.velocity.y > 0)
				{
					movementVector += new Vector2(0.0f, jumpBoost);

				}
			
			}

			//Falling
			isFalling = false;
			if (!grounded && jumpNumber == 0)
				isFalling = true;

			//Left and Right Movement
			float horizontalInput = Input.GetAxis ("Horizontal");

			if(horizontalInput != 0)
			{
				Move (horizontalInput);
				
				//rotate sprite to face direction
				if (horizontalInput > 0 && !facingRight || horizontalInput < 0 && facingRight)
					Turn ();
				
			}

			// Set animation parameters
			if(animator != null)
			{
				animator.SetBool("Falling", isFalling);
				animator.SetBool ("Jumping", Convert.ToBoolean (jumpNumber));
				animator.SetBool ("Moving", Convert.ToBoolean (horizontalInput));
				animator.SetFloat ("Speed", rigidbody2D.velocity.magnitude * animationSpeed);
			}

			DrainBattery (battery.standingDrain);

			//If the robot gets stuck on a corner, then give it a little boost
			if (!grounded && rigidbody2D.velocity.magnitude == 0)
			{
				Debug.Log ("Stuck");
				Vector3 v = transform.position;
				v.y += 0.2f;
				transform.position = v;
			}
		}

		if(animator != null)
			animator.SetBool("Active", isControlledCharacter);

		
	}

	//Forces should be applied in FixedUpdate, so we store the forces in a vector and add it here
	protected override void FixedUpdate()
	{
		rigidbody2D.AddForce (movementVector);
		
		movementVector = Vector2.zero;
	}

	virtual public void Move(float axis)
	{
		//MovementVector force will be added during the next FixedUpdate, to play nice with the physics engine
		if(axis > 0 && rigidbody2D.velocity.x < walkingSpeed ||
		   axis < 0 && rigidbody2D.velocity.x > -walkingSpeed)
			movementVector += new Vector2(walkingAcceleration * axis, 0.0f);

		//Limit the robot's speed
		/*if (rigidbody2D.velocity.x > walkingSpeed || rigidbody2D.velocity.x < -walkingSpeed)
		{
			Vector2 v = rigidbody2D.velocity;
			v.x = Mathf.Clamp(v.x, -walkingSpeed, walkingSpeed);
			rigidbody2D.velocity = v;
		}*/

		DrainBattery (battery.movingDrain);
	}

	virtual public void Turn()
	{
		facingRight = !facingRight;

		Vector2 scale = transform.localScale;
		scale.x = -scale.x;
		transform.localScale = scale;
	}

	virtual public void Jump(float force)
	{
		rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, force);
		audio.Play ();
		DrainBattery (battery.jumpingDrain);
		jumpNumber++;
		jumpTime = Time.frameCount;
	}
	virtual public void Jump(Vector2 vector)
	{
		rigidbody2D.velocity = vector;
		audio.Play ();
		DrainBattery (battery.jumpingDrain);
		jumpNumber++;
		jumpTime = Time.frameCount;
	}

	virtual public bool TestForGround()
	{
		// Test each bottom corner
		//RaycastHit2D[] g1 = Physics2D.LinecastAll(transform.position , transform.position + groundCheck1);
		//RaycastHit2D[] g2 = Physics2D.LinecastAll(transform.position , transform.position + groundCheck2);

		//If either corner is touching the ground, then the robot is standing
		//return (g1.Length > 0 || g2.Length > 0) ? true : false;

		RaycastHit2D g = Physics2D.Linecast(new Vector3(transform.position.x - groundCheckWidth, transform.position.y - groundCheckHeight), 
		                                      new Vector3(transform.position.x + groundCheckWidth, transform.position.y - groundCheckHeight));

		transform.SetParent (parentObjectTransform);
		if(g.collider && g.collider.CompareTag ("Platform"))
		{
			transform.SetParent (g.collider.transform);
		}


		return(g.collider != null) ? true : false;
		
	}

	virtual public void DrainBattery(float amount)
	{
		battery.Drain (amount);
		if(battery.isDead())
			Death ();

	}

	virtual public void Activate(bool activate)
	{
		isActivated = true;

		levelManager.ActivateRobot (this);
	}

	virtual public bool IsActivated()
	{
		return (isActivated);
	}

	virtual public void Death()
	{
		//TODO: this should play the battery drained animation, deactivate the robot, and switch to the next active robot.
		Application.LoadLevel ("GameOver");
	}

	virtual public void SetAsCurrent(bool current)
	{
		this.isControlledCharacter = current;
	}

	virtual public bool IsControlledCharacter()
	{
		return(isControlledCharacter);
	}

	public void Trigger()
	{
		Activate(true);
	}

	public void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawLine(new Vector3(transform.position.x - groundCheckWidth, transform.position.y - groundCheckHeight), 
		                new Vector3(transform.position.x + groundCheckWidth, transform.position.y - groundCheckHeight));
	}
}

