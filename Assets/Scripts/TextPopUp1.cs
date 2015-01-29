using UnityEngine;
using System.Collections;

public class TextPopUp1 : MonoBehaviour {
	public string stringToEdit = "A winner is you!";
	Door door = new Door();
	// Use this for initialization
	void Start () {
	
	}
	// Update is called once per frame
	void Update () {
		if (door.isWinning) {
			stringToEdit = GUI.TextField(new Rect(10, 10, 200, 20), stringToEdit, 25);
		}
	}
}
