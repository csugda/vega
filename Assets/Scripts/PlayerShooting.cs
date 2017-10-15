using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public int weaponDamage = 10;
    public float fireRate = 0.25f;
    public float weaponRange = 50f;

    private WaitForSeconds shotDuration = new WaitForSeconds(0.70f);
    Ray shootRay;
    RaycastHit shootHit;
    LineRenderer weaponLine;
    AudioSource weaponAudio;
    private float nextFire;

    void Awake()
    {
        weaponLine = GetComponent<LineRenderer>();
        weaponAudio = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            StartCoroutine(Shoot());
        }

    }

    private IEnumerator Shoot()
    {
        weaponAudio.Play();
        weaponLine.enabled = true;
        weaponLine.SetPosition(0, transform.position);
        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;

        if(Physics.Raycast(shootRay, out shootHit, weaponRange))
        {
            EnemyHealth enemyHealth = shootHit.collider.GetComponent<EnemyHealth>();

            if (enemyHealth != null)
            {
                enemyHealth.Damage(weaponDamage, shootHit.point);
            }

            weaponLine.SetPosition(1, shootHit.point);
        } else
        {
            weaponLine.SetPosition(1, shootRay.origin + shootRay.direction * weaponRange);
        }

        yield return shotDuration;
        weaponLine.enabled = false;
    }

}
