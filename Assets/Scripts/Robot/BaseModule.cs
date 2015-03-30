﻿using UnityEngine;
using System.Collections;


public class BaseModule : MonoBehaviour, IModule {

	public int priority {get;set;}
	protected Vector2 movementVector;
	protected Rigidbody2D rigidbody2D;
	protected BatteryModule battery; 
	protected Animator animator;
	protected new AudioSource audio;
	//protected SpringJoint2D joint;

	// Use this for initialization
	protected virtual void Start () {
		rigidbody2D = GetComponent<Rigidbody2D>();
		battery = GetComponent<BatteryModule>();
		animator = gameObject.GetComponent<Animator>();
		audio = GetComponent<AudioSource>();
		
	}
	
	// Update is called once per frame
	protected virtual void Update () {
	
	}

	
	void FixedUpdate()
	{
		ApplyForces ();
	}
	
	
	//Forces should be applied in FixedUpdate, so we store the forces in a vector and add it here
	public virtual void ApplyForces()
	{
		rigidbody2D.AddForce (movementVector);
		
		movementVector = Vector2.zero;
	}

	public void Enable()
	{
		enabled = true;
	}
	public void Enable(bool enable)
	{
		enabled = enable;
	}

	public void Disable()
	{
		enabled = false;
	}
}
