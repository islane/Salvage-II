using UnityEngine;
using System.Collections;

public class DoorSwitch : MonoBehaviour {

	public Door door;

	void OnTriggerEnter2D()
	{
		//other.rigidbody2D.AddForce ();
		door.Open();
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
