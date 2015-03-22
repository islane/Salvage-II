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

			if (leftDir == 0 && rightDir == 0) {
				// check for vertical only

				if (myTransform.localPosition.y >= maxHeight) {
					myTransform.localPosition = new Vector3(myTransform.localPosition.x, maxHeight, myTransform.localPosition.z);
					velocity = new Vector3(0.0f, -1 * vertSpeed, 0.0f);
				} else if (myTransform.localPosition.y <= minHeight) {
					myTransform.localPosition = new Vector3(myTransform.localPosition.x, minHeight, myTransform.localPosition.z);
					velocity = new Vector3(0.0f, vertSpeed, 0.0f);
				}

			} else if (maxHeight == 0 && minHeight == 0) {
				//check for horizontal only
				if (myTransform.localPosition.x >= rightDir) {
					myTransform.localPosition = new Vector3 (rightDir, myTransform.localPosition.y, myTransform.localPosition.z);
					velocity = new Vector3 (-1 * horzSpeed, 0.0f, 0.0f);
				} else if (myTransform.localPosition.x <= leftDir) {
					myTransform.localPosition = new Vector3 (leftDir, myTransform.localPosition.y, myTransform.localPosition.z);
					velocity = new Vector3 (horzSpeed, 0.0f, 0.0f);
				}
			
			} else {
				// check both horizontal and vertical
				if (myTransform.localPosition.x >= rightDir) {
					myTransform.localPosition = new Vector3 (rightDir, maxHeight, myTransform.localPosition.z);
					velocity = new Vector3 (-1 * horzSpeed, -1 * vertSpeed, 0.0f);
				} else if (myTransform.localPosition.x <= leftDir) {
					myTransform.localPosition = new Vector3 (leftDir, minHeight, myTransform.localPosition.z);
					velocity = new Vector3 (horzSpeed, vertSpeed, 0.0f);
				}
			}

		}


}
