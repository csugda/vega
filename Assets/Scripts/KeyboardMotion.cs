using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// NOTE: this is a temporary script used soley for testing perspective.
public class KeyboardMotion : MonoBehaviour {
	public float speed = 1.0f;
	void Start () {
		
	}

	void Update () {
		int xOffset = 0, yOffset = 0;
		if (Input.GetKey(KeyCode.LeftArrow)) {
			xOffset = -1;
		}
		else if (Input.GetKey(KeyCode.RightArrow)) {
			xOffset = 1;
		}
		if (Input.GetKey(KeyCode.UpArrow)) {
			yOffset = 1;
		}
		else if (Input.GetKey(KeyCode.DownArrow)) {
			yOffset = -1;
		}
		transform.position = new Vector3(transform.position.x + xOffset * speed, transform.position.y, transform.position.z + yOffset * speed);
	}
}
