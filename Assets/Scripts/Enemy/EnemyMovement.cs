using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
    public float detectionRange = 15f;
    public float damp = 10f;          // Higher means the enemy turns to face the player faster.
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
  
    public IEnumerator Aim()
    {
        var rotationAngle = Quaternion.LookRotation(player.position - transform.position);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotationAngle, Time.deltaTime * damp);
        yield return null;
    }

    void Update()
    {
        // if enemy is alive and has detected the player
        if (enemyHealth.currHealth > 0)
        {
            float distance = Vector3.Distance(transform.position, player.position);
            bool detectedPlayer = distance < detectionRange;
            if (detectedPlayer)
            {
                anim.SetBool("IsMoving", true);
                nav.SetDestination(player.position);
            }
        }
        else
        {
            anim.SetBool("IsMoving", false);
        }
    }
}