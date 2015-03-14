using UnityEngine;
using System.Collections;

public class Crate : MonoBehaviour {

	// Use this for initialization
	void Start () {
		gameObject.AddComponent<Rigidbody2D>();
		gameObject.AddComponent<BoxCollider2D>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
