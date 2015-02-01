using UnityEngine;
using System.Collections;

public class BatteryStation : MonoBehaviour {
	public AudioClip chargingSound;

	void OnTriggerEnter2D(Collider2D other) 
	{
		if (other.gameObject.tag == "Player") 
		{
			Debug.Log ("Player is charging");

			//TODO: fix this
			//Robot robot = other.gameObject.GetComponent<Robot>();
			DrainBar drainbar = other.gameObject.GetComponent<DrainBar>();

			Debug.Log ("Player is charged");
			drainbar.currentBattery = drainbar.getMaxBattery();

			AudioSource.PlayClipAtPoint(chargingSound, transform.position); 
		}
	}
}
