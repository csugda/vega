using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class TapToMove : MonoBehaviour
{
    // declare variables for use in counting collectibles
    public Text scrapText;
    private int scrapAmount;

    public GameObject menuGO;
    NavMeshAgent agent;

    //flag to check if the user has tapped / clicked. 
    //Set to true on click. Reset to false on reaching destination
    //private bool flag = false; PROBABLY NOT NEEDED ANYMORE! (otherwise killz me by commentz)
    //destination point
    private Vector3 endPoint;
    //alter this to change the speed of the movement of player / gameobject
    //public float duration = 50.0f; PROBABLY NOT NEEDED ANYMORE! (otherwise killz me by commentz)
    //vertical position of the gameobject
    private float yAxis;


    void Start()
    {
        // define variables for counting collectibles
        scrapAmount = 0;
        SetScrapText();

        //save the y axis value of gameobject
        yAxis = gameObject.transform.position.y;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
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
	public void MoveToMenuGO() {
		MoveTo(menuGO.transform.position);
	}
	// Moves the object to pos
	void MoveTo(Vector3 pos) {
		//save the click/tap position with object's original y axis value
		endPoint = new Vector3(pos.x, yAxis, pos.z);
        agent.SetDestination(endPoint);
    }
    void OnTriggerEnter(Collider other) // triggers on contact with a collider
    {
        if (other.gameObject.CompareTag("Pick Up")) // compares the tag of an object
        {
            other.gameObject.SetActive(false); // sets gameobject to inactive
            scrapAmount++; // increments the scrap count
            SetScrapText(); // calls method for setting the amount of scrap collected
        }
    }
    // method for setting the amount of scrap collected
    void SetScrapText()
    {
       scrapText.text = "Piles of Scrap: " + scrapAmount.ToString();
    }
    // Returns true if the vectors are approximately equal, false otherwise.
    //bool VectorApproximately(Vector3 v1, Vector3 v2) { 
    //PROBABLY NOT NEEDED ANYMORE! (otherwise killz me by commentz)
    //	return (Mathf.Approximately(v1.x, v2.x) && Mathf.Approximately(v1.x, v2.x) && Mathf.Approximately(v1.x, v2.x));
    //}
}


