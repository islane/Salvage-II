using UnityEngine;
using System.Collections;


public class Robot : Entity 
{

	public DrainBar drainBar;
	
	public float movement;
	public float maxSpeed;
	public float jump;

	public bool activated = false;
	public bool current = false;
	public int batteryDrainSpeed = 15;

	public float animationSpeed = 1;

	protected Transform groundCheck;
	protected Animator animator;
	protected bool grounded = false;
	protected int batteryDrain;

	public RobotBattery battery;

	// Use this for initialization
	protected virtual void Start () 
	{
		animator = gameObject.GetComponent<Animator>();
		groundCheck = transform.FindChild ("GroundCheck").gameObject.transform;
		battery = new RobotBattery();
	}
	
	// Update is called once per frame
	protected virtual void Update () 
	{
		if(current)
		{
			grounded = Physics2D.Linecast(transform.position , groundCheck.position);//, 1 << LayerMask.NameToLayer("Floor"));

			Move ();
			
			if (Input.GetButtonDown("Jump") && grounded)
			{
				Jump(jump);
			}
			
			/*if (drainBar.currentBattery == 0){
				Death();
			}*/
			
		}
		
	}
	
	virtual public void Move()
	{
		float axis =  Input.GetAxis("Horizontal");

		//Add force
		if(axis != 0)
		{
			rigidbody2D.AddForce(new Vector2(movement * axis, 0.0f));
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

		//Drain battery;
		Drain();
		//if (battery.Drain (battery.movingDrain))
		//	Death ();
		//drainBar.CurrentBattery = (int)battery.level;
		
	}

	virtual public void Jump(float jump)
	{
		rigidbody2D.AddForce(new Vector2(0.0f, jump));
		audio.Play ();

		Drain();
		//if (battery.Drain (battery.jumpingDrain))
		//	Death ();
		//drainBar.CurrentBattery = (int)battery.level;
	}
	
	virtual public void Drain()
	{
		//battery.Drain (amount);
		//drainBar.CurrentBattery = (int)battery.level;

		batteryDrain++;
		if(batteryDrain > batteryDrainSpeed)
		{
			drainBar.CurrentBattery--;
			batteryDrain = 0;

			if(drainBar.CurrentBattery <= 0)
				Death ();
		}
	}
	
	virtual public void Damage(int dmg)
	{
		//if (battery.Drain (dmg))
		//	Death();
		//drainBar.CurrentBattery = (int)battery.level;

		drainBar.CurrentBattery -= dmg;

		if(drainBar.CurrentBattery <= 0)
			Death ();

	}
	
	virtual public void Death()
	{
		Application.LoadLevel ("GameOver");
	}
}
