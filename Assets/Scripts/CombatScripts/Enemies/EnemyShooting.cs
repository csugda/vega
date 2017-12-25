using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour {
    public int weaponDamage = 10;
    public float fireRate = 0.5f;
    public float weaponRange = 15f;

    private WaitForSeconds shotDuration = new WaitForSeconds(0.1f);
    Ray shootRay;
    RaycastHit shootHit;
    LineRenderer weaponLine;
    AudioSource weaponAudio;
    private float nextFire;
    public Animator anim;

    void Awake()
    {
        weaponLine = GetComponent<LineRenderer>();
        weaponAudio = GetComponent<AudioSource>();
    }

    //TODO: change how enemies attack
    void Update()
    {
        if (Input.GetKeyDown("f") && Time.time > nextFire)
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
            StartCoroutine(Shoot(target.position - transform.position));
        }
    }
    private IEnumerator Shoot(Vector3 target)
    {
        weaponAudio.Play();
        anim.SetBool("IsShooting", true);
        weaponLine.enabled = true;
        weaponLine.SetPosition(0, transform.position);
        shootRay.origin = transform.position;
        shootRay.direction = target;

        if (Physics.Raycast(shootRay, out shootHit, weaponRange))
        {
            if (shootHit.collider.gameObject.tag == "Player")
            {
                // TODO: Damage Player
                shootHit.collider.gameObject.GetComponent<PlayerHealth>().onDamaged.Invoke(weaponDamage, this.transform);
            }
            weaponLine.SetPosition(1, shootHit.point);
        }
        else
        {
            weaponLine.SetPosition(1, shootRay.origin + shootRay.direction * weaponRange);
        }

        yield return shotDuration;
        weaponLine.enabled = false;
    }
}
