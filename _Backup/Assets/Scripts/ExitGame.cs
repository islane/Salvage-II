using UnityEngine;
using System.Collections;

public class ExitGame : MonoBehaviour {
	public AudioClip exitSound;
	public string levelToLoad = "WinScene";
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{

	}
	
	void OnTriggerEnter2D(Collider2D other) {
		
		//if (other.gameObject.GetComponent<Robot>())	{
		Robot robot = other.gameObject.GetComponent<Robot>();
		
		//Trigger if the object is the player controlled robot
		if (robot != null && robot.IsControlledCharacter())
		{
			AudioSource.PlayClipAtPoint(exitSound, transform.position);
			Debug.Log("Player Exit");
			Application.LoadLevel(levelToLoad);
		}
	}
}