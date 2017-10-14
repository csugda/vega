using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastShoot : MonoBehaviour
{

    public int gunDamage = 1;                                           
    public float fireRate = 0.25f;                                      
    public float weaponRange = 50f;                                     
    public float hitForce = 100f;                                       // Amount of force which will be added to objects with a rigidbody shot by the player
    public Transform gunEnd;                                            // Holds a reference to the gun end object, marking the muzzle location of the gun

    private WaitForSeconds shotDuration = new WaitForSeconds(0.70f);    // WaitForSeconds object used by our ShotEffect coroutine, determines time laser line will remain visible
    private AudioSource gunAudio;                                       // Reference to the audio source which will play our shooting sound effect
    private LineRenderer laserLine;                                     // Reference to the LineRenderer component which will display our laserline
    private float nextFire;                                             // Float to store the time the player will be allowed to fire again, after firing


    void Start()
    {
        laserLine = GetComponent<LineRenderer>();
        gunAudio = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            
            // Start our ShotEffect coroutine to turn our laser line on and off
            StartCoroutine(ShotEffect());

            Vector3 rayOrigin = this.transform.position;
            RaycastHit hit;
            laserLine.SetPosition(0, gunEnd.position);

            // Check if our raycast has hit anything
            if (Physics.Raycast(rayOrigin, this.transform.forward, out hit, weaponRange))
            {
                laserLine.SetPosition(1, hit.point);
                HealthBarTestScript health = hit.collider.GetComponent<HealthBarTestScript>(); 

                if (health != null)
                {
                    health.DecreaseHealth(gunDamage, this.transform);
                }

                if (hit.rigidbody != null)
                {
                    hit.rigidbody.AddForce(-hit.normal * hitForce);
                }
                
            }
            else
            {
                laserLine.SetPosition(1, rayOrigin + (this.transform.forward * weaponRange));
            }
        }
    }


    private IEnumerator ShotEffect()
    {
        gunAudio.Play();
        laserLine.enabled = true;
        yield return shotDuration;
        laserLine.enabled = false;
    }
}