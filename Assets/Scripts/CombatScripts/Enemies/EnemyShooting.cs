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
    EnemyMovement enemyMovement;
    private float nextFire;
    public Animator anim;
    LayerMask ignoreLayer;

    void Awake()
    {
        weaponLine = GetComponent<LineRenderer>();
        weaponAudio = GetComponent<AudioSource>();
        enemyMovement = (EnemyMovement) GetComponentInParent(typeof(EnemyMovement));
        ignoreLayer = LayerMask.GetMask("Default");
    }

    //TODO: change how enemies attack
    void Update()
    {
        if (Input.GetKeyDown("f") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            StartCoroutine(enemyMovement.Aim());
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

        if (Physics.Raycast(shootRay, out shootHit, weaponRange, ignoreLayer))
        {
            if (shootHit.collider.gameObject.tag == "Player")
            {
                // TODO: Damage Player
                Debug.Log("Hit Player");
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
