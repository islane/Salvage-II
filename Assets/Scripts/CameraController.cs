using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	GameObject player;
	LevelManager levelManager;

	public float xSmooth = 2.0f;
	public float ySmooth = 2.0f;

	float targetX = 0.0f;
	float targetY = 0.0f;

	// Use this for initialization
	void Start () {
		//TODO:This is only here for old levels
		GameObject go = GameObject.Find("LevelManager");
		if (go != null) levelManager = go.GetComponent<LevelManager>();

		player = FindObjectOfType<ControllerRobot>().gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		//TODO:This is only here for old levels
		if(levelManager != null) player = levelManager.currentRobot.gameObject;

		targetX = Mathf.Lerp(transform.position.x, player.transform.position.x, xSmooth * Time.deltaTime);
		targetY = Mathf.Lerp(transform.position.y, player.transform.position.y, ySmooth * Time.deltaTime);
		
		transform.position = new Vector3(targetX, targetY, transform.position.z);
	}
}
