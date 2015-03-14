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
			BatteryBar drainbar = other.gameObject.GetComponent<BatteryBar>();

			Debug.Log ("Player is charged");
			drainbar.CurrentBattery = drainbar.GetMaxBattery();

			AudioSource.PlayClipAtPoint(chargingSound, transform.position); 
		}
	}
}
