using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	GameManager gameManager;

	public float xSmooth = 2.0f;
	public float ySmooth = 2.0f;

	float targetX = 0.0f;
	float targetY = 0.0f;

	// Use this for initialization
	void Start () {
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {
		if (gameManager.currentRobot != null)
		{
			GameObject currentCharacter = gameManager.currentRobot.gameObject;//GameManager.Instance.currentRobot.gameObject;
			targetX = Mathf.Lerp(transform.position.x, currentCharacter.transform.position.x, xSmooth * Time.deltaTime);
			targetY = Mathf.Lerp(transform.position.y, currentCharacter.transform.position.y, ySmooth * Time.deltaTime);
		}
		transform.position = new Vector3(targetX, targetY, transform.position.z);

	}
}
