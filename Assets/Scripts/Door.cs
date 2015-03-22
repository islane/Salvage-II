using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

	public bool isWinning = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Open(){
		gameObject.SetActive(false);
	}
	
	public void Close(){
		gameObject.SetActive(true);
	}
	
	void OnTriggerEnter2D(Collider2D other) {
		
		//if (other.gameObject.GetComponent<Robot>())	{
		Robot robot = other.gameObject.GetComponent<Robot>();
		
		//Trigger if the object is the player controlled robot
		if (robot != null && robot.IsControlledCharacter())
		{
			//AudioSource.PlayClipAtPoint(exitSound, transform.position);
			Debug.Log("Player Exit");
			Application.LoadLevel("WinScene");
		}
	}
}
