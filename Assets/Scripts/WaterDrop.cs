using UnityEngine;
using System.Collections;

public class WaterDrop : MonoBehaviour {

	public int damage = 10;

	public AudioClip dripSound;
	public AudioClip electrocuteSound;

	// Use this for initialization
	void Start () {
		audio.PlayOneShot(dripSound);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other) {

		Robot robot = other.gameObject.GetComponent<Robot>();

		if (robot != null)
		{
			robot.Damage(damage);
			AudioSource.PlayClipAtPoint(electrocuteSound, transform.position);
		}

		//AudioSource.PlayClipAtPoint(dripSound, transform.position);

		Destroy (gameObject);
	}
}
