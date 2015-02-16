using UnityEngine;
using System.Collections;

public class Mine : MonoBehaviour {

	public int damage = 10;
	
	public AudioClip sound;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerEnter2D(Collider2D other) {
		
		if (other.gameObject.GetComponent<Robot>())
		{
			other.gameObject.GetComponent<Robot>().DrainBattery(damage);
		}
		
		AudioSource.PlayClipAtPoint(sound, transform.position);
		Destroy (gameObject);
		//}
	}
}