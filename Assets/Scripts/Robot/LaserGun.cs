using UnityEngine;
using System.Collections;

public class LaserGun : MonoBehaviour {
	public Transform gun;
	public Transform gunDirection;
	public float shootSight;
	protected RaycastHit2D hit;


	// Use this for initialization
	void Start () 
	{   //Finds the Gun object
		gun = transform.FindChild ("Gun");
		if (gun == null) {
				Debug.LogError ("No gun!!!");
		}
		//Finds the Gun Direction Object
		gunDirection = transform.FindChild ("Gun Direction");
		if (gunDirection == null) {
				Debug.LogError ("No gunDirection");
		}
	}
	// Update is called once per frame
	void Update () 
	{   //Executes shoot method if "Fire 1" is pressed
		if (Input.GetButtonDown ("Fire1")) 
		{	
            Debug.Log ("Shot fired");
			shoot();
		} 
		else 
		{
		}

	}

	void shoot()
	{ 
	  //Creates the origin of the line 
	  Vector2 gunPointPosition = new Vector2 (gun.position.x, gun.position.y);
		//Finds the parent to get the faceRight variable
		Robot r = GetComponentInParent<Robot> ();
		//Creates the line depending upon the direction of the robot
	  if (r.facingRight == true) 
	  {
	    //Creates the direction of the line by using the position of the gun Object
		Vector2 gunPath = new Vector2 (gunDirection.position.x, gunDirection.position.y);
	    //Creates the line 
		RaycastHit2D hit = Physics2D.Raycast (gunPointPosition, gunPath, shootSight);
		//Debug of the ray created
		Debug.DrawLine (gunPointPosition, new Vector3(gunPath.x * shootSight, gunPath.y, 0.0f));
	  }
	  else
	  { 
		Vector2 gunPath = new Vector2(-(gunDirection.position.x), gunDirection.position.y);
		RaycastHit2D hit = Physics2D.Raycast (gunPointPosition, gunPath, shootSight);
		Debug.DrawLine (gunPointPosition, new Vector3(gunPath.x * shootSight, gunPath.y, 0.0f));
	  }


	}
}


