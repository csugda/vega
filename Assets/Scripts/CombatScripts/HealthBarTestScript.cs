﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarTestScript : MonoBehaviour {

    public float maxHealth;
    public float curHealth;
    public GameObject healthBar;
    Vector3 initalScale= Vector3.one; //NOTE make sure that the local scale is actully one to begin with..
	// Use this for initialization
	public void Start ()
    {
       // InvokeRepeating("decreaseHealth", 1f, 1f); // decreases the health bar by x amount every second
	}



    //private void OnCollisionEnter(Collision other)
    //{

    //    if (other.gameObject.CompareTag("Player"))
    //    {
    //        float damagePerHit = 25f;
    //        decreaseHealth(damagePerHit);
    //    }
    //    // can add else statements for different tags corresponding to different weapons.
    //}
    public void DecreaseHealth(int damage)
    {
        DecreaseHealth(-damage, null);
    }
    public void DecreaseHealth(int damage, Transform source)
    {
        if (curHealth - damage < 0)
            curHealth = 0;
        if (curHealth - damage > maxHealth)
            curHealth = maxHealth;
        curHealth -= damage; // reduces health by damage
        if (initalScale == Vector3.zero)
            this.Start();
        healthBar.transform.localScale = 
            new Vector3(Mathf.Clamp(
                (curHealth / maxHealth), 0f, 1f)*initalScale.x,initalScale.y,initalScale.z);
    }
    public void ChangeMaxHealth(int ammount)
    {
        maxHealth += ammount;
        DecreaseHealth(0);
    }
    private void SetHealthBar ()
    {
        // myHealth needs to be between 0-1
        // scales health bar along the x axis of the canvas based on myHealth

    }
}
