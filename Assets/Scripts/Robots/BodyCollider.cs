using UnityEngine;
using System.Collections;

public class BodyCollider : MonoBehaviour {

	//GameObject go;

	// Use this for initialization
	void Start () {
		//go = 
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionStay2D(Collision2D other)
	{
		Vector2 v = new Vector2(0.0f, gameObject.transform.parent.rigidbody2D.velocity.y);
		gameObject.transform.parent.rigidbody2D.velocity = v;
	}
}
