using UnityEngine;
using System.Collections;

public class Robot : Entity {
	

	public DrainBar drainBar;
	
	public float movement;
	public float maxSpeed;
	public float jump;

	public bool activated = false;
	public bool current = false;
	public int batteryDrainSpeed = 15;

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
		if(current)
		{
			grounded = Physics2D.Linecast(transform.position , groundCheck.position , 1 << LayerMask.NameToLayer("Floor"));
			
			Move ();
			
			if (Input.GetButtonDown("Jump") && grounded)
			{
				Jump(jump);
			}
			
			if (drainBar.currentBattery == 0){
				Death();
			}
			
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
		animator.SetFloat ("Speed", rigidbody2D.velocity.magnitude / maxSpeed);

		//rotate sprite to face direction
		Vector2 scale = transform.localScale;
		if (axis > 0)
			scale.x = 1;
		if (axis < 0)
			scale.x = -1;
		transform.localScale = scale;

		//Drain battery;
		Drain();
		
	}

	virtual public void Jump(float jump)
	{
		rigidbody2D.AddForce(new Vector2(0.0f, jump));
		audio.Play ();

		Drain();
	}
	
	virtual public void Drain()
	{
		batteryDrain++;
		if(batteryDrain > batteryDrainSpeed)
		{
			drainBar.CurrentBattery -=1;
			batteryDrain = 0;
		}
	}
	
	virtual public void Damage(int dmg)
	{
		drainBar.CurrentBattery -= dmg;
	}
	
	virtual public void Death()
	{
		Application.LoadLevel ("GameOver");
	}
}
