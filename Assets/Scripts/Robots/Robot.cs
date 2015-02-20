using UnityEngine;
using System.Collections;


public class Robot : Entity 
{
	public float movement;
	public float maxSpeed;
	public float jumpAmount;
	public float jumpNumber;
	protected float jumpsLeft;

	public float animationSpeed = 1;

	//public static Robot SelectedRobot;

	public RobotBattery battery;

	protected bool isActivated = false;
	protected bool isControlledCharacter = false;

	protected Transform groundCheckA1;
	protected Transform groundCheckA2;
	protected Transform groundCheckB1;
	protected Transform groundCheckB2;
	protected Animator animator;
	protected bool grounded = false;
	protected int batteryDrain;


	// Use this for initialization
	protected virtual void Start () 
	{
		base.Start ();

		animator = gameObject.GetComponent<Animator>();
		groundCheckA1 = transform.Find ("GroundCheckA1").transform;
		groundCheckA2 = transform.Find ("GroundCheckA2").transform;
		groundCheckB1 = transform.Find ("GroundCheckB1").transform;
		groundCheckB2 = transform.Find ("GroundCheckB2").transform;
		jumpsLeft = jumpNumber;
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
				jumpsLeft = jumpNumber;

			if (Input.GetButtonDown("Jump") && jumpsLeft-- > 1)
			{
				Jump (jumpAmount);
			}

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
		}

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
	}

	virtual public bool TestForGround()
	{
		// Test each bottom corner
		bool g1 = Physics2D.Linecast(groundCheckA1.position , groundCheckA2.position);
		bool g2 = Physics2D.Linecast(groundCheckB1.position , groundCheckB2.position);
		//If either corner is touching the ground, then the robot is standing
		return (g1 || g2) ? true : false;
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
