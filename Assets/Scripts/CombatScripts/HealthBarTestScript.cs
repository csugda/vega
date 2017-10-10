using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarTestScript : MonoBehaviour {

    public float maxHealth = 100f;
    public float curHealth = 0f;
    public GameObject healthBar;

	// Use this for initialization
	void Start ()
    {
        curHealth = maxHealth; // sets curHealth on play
       // InvokeRepeating("decreaseHealth", 1f, 1f); // decreases the health bar by x amount every second
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        float damagePerHit = 25f;
        if (other.gameObject.CompareTag("Player"))
        {
            decreaseHealth(damagePerHit);
        }
        // can add else statements for different tags corresponding to different weapons.
    }

    void decreaseHealth(float damage)
    {
        curHealth -= damage; // reduces health by damage
        float calcHealth = curHealth / maxHealth; // generates a decimal for scaling i.e. 0.5 for half health etc.
        SetHealthBar(calcHealth);
        if (curHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void SetHealthBar (float myHealth)
    {
        // myHealth needs to be between 0-1
        // scales health bar along the x axis of the canvas based on myHealth
        healthBar.transform.localScale = new Vector3(Mathf.Clamp(myHealth, 0f, 1f), healthBar.transform.localScale.y, healthBar.transform.localScale.z);

    }
}
