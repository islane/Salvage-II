using UnityEngine;
using System.Collections;

public class Water : MonoBehaviour {

	public int damage = 3;
	public AudioClip damageSound;

	void OnTriggerStay2D(Collider2D other) {

		Robot robot = other.gameObject.GetComponent<Robot>();

		if (robot != null)
		{
			robot.DrainBattery(damage);
			GetComponent<AudioSource>().PlayOneShot(damageSound);
		}

	}
}
