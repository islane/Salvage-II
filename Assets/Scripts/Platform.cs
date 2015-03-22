using UnityEngine;
using System.Collections;

public class Platform : MonoBehaviour {
	
	public Vector3 velocity {get; protected set;}
	public Transform myTransform {get; protected set;}
	
	public float leftDir;
	public float rightDir;
	public float horzSpeed;
	
	public float maxHeight;
	public float minHeight;
	public float vertSpeed;

	void Start () {
		myTransform = transform;
		PlatformStart();
	}
	
	void Update(){
		Move();
		PlatformUpdate();
	}

	virtual protected void Move() {
		myTransform.Translate(velocity * 0.033f);
	}


	protected void PlatformStart() {

		if (vertSpeed < 0.0f && horzSpeed < 0.0f) {
			velocity = new Vector3 (horzSpeed, vertSpeed, 0.0f);
			vertSpeed *= -1;
			horzSpeed *= -1;
		} else if (horzSpeed < 0.0f && vertSpeed > 0.0f) {
			velocity = new Vector3(horzSpeed, vertSpeed, 0.0f);
			horzSpeed *= -1;
		} else if (horzSpeed > 0.0f && vertSpeed < 0.0f) {
			velocity = new Vector3(horzSpeed, vertSpeed, 0.0f);
			vertSpeed *= -1;
		} else {
			velocity = new Vector3(horzSpeed, vertSpeed, 0.0f);
		}
	}



	protected void PlatformUpdate() {
		// Vertical check - needs more testing, fix for negative values...
//		if (myTransform.position.y >= maxHeight) {
//			myTransform.position = new Vector3(myTransform.position.x, maxHeight, myTransform.position.z);
//			velocity = new Vector3(0.0f, -1 * vertSpeed, 0.0f);
//		} else if (myTransform.position.y <= minHeight) {
//			myTransform.position = new Vector3(myTransform.position.x, minHeight, myTransform.position.z);
//			velocity = new Vector3(0.0f, vertSpeed, 0.0f);
//		}

		//Horizontal check
		if (myTransform.position.x >= rightDir) {
			myTransform.position = new Vector3(rightDir, myTransform.position.y, myTransform.position.z);
			velocity = new Vector3(-1 * horzSpeed, 0.0f, 0.0f);
		} else if (myTransform.position.x <= leftDir) {
			myTransform.position = new Vector3(leftDir, myTransform.position.y, myTransform.position.z);
			velocity = new Vector3(horzSpeed, 0.0f, 0.0f);
		}
	}



}
