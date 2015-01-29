using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DrainBar : MonoBehaviour {

	// float currentGraphScale = 1.0f;//draining speed vs bar drain speed
	
	public int drainRate;
	public RectTransform batteryTransform;
	private float cachedY;
	private float minXValue;
	private float maxXValue;
	public int maxBattery; //scales depending on the robot in use
	public int currentBattery;

	public int CurrentBattery //access this to edit battery level
	{
		get { return currentBattery; }
		set { 
			currentBattery = value;
			HandleBattery();
		}
	}
	public int getMaxBattery(){
		return maxBattery;
	}

	public Image visualLevel; 
	// public Text batteryText; //for use with text readout of battery level

	// Use this for initialization
	void Start () {

		if (!(GameObject.FindGameObjectsWithTag ("RobotPush") == null)) {
			drainRate = 1;//whatever the appropriate drain rate is here
			maxBattery = 100;//Again, whatever the battery is for the particular bot
		}
		if (!(GameObject.Find("RobotSmall") == null)) {
			drainRate = 1;//whatever the appropriate drain rate is here
			maxBattery = 100;//Again, whatever the battery is for the particular bot
		}
		if (!(GameObject.Find("JumpRobot") == null)) {
			drainRate = 1;//whatever the appropriate drain rate is here
			maxBattery = 100;//Again, whatever the battery is for the particular bot
		}
		
		cachedY = batteryTransform.position.y;
		maxXValue = batteryTransform.position.x;
		minXValue = batteryTransform.position.x - batteryTransform.rect.width;
		Debug.Log ("Minx value: "+minXValue);
		currentBattery = maxBattery;

	}
	 // Update is called once per frame
	 void Update () {//below is check for death, TODO move some code from robot here or vice versa...
		if (currentBattery <= 0) {

			Application.LoadLevel("GameOver");
		}
	}
	// public void OnGUI(){
	// 	GUI.backgroundColor = Color.yellow;
	// 	GUI.Button(new Rect(10, 40, 100 * currentGraphScale, 20), "Energy");
	// }
	// public void drainBattery(){
	// 	currentBattery -= drainRate;
	// 	currentGraphScale = currentBattery * maxBattery;
	// }

	private float MapValues(float x, float inMin, float inMax, float outMin, float outMax) {
		return (x - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
	}

	private void HandleBattery() {
		// batteryText.text = "Battery: " + currentBattery; //for use with text readout

		float currentXValue = MapValues(currentBattery, 0, maxBattery, minXValue, maxXValue);

		batteryTransform.position = new Vector3(currentXValue, cachedY);

		if (currentBattery > maxBattery/2) //greater than 50% battery
		{
			visualLevel.color = new Color32((byte)MapValues(currentBattery, maxBattery/2, maxBattery, 255, 0), 255, 0, 255);
		}
		else //under 50% battery
		{
			visualLevel.color = new Color32(255, (byte)MapValues(currentBattery, 0, maxBattery/2, 0, 255), 0, 255);
		}

	}
}
