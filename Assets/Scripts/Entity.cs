using UnityEngine;
using System.Collections;

public class Entity : MonoBehaviour {

	protected LevelManager levelManager;

	// Use this for initialization
	protected virtual void Start () {
		levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();

	}
	
	// Update is called once per frame
	protected virtual void Update () {
	
	}
}
