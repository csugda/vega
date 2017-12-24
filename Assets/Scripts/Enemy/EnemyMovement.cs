using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
    Transform player;                 // Reference to the player's position.
    //PlayerHealth playerHealth;      // Reference to the player's health.
    EnemyHealth enemyHealth;          // Reference to this enemy's health.
    UnityEngine.AI.NavMeshAgent nav;  // Reference to the nav mesh agent.
    Animator anim;
    bool detectedPlayer;

    void Awake()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        //playerHealth = player.GetComponent<PlayerHealth>();
        enemyHealth = GetComponent<EnemyHealth>();
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }
     
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            detectedPlayer = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            detectedPlayer = false;
        }
    }

    void Update()
    {
        // if enemy is alive and has detected the player
        if (enemyHealth.currHealth > 0 && detectedPlayer)
        {
            anim.SetBool("IsMoving", true);
            nav.SetDestination(player.position);
        }
        else
        {
            anim.SetBool("IsMoving", false);
        }
    }
}