using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{
	GameObject newBullet;

	void fire () 
	{
		newBullet = Instantiate(Resources.Load("Bullet", typeof(GameObject))) as GameObject;
		newBullet.transform.position = transform.position;
		Bullet b = newBullet.GetComponent<Bullet> ();
		Robot r = GetComponentInParent<Robot> ();
		if (!r.facingRight) 
		{
			b.speed = -b.speed;
		}

	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetButtonDown("Fire1"))
		{
		    fire ();
		}
		
	}
	 

}
