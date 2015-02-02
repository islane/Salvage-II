using UnityEngine;
using System.Collections;

public class ExitSound : MonoBehaviour {

	public AudioClip exitSound;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other) {
		
		if (other.gameObject.GetComponent<Robot>())	{
			AudioSource.PlayClipAtPoint(exitSound, transform.position);
			Debug.Log("Player Exit");
		}
}
}
