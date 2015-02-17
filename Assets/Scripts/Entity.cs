using UnityEngine;
using System.Collections;

public class Entity : MonoBehaviour {

	protected GameManager gameManager;

	// Use this for initialization
	virtual public void Start () {
		GameObject go = GameObject.FindGameObjectWithTag ("GameManager");
		gameManager = go.GetComponent<GameManager>();

	}
	
	// Update is called once per frame
	virtual public void Update () {
	
	}
}
