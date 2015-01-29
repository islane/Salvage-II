using UnityEngine;
using System.Collections;

public class Robot : MonoBehaviour {
	
	public Rigidbody2D rigidBody2d;
	
	public DrainBar drainBar;
	
	public float movement = 10.0f;
	public float jump;
	//public int batteryHealth = 100;
	public bool activated = false;
	//[System.NonSerialized]
	public bool current = false;
	bool grounded = false;
	public GameObject groundCheck;
	public Global global;
	public int batteryDrainSpeed = 15;
	int batteryDrain;
	
	// Use this for initialization
//	public void SetBatteryHealth(int health){
//		batteryHealth = health;
//	}
	
	
	
	void Start () 
	{
		groundCheck = transform.FindChild ("GroundCheck").gameObject;
	}
	
	// Update is called once per frame
	void Update () 
	{
		GetInput ();
		
	}
	
	virtual public void GetInput()
	{
		grounded = Physics2D.Linecast(transform.position, groundCheck.transform.position);
		//Debug.Log (grounded);
		
		if(current)
		{
			if (Input.GetKey(KeyCode.A))
			{
				MoveRight (movement);
			}
			if (Input.GetKey(KeyCode.D))
			{
				MoveLeft (movement);
			}
			if (Input.GetKeyDown(KeyCode.Space))
			{
				Jump(jump);
			}
			
			if (drainBar.currentBattery == 0){
				Death();
			}
			
		}
	}
	
	virtual public void MoveRight(float move)
	{
		rigidBody2d.AddForce(new Vector2(-movement, 0.0f));
		Vector2 scale = transform.localScale;
		scale.x = -1;
		transform.localScale = scale;
		
		Drain();
		
	}
	
	virtual public void MoveLeft(float move)
	{
		rigidBody2d.AddForce(new Vector2(movement, 0.0f));
		
		Vector2 scale = transform.localScale;
		scale.x = 1;
		transform.localScale = scale;
		
		Drain();

	}
	virtual public void Jump(float jump)
	{
		rigidBody2d.AddForce(new Vector2(0.0f, jump));
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
