using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    // Player ka reference
    [Header("Player")]
    [SerializeField] private PlayerMovement player;

    // NavMesh agent jo enemy ko move karata hai
    [Header("Navmesh")]
    [SerializeField] private NavMeshAgent agent;

    // Player ki position ka reference
    [Header("Player")]
    [SerializeField] private Transform playerPos;

    // Enemy ka animator for animations
    [Header("Animator")]
    [SerializeField] private Animator animator;

    // Ground aur player layer ke masks
    [Header("LayerMasks")]
    [SerializeField] private LayerMask whatIsGround, whatIsPlayer;

    // Attack delay handle karne ke liye
    [Header("Attacking")]
    [SerializeField] private float attackTime;
    private bool alreadyAttack;

    // Enemy ke state check karne ke liye
    [Header("States")]
    [SerializeField] private float attackrange;
    [SerializeField] private float sightRange;
    private bool playerAttackRange;
    private bool playerInSightRange;
    private bool walkPointSet;
    Vector3 walkPoint;

    // Damage aur patrol range
    [SerializeField] private int enemyDamage;
    [SerializeField] private float walkPointRange;

    private int currentHealth;

    private void Start()
    {
        // Player position ko find kar rahe hain (runtime pe)
        playerPos = GameObject.Find("Space_Soldier_A").transform;

        // NavMesh Agent assign kar rahe hain
        agent = GetComponent<NavMeshAgent>();

        // Agar player ka reference set nahi hua to runtime pe find karo
        if (player == null)
        {
            player = FindObjectOfType<PlayerMovement>();
        }
    }

    private void Update()
    {
        // Enemy detect karta hai ke player nazdeek ya attack range mein hai ya nahi
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerAttackRange = Physics.CheckSphere(transform.position, attackrange, whatIsPlayer);

        // Teen conditions ke hisaab se behavior change karta hai
        if (!playerAttackRange && !playerInSightRange)
        {
            Patrolling();
        }
        if (playerInSightRange && !playerAttackRange)
        {
            Chasing();
        }
        if (playerAttackRange && playerInSightRange)
        {
            Attack();
        }
    }

    private void Patrolling()
    {
        // Agar walk point set nahi to naya point dhundo
        if (!walkPointSet)
        {
            SearcWalkPoint();
        }

        // Walk point mil gaya to uss direction mein move karo
        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
        }

        // Check karo ke destination pe pohanch gaye ya nahi
        Vector3 dis = transform.position - walkPoint;
        if (dis.magnitude > 1f)
        {
            walkPointSet = false;
        }
    }

    private void SearcWalkPoint()
    {
        // Random direction mein walk point set karo
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        // Raycast check karta hai ground pe hi chal rahe hain ya nahi
        if (Physics.Raycast(walkPoint, -transform.up, 10f))
        {
            walkPointSet = true;
        }
    }

    private void Chasing()
    {
        // Player ke peeche bhago
        agent.SetDestination(playerPos.position);
        animator.SetBool("Run", true);
    }

    private void Attack()
    {
        // Attack range mein ho to ruk jao
        agent.SetDestination(transform.position);

        // Player ki taraf dekho
        transform.LookAt(playerPos);

        // Agar already attack nahi kar rahe to animation aur timer set karo
        if (!alreadyAttack)
        {
            animator.SetBool("Run",false);
            animator.SetBool("Attacking", true);
            alreadyAttack = true;
            Invoke(nameof(ResetAttack), attackTime); // Attack dubara tabhi ho jab cooldown complete ho
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Agar bullet se takra gaye to "hit" message aur deactivate
        if (collision.collider.CompareTag("Bullet"))
        {
            Debug.Log("Hit!");
            gameObject.SetActive(false);
        }
    }

    private void Die()
    {
        // Agent ko rok do aur death animation play karo
        agent.isStopped = true;
        animator.SetTrigger("Dead");

        // RigidBody freeze karo takay gire ya move na kare
        if (GetComponent<Rigidbody>() != null)
        {
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }

        // Enemy ka script disable kar do
        this.enabled = false;

        // Death animation complete hone ke baad deactivate karo
        StartCoroutine(DeactivateAfterDeath());
    }

    private IEnumerator DeactivateAfterDeath()
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        gameObject.SetActive(false);
    }

    private void ResetAttack()
    {
        // Attack ka cooldown reset
        alreadyAttack = false;
        animator.SetBool("Attacking", false);
    }
}
