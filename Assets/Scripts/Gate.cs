using UnityEngine;
using System.Collections;

public class Gate : MonoBehaviour {

	public bool isOpen = false;

	void Start(){
		if (isOpen)
			Open();

	}

	public void Open()
	{
		Debug.Log ("Door Opened");
		Activate (false);
	}

	public void Close()
	{
		Debug.Log ("Door Closed");
		Activate (true);
	}

	public void Toggle()
	{
		Debug.Log ("Toggle Door");
		Activate (isOpen);
	}

	public void Activate(bool activate)
	{
		renderer.enabled = activate;
		collider2D.enabled = activate;
		isOpen = !activate;
	}

	// Update is called once per frame
	void Update () {
	
	}

}
