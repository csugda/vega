using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[System.Serializable]
public class MouseEvent: UnityEvent<int, Vector3, Transform>{
}
[System.Serializable]
public class MouseUpEvent: UnityEvent<int>{
}

public class MouseInput : MonoBehaviour {
	public MouseEvent mouseButton;    // fires if a mouse button is held down this frame.
	public MouseEvent mouseButtonDown;// fires if the user pressed a mouse button this frame.
	public MouseUpEvent mouseButtonUp;  // fires if the user released a mouse button this frame.

	void Update () {
		for (int i = 0; i < 2; i++) {
			if (Input.GetMouseButton(i)) {
				RaycastHit hit;
				if (RaycastFromMouse(out hit)) {
					if (Input.GetMouseButtonDown(i)) {
						mouseButtonDown.Invoke(i, new Vector3(hit.point.x, 0, hit.point.z), hit.transform);
					}
					mouseButton.Invoke(i, new Vector3(hit.point.x, 0, hit.point.z), hit.transform);
				}
			}
			else if (Input.GetMouseButtonUp(i)) {
				mouseButtonUp.Invoke(i);
			}
		}
	}

	bool RaycastFromMouse (out RaycastHit rcHit) {
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast(ray, out hit, 100.0f)) {
			rcHit = hit;
			return true;
		}
		rcHit = hit;
		return false;
	}
}
