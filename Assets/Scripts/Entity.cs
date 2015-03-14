using UnityEngine;
using System.Collections;

public class Entity : MonoBehaviour {

	protected LevelManager levelManager;

	protected Vector2 movementVector;

	protected new Rigidbody2D rigidbody2D;

	protected new AudioSource audio;

	// Use this for initialization
	protected virtual void Start () {
		levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();

		rigidbody2D = GetComponent<Rigidbody2D>();

		audio = GetComponent<AudioSource>();
	}
	
	void FixedUpdate()
	{
		rigidbody2D.AddForce (movementVector);
		movementVector = Vector2.zero;
	}

	// Update is called once per frame
	protected virtual void Update () {
	
	}
}
