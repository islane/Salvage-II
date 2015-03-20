using UnityEngine;
using System.Collections;
using System;


public class Robot : Entity 
{
	public float walkingSpeed;
	public float walkingAcceleration;
	public float jumpForce;
	public float jumpBoost;
	public float numberOfJumps;
	public int jumpNumber;
	public int jumpTime;
	public bool isFalling;
	public bool facingRight;

	public float animationSpeed = 1;

	//public static Robot SelectedRobot;

	public RobotBattery battery;

	public bool isActivated = false;
	public bool isControlledCharacter = false;

	protected Transform groundCheckA;
	protected Transform groundCheckB;
	public bool grounded = false;
	public int selfColliderCount;

	protected Animator animator;
	protected int batteryDrain;



	// Use this for initialization
	protected override void Start () 
	{
		base.Start ();

		animator = gameObject.GetComponent<Animator>();

		groundCheckA = transform.Find ("GroundCheckA").transform;
		groundCheckB = transform.Find ("GroundCheckB").transform;
		selfColliderCount = Physics2D.LinecastAll(transform.position , groundCheckA.position).Length;
	}
	
	// Update is called once per frame
	protected override void Update () 
	{
		base.Update ();


		if(isControlledCharacter)
		{
			//Check to see if the robot is on the ground (or some other collider)
			grounded = TestForGround();

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
			float axis = Input.GetAxis("Horizontal");

			if(axis != 0)
			{
				Move (axis);

				//rotate sprite to face direction
				if (axis < 0 && !facingRight || axis > 0 && facingRight)
					Turn ();
				
			}
			
			// Set animation parameters
			if(animator != null)
			{
				animator.SetBool("Falling", isFalling);
				animator.SetBool ("Jumping", Convert.ToBoolean (jumpNumber));
				animator.SetBool ("Moving", Convert.ToBoolean (axis));
				animator.SetFloat ("Speed", rigidbody2D.velocity.magnitude * animationSpeed);
			}

			DrainBattery (battery.standingDrain);

		}

		if(animator != null)
			animator.SetBool("Active", isControlledCharacter);

		
	}

	virtual public void Move(float axis)
	{
		if (rigidbody2D.velocity.x < walkingSpeed && rigidbody2D.velocity.x > -walkingSpeed)
		{
			movementVector += new Vector2(walkingAcceleration * axis, 0.0f);
		}
		else
		{
			Vector2 v = rigidbody2D.velocity;
			v.x = Mathf.Clamp(v.x, -walkingSpeed, walkingSpeed);
			rigidbody2D.velocity = v;

		}
		
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
		rigidbody2D.velocity = new Vector2(0.0f, force);
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
		RaycastHit2D[] g1 = Physics2D.LinecastAll(transform.position , groundCheckA.position);
		RaycastHit2D[] g2 = Physics2D.LinecastAll(transform.position , groundCheckB.position);

		//If either corner is touching the ground, then the robot is standing
		return (g1.Length > selfColliderCount || g2.Length > selfColliderCount) ? true : false;
		
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

}
