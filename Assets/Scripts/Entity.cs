using UnityEngine;
using System.Collections;

public class Entity : MonoBehaviour {

	protected LevelManager levelManager;

	protected Vector2 movementVector;

	// Use this for initialization
	protected virtual void Start () {
		levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();

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
