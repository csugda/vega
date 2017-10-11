using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarTestScript : MonoBehaviour {

    public float maxHealth = 100f;
    private float curHealth = 0f;
    public GameObject healthBar;

	// Use this for initialization
	void Start ()
    {
        curHealth = maxHealth; // sets curHealth on play
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

    public void DecreaseHealth(int damage, Transform source)
    {
        curHealth -= damage; // reduces health by damage
        if (curHealth <= 0)
        {
            Destroy(this.gameObject);
        }
        healthBar.transform.localScale = 
            new Vector3(Mathf.Clamp(
                (curHealth / maxHealth), 0f, 1f),
                healthBar.transform.localScale.y, 
                healthBar.transform.localScale.z);
    }

    private void SetHealthBar ()
    {
        // myHealth needs to be between 0-1
        // scales health bar along the x axis of the canvas based on myHealth

    }
}
