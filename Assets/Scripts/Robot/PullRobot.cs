using UnityEngine;
using System.Collections;


public class PullRobot : Robot 
{
	public float max_dis;
	public GameObject pullObject;
	public bool carryingObject = false;

	// Use this for initialization
	void Start () 
	{
		base.Start ();
		pullObject = GameObject.FindGameObjectWithTag ("PullObject");
	}
	
	// Update is called once per frame
	void Update () 
	{
		base.Update();


	}
	
	override public void Turn()
	{   
		//Lets the robot turn if its not carrying anything or its stops turn 
		if (carryingObject == false) 
		{
			base.Turn();
		}
	}

	public void Detach()
	{   //Detach
		pullObject.transform.parent = null;
		carryingObject = false;
	}

	void OnTriggerEnter(Collider other) 
	{
		Debug.Log("You can pull");
		if(Input.GetButtonDown("Special") & carryingObject == false)
		{	
			//Stops robot from turning and attachs the pull object to the robot as a child
			carryingObject = true;
			Turn ();
			pullObject.transform.SetParent(transform);
			pullObject.transform.localScale = new Vector3(1,1,0);
		}
		
		else if(Input.GetButtonDown("Special") & carryingObject == true)
		{   
			//Calls detach method
			Detach();
		}
	}
}