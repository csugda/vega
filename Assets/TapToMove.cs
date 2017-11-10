using UnityEngine;
using UnityEngine.AI;

public class TapToMove : MonoBehaviour
{
    public bool isMoving = false;
    public bool isShooting = false;
    public GameObject menuGO;
    NavMeshAgent agent;

    //destination point
    private Vector3 endPoint;
    //alter this to change the speed of the movement of player / gameobject
    //vertical position of the gameobject
    private float yAxis;
    Rigidbody playerRigidBody;
    float camRayLength = 100f;
    float rotationSpeed = 5f;

    void Awake()
    {
        playerRigidBody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        //save the y axis value of gameobject
        yAxis = gameObject.transform.position.y;
        agent = GetComponent<NavMeshAgent>();
    }

    void FixedUpdate()
    {
        Turn();
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
		//save the click/tap position with object's original y axis value
		endPoint = new Vector3(pos.x, yAxis, pos.z);
        agent.SetDestination(endPoint);
    }

    //move to the object clicked on. 
    public void Move(Vector3 pos, Transform target)
    {
        //save the click/tap position with object's original y axis value
        endPoint = new Vector3(pos.x, yAxis, pos.z);
        agent.SetDestination(endPoint);
    }

    public void Turn()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorHit;
        if (Physics.Raycast(camRay, out floorHit, camRayLength))
        {
            Vector3 playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0f;

            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            transform.rotation = Quaternion.Slerp(playerRigidBody.transform.rotation, newRotation, Time.deltaTime * rotationSpeed);
            //playerRigidBody.MoveRotation(newRotation);
        }
    }
}
