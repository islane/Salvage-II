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
	int jumpNumber;
	int jumpTime;

	protected float animationSpeed = 1;

	//public static Robot SelectedRobot;

	public RobotBattery battery;

	protected bool isActivated = false;
	protected bool isControlledCharacter = false;

	protected Transform groundCheckA;
	protected Transform groundCheckB;
	bool grounded = false;
	int selfColliderCount;

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

			//Animation parameter
			animator.SetBool ("Jumping", Convert.ToBoolean (jumpNumber));


			Move (Input.GetAxis("Horizontal"));
			
			DrainBattery (battery.standingDrain);

		}
		
	}

	virtual public void Move(float axis)
	{
		//If player is inputting movement
		if(axis != 0)
		{
			//Add force 
			//if(grounded)
			movementVector += new Vector2(walkingAcceleration * axis, 0.0f);
			//else

			//Drain battery;
			DrainBattery (battery.movingDrain);

			animator.SetBool ("Moving", true);
		}
		else
			animator.SetBool ("Moving", false);

		//Clamp at max speed
		Vector2 v = rigidbody2D.velocity;
		v.x = Mathf.Clamp(v.x, -walkingSpeed, walkingSpeed);
		rigidbody2D.velocity = v;

		//Set animation parameter based on the velosity
		animator.SetFloat ("Speed", rigidbody2D.velocity.magnitude * animationSpeed); // / maxSpeed

		//rotate sprite to face direction
		Vector2 scale = transform.localScale;
		if (axis > 0)
			scale.x = 1;
		if (axis < 0)
			scale.x = -1;
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
