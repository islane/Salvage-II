using UnityEngine;
using System.Collections;

public class water : MonoBehaviour {

	public int damage = 3;

	void OnTriggerStay2D(Collider2D other) {
		if (other.gameObject.GetComponent<Robot>())
		{
			other.gameObject.GetComponent<Robot>().Damage(damage);
		}

	}
}
