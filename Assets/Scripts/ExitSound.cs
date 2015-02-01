using UnityEngine;
using System.Collections;

public class ExitSound : MonoBehaviour {

	public AudioClip sound;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter2D(Collider2D other) {
		
		if (other.gameObject.GetComponent<Robot>())	{
			AudioSource.PlayClipAtPoint(sound, transform.position);
			Debug.Log("Player Exit");
		}
}
}
