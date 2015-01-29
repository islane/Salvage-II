using UnityEngine;
using System.Collections;

public class Mine : MonoBehaviour {
	public int damage = 10;
	
	public AudioClip audio;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerEnter2D(Collider2D other) {
		
		if (other.gameObject.GetComponent<Robot>())
		{
			other.gameObject.GetComponent<Robot>().Damage(damage);
		}
		
		AudioSource.PlayClipAtPoint(audio, transform.position);
		Destroy (gameObject);
		//}
	}
}