using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{   
	public float speed = 10;
	public float secondsTilDestory = 10;
	private float startTime;

	void Start()
	{
		startTime = Time.time;
	}

	void Update()
	{
		Vector2 v = new Vector2(transform.position.x + speed, transform.position.y);

		transform.position = v;
		if (Time.time - startTime >= secondsTilDestory) 
		{
			Destroy(this.gameObject);
		} 
	}

	void OnCollisionEnter(Collision collider)
	{
		Destroy (gameObject);
	}
}