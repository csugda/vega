using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapToMove : MonoBehaviour
{
    //flag to check if the user has tapped / clicked. 
    //Set to true on click. Reset to false on reaching destination
    private bool flag = false;
    //destination point
    private Vector3 endPoint;
    //alter this to change the speed of the movement of player / gameobject
    public float duration = 50.0f;
    //vertical position of the gameobject
    private float yAxis;

    void Start()
    {
        //save the y axis value of gameobject
        yAxis = gameObject.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        //check if the flag for movement is true and the current gameobject position is not same as the clicked / tapped position
        if (flag && !VectorApproximately(gameObject.transform.position, endPoint))
        { //&& !(V3Equal(transform.position, endPoint))){
          //move the gameobject to the desired position
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, endPoint, 1 / (duration * (Vector3.Distance(gameObject.transform.position, endPoint))));
        }
        //set the movement indicator flag to false if the endPoint and current gameobject position are equal
        else if (flag && VectorApproximately(gameObject.transform.position, endPoint))
        {
			
            flag = false;
            Debug.Log("I am here");
        }

    }

	public void OnMouseButtonDown(int button, Vector3 pos, Transform obj) {
		if (button == 0) {
			MoveTo(pos);
		}
	}
	public void OnMouseButton(int button, Vector3 pos, Transform obj) {
		if (button == 0) {
			MoveTo(pos);
		}
	}
	// Moves the object to pos
	void MoveTo(Vector3 pos) {
		//set a flag to indicate to move the gameobject
		flag = true;
		//save the click / tap position
		endPoint = pos;
		//as we do not want to change the y axis value based on touch position, reset it to original y axis value
		endPoint.y = yAxis;
		Debug.Log(endPoint);
	}

	// Returns true if the vectors are approximately equal, false otherwise.
	bool VectorApproximately(Vector3 v1, Vector3 v2) {
		return (Mathf.Approximately(v1.x, v2.x) && Mathf.Approximately(v1.x, v2.x) && Mathf.Approximately(v1.x, v2.x));
	}
}


