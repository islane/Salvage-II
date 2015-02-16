using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BatteryBar : MonoBehaviour {

	// float currentGraphScale = 1.0f;//draining speed vs bar drain speed
	
	public RectTransform batteryTransform;
	public Image visualLevel;
	public CanvasGroup canvasGroup;


	private float cachedY;
	private float minXValue;
	private float maxXValue;
	float maxBattery; //scales depending on the robot in use

	GameManager gameManager;

	private float _currentBattery;
	public float CurrentBattery //access this to edit battery level
	{
		get { return _currentBattery; }
		set { 
			_currentBattery = value;
			HandleBattery();
		}
	}

	public float GetMaxBattery(){
		return maxBattery;
	}


	// public Text batteryText; //for use with text readout of battery level

	// Use this for initialization
	void Start () {

		cachedY = batteryTransform.position.y;
		maxXValue = batteryTransform.position.x;
		minXValue = batteryTransform.position.x - batteryTransform.rect.width;
		//Debug.Log ("Minx value: "+minXValue);

		GameObject go = GameObject.FindGameObjectWithTag ("GameManager");
		gameManager = go.GetComponent<GameManager>();
		//maxBattery = gameManager.currentRobot.battery.max;
		//CurrentBattery = maxBattery;

	}
	// Update is called once per frame
	void Update () {

		//Hide the display if no robot is selected
		if(gameManager.currentRobot == null)
		{
			canvasGroup.alpha = 0.0f;
			return;
		}
		else 
			canvasGroup.alpha = 1.0f;

		//Get the battery of the current robot
		RobotBattery battery = gameManager.currentRobot.battery;

		//incase the current robot has changed
		maxBattery = battery.max;

		//Get level
		CurrentBattery = battery.level;

	}

	private float MapValues(float x, float inMin, float inMax, float outMin, float outMax) {
		return (x - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
	}

	private void HandleBattery() {
		// batteryText.text = "Battery: " + currentBattery; //for use with text readout

		float currentXValue = MapValues(_currentBattery, 0, maxBattery, minXValue, maxXValue);

		batteryTransform.position = new Vector3(currentXValue, cachedY, 0.0f);

		if (_currentBattery > maxBattery/2) //greater than 50% battery
		{
			visualLevel.color = new Color32((byte)MapValues(_currentBattery, maxBattery/2, maxBattery, 255, 0), 255, 0, 255);
		}
		else //under 50% battery
		{
			visualLevel.color = new Color32(255, (byte)MapValues(_currentBattery, 0, maxBattery/2, 0, 255), 0, 255);
		}

	}
}
