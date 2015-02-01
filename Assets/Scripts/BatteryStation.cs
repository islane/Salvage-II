using UnityEngine;
using System.Collections;

public class BatteryStation : MonoBehaviour {
	public AudioClip audio;
	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Player") {
			Debug.Log ("Player is charging");
			//Robot robot = other.gameObject.GetComponent<Robot>();
			DrainBar drainbar = other.gameObject.GetComponent<DrainBar>();
			Debug.Log ("Player is charged");
			drainbar.currentBattery=drainbar.getMaxBattery();
			AudioSource.PlayClipAtPoint(audio, transform.position); 
		}
	}
}
