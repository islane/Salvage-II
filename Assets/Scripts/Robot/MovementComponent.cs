using UnityEngine;
using System;
using System.Collections;

public class MovementComponent : BaseComponent {

	[Header("These modify the robot's behaviour")]
	public float walkingSpeed;
	public float walkingAcceleration;
	public bool facingRight = true;
	public float animationSpeed = 1;
	public float horizontalInput;


	// Use this for initialization
	protected override void Start () {
		base.Start();

		if (!facingRight)
		{
			Turn ();
			facingRight = false;
		}
		
	}
	
	// Update is called once per frame
	protected override void Update () 
	{
		base.Update ();

		
		if(animator != null)
		{
			if(isEnabled)
				animator.SetBool ("Moving", Convert.ToBoolean (horizontalInput));
			else
				animator.SetBool ("Moving", false);

			animator.SetFloat ("Speed", rigidbody2D.velocity.magnitude * animationSpeed);
		}
	}
	
	protected override void UpdateOnEnabled()
	{
		
		//Left and Right Movement
		horizontalInput = Input.GetAxis ("Horizontal");
		
		if(horizontalInput != 0)
		{
			Move (horizontalInput);
			
			//Flip sprite to face direction
			//if (horizontalInput > 0 && !facingRight || horizontalInput < 0 && facingRight)
			//	Turn ();

			if (horizontalInput > 0)
				TurnRight ();
			else
				TurnLeft ();
		}

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
		
		battery.Drain (battery.movingDrain);
	}
	
	virtual public void Turn()
	{
		facingRight = !facingRight;
		
		Vector2 scale = transform.localScale;
		scale.x = -scale.x;
		transform.localScale = scale;
	}

	virtual public void TurnRight()
	{
		facingRight = true;

		Vector2 scale = transform.localScale;
		scale.x = 1.0f;
		transform.localScale = scale;
	}
	virtual public void TurnLeft()
	{
		facingRight = false;
		
		Vector2 scale = transform.localScale;
		scale.x = -1.0f;
		transform.localScale = scale;
	}
}
