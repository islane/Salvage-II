using UnityEngine;
using System.Collections;

public class PrefabSpawner : MonoBehaviour {

	public GameObject prefabToSpawn;
	public float intervalInSeconds;
	public bool active = true;

	void Start()
	{
		StartCoroutine ("Spawn");
	}

	void Update() 
	{
	}

	IEnumerator Spawn()
	{
		while(active)
		{
			GameObject p = Instantiate(Resources.Load(prefabToSpawn.name, typeof(GameObject))) as GameObject;
			p.transform.position = transform.position;

			yield return new WaitForSeconds(intervalInSeconds);
		}
	}
}