using UnityEngine;
using System.Collections;


public class Robot : Entity 
{
	public float movement;
	public float maxSpeed;
	public float jumpAmount;
	public bool activated = false;

	public float animationSpeed = 1;

	public RobotBattery battery;

	protected bool isControlledCharacter = false;

	protected Transform groundCheck;
	protected Animator animator;
	protected bool grounded = false;
	protected int batteryDrain;


	// Use this for initialization
	protected virtual void Start () 
	{
		animator = gameObject.GetComponent<Animator>();
		groundCheck = transform.FindChild ("GroundCheck").gameObject.transform;
	}
	
	// Update is called once per frame
	protected virtual void Update () 
	{

		if(isControlledCharacter)
		{
			grounded = Physics2D.Linecast(transform.position , groundCheck.position);//, 1 << LayerMask.NameToLayer("Floor"));

			Move ();
			
			if (Input.GetButtonDown("Jump") && grounded)
			{
				Jump(jumpAmount);
			}

			DrainBattery (battery.standingDrain);

		}
		
	}
	
	virtual public void Move()
	{
		float axis =  Input.GetAxis("Horizontal");

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
	
	virtual public void DrainBattery(float amount)
	{
		battery.Drain (amount);
		if(battery.isDead())
			Death ();

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
