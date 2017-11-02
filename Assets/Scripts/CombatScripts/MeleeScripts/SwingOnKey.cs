using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingOnKey : MonoBehaviour {

    public Animator anim;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.A))
        {
            anim.Play("StickSwing");

        }
        //else if (Input.GetKeyUp(KeyCode.A))
        //{
        //    anim.Play("New State");
        //}
	}
}
