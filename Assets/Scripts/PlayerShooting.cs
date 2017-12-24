using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public int weaponDamage = 10;
    public float fireRate = 0.25f;
    public float weaponRange = 50f;

    private WaitForSeconds shotDuration = new WaitForSeconds(0.1f);
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
        if (Input.GetKeyDown(KeyCode.Mouse0) && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            StartCoroutine(Shoot(transform.forward));
        }       

    }
    public void Shoot(Vector3 location, Transform target)
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            StartCoroutine(Shoot(target.position-transform.position));
        }
    }
    private IEnumerator Shoot(Vector3 target)
    {
        weaponAudio.Play();
        weaponLine.enabled = true;
        weaponLine.SetPosition(0, transform.position);
        shootRay.origin = transform.position;
        shootRay.direction = target;

        if(Physics.Raycast(shootRay, out shootHit, weaponRange))
        {
            if (shootHit.collider.gameObject.tag == "Enemy")
            {
                shootHit.collider.gameObject.GetComponent<EnemyHealth>().onDammaged.Invoke(weaponDamage, this.transform);
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
