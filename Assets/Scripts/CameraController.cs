using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	//public float speed = 5.0f;
	GameObject currentCharacter;

	float xSmooth = 2.0f;
	float ySmooth = 2.0f;
	Global global;

	// Use this for initialization
	void Start () {
		global = GameObject.Find ("Global").GetComponent<Global>();
	}
	
	// Update is called once per frame
	void Update () {
		currentCharacter = global.currentRobot.gameObject;
		float targetX = Mathf.Lerp(transform.position.x, currentCharacter.transform.position.x, xSmooth * Time.deltaTime);
		float targetY = Mathf.Lerp(transform.position.y, currentCharacter.transform.position.y, ySmooth * Time.deltaTime);
		transform.position = new Vector3(targetX, targetY, transform.position.z);
	}
}
