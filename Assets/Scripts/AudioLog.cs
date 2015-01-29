using UnityEngine;
using System.Collections;

public class AudioLog : MonoBehaviour {
	public AudioClip audioLog;
		void OnTriggerEnter2D(Collider2D other) {
		Debug.Log ("Play audio");
		AudioSource.PlayClipAtPoint(audioLog, transform.position); 
		Destroy(gameObject);
	}
}
