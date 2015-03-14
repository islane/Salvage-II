using UnityEngine;
using System.Collections;

public class WallSound : MonoBehaviour {

	public AudioClip bumpSound;

	void OnCollisionEnter2D(Collision2D otherObj)
	{
		
		if (otherObj.gameObject.tag == "Player")//(otherObj.gameObject.GetComponent<Robot>())//
		{
			GetComponent<AudioSource>().PlayOneShot (bumpSound);
		}
	}
}
