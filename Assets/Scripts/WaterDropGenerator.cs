using UnityEngine;
using System.Collections;

public class WaterDropGenerator : MonoBehaviour {

	public GameObject projectile;
	public int delay;
	int timer;

	void Start()
	{
		//projectile = GameObject.Find ("drop1");
		timer = 0;
	}

	void Update() 
	{
		timer++;
		if (timer > delay) 
		{
			GameObject clone;
			clone = (GameObject)Instantiate (projectile, transform.position, transform.rotation);
			clone.GetComponent<Rigidbody2D> ().velocity = transform.TransformDirection (Vector3.down * 1);
			timer = 0;
		}
	}
}