using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
    GameObject player;                 // Reference to the player.
    //PlayerHealth playerHealth;      // Reference to the player's health.
    EnemyHealth enemyHealth;          // Reference to this enemy's health.
    UnityEngine.AI.NavMeshAgent nav;  // Reference to the nav mesh agent.
    public bool detectedPlayer = false;
    Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        //playerHealth = player.GetComponent<PlayerHealth>();
        enemyHealth = GetComponent<EnemyHealth>();
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    void onTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            detectedPlayer = true;
        }
    }

    void onTriggerExit (Collider other)
    {
        if (other.gameObject == player)
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
            nav.SetDestination(player.transform.position);
        }
        else
        {
            anim.SetBool("IsMoving", false);
        }
    }
}