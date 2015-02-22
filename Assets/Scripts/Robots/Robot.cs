using UnityEngine;
using System.Collections;


public class Robot : Entity 
{
	public float movement;
	public float maxSpeed;
	public float jumpForce;
	public float numberOfJumps;
	protected float holdJumpBoost = 0.02f;
	protected int firstFrameForDoubleJump = 15;
	protected int LastFrameForForceAdd = 20;
	protected float jumpsRemaining;
	protected bool isJumping;
	protected int jumpTime;

	public float animationSpeed = 1;

	//public static Robot SelectedRobot;

	public RobotBattery battery;

	protected bool isActivated = false;
	protected bool isControlledCharacter = false;

	protected Transform groundCheckA;
	protected Transform groundCheckB;
	protected Animator animator;
	protected bool grounded = false;
	protected int batteryDrain;


	// Use this for initialization
	protected virtual void Start () 
	{
		base.Start ();

		animator = gameObject.GetComponent<Animator>();
		groundCheckA = transform.Find ("GroundCheckA").transform;
		groundCheckB = transform.Find ("GroundCheckB").transform;
		jumpsRemaining = numberOfJumps;
	}
	
	// Update is called once per frame
	protected virtual void Update () 
	{
		base.Update ();


		if(isControlledCharacter)
		{
			//Check to see if the robot is on the ground (or some other collider)
			grounded = TestForGround();

			if(grounded)
			{
				jumpsRemaining = numberOfJumps;
				isJumping = false;
			}

			//Player hits the jump button
			if (Input.GetButtonDown("Jump"))
			{
				//If the player is on the ground, then no problem
				if (grounded)
					Jump (jumpForce);
				else
				{
					//If the player has jumped already, check if more jumps are available and enough time has passed
					if (isJumping && jumpsRemaining > 0 && Time.frameCount - jumpTime > firstFrameForDoubleJump)
						Jump (jumpForce);
					//If the player is in the air but didn't jump, then they fell, and they don't get the first jump
					if (!isJumping && jumpsRemaining-- > 1)
						Jump (jumpForce);
				}
				jumpsRemaining--;
			}

			//If the player continues to hold the jump button, then they jump a little higher
			if(Input.GetButton ("Jump"))
			{
				//If the player is still jumping and not too much time has passed, then add the extra force.
				if(isJumping && Time.frameCount - jumpTime < LastFrameForForceAdd)
				{
					rigidbody2D.AddForce(new Vector2(0.0f, jumpForce * holdJumpBoost));
				}
			
			}

			//Animation parameter
			animator.SetBool ("Jumping", isJumping);


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
			rigidbody2D.AddForce(new Vector2(movement * axis, 0.0f));

			//Drain battery;
			DrainBattery (battery.movingDrain);

			animator.SetBool ("Moving", true);
		}
		else
			animator.SetBool ("Moving", false);

		//Clamp at max speed
		Vector2 v = rigidbody2D.velocity;
		v.x = Mathf.Clamp(v.x, -maxSpeed, maxSpeed);
		rigidbody2D.velocity = v;

		//Set animation based on the velosity
		animator.SetFloat ("Speed", rigidbody2D.velocity.magnitude * animationSpeed); // / maxSpeed

		//rotate sprite to face direction
		Vector2 scale = transform.localScale;
		if (axis > 0)
			scale.x = 1;
		if (axis < 0)
			scale.x = -1;
		transform.localScale = scale;

	}

	virtual public void Jump(float amount)
	{
		rigidbody2D.AddForce(new Vector2(0.0f, amount));
		audio.Play ();
		DrainBattery (battery.jumpingDrain);
		isJumping = true;
		jumpTime = Time.frameCount;
	}

	virtual public bool TestForGround()
	{
		// Test each bottom corner
		RaycastHit2D[] g1 = Physics2D.LinecastAll(transform.position , groundCheckA.position);
		RaycastHit2D[] g2 = Physics2D.LinecastAll(transform.position , groundCheckB.position);
		//bool c1 = false;
		//bool c2 = false;
		//if(g1.Length > 1)
		//If either corner is touching the ground, then the robot is standing
		return (g1.Length > 1 || g2.Length > 1) ? true : false;
		
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

		gameManager.ActivateRobot (this);
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
