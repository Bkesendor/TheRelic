using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ArcherAI : MonoBehaviour
{
    // patrolling random between waypoints
    public Transform[] moveSpots;
    private int randomSpot;

    //waypoint patrol
    private float waitTime;
    public float startWaitTime = 1f;
    public Transform player;

    NavMeshAgent nav;

    //AI strafe
    public float distToPlayer = 5.0f; //radius of starfe

    private float randomStrafeStartTime;
    private float waitStrafeTime;
    public float t_minStrafe;
    public float t_maxStrafe;

    public Transform strafeRight;
    public Transform strafeLeft;
    private int randomStrafeDir;

    //when to chase
    public float chaseRadius = 20f;
    public float facePlayerFactor = 20f;

    private Animator animator;
    const float locomationAnimationSmoothtime = .1f;

    //attack
    public Transform arrowSpawn;
    public GameObject arrowPrefab;
    public float shootForce = 40f;

    //health
    public int maxHB = 450;
    public int currentHB;
    public BossHB bossHB;

    //sound
    public AudioSource ThrowArrow;

    private void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        nav.enabled = true;
    }

    void Start()
    {
        waitTime = startWaitTime;
        randomSpot = Random.Range(0, moveSpots.Length);
        animator = GetComponent<Animator>();
        currentHB = maxHB;
        bossHB.BossMaxHealth(maxHB); 
    }

    void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);

        if (distance > chaseRadius)
        {
            Patrol();
            animator.SetTrigger("Walk");
        }
        else if (distance <= chaseRadius)
        {
            ChasePlayer();
            FacePlayer();
            animator.SetTrigger("Attack");
        }

        
        //death
        ded();
    }

    void Patrol()
    {
        nav.SetDestination(moveSpots[randomSpot].position);

        if (Vector3.Distance(transform.position, moveSpots[randomSpot].position) < 2.0f)
        {
            if (waitTime < 0)
            {
                randomSpot = Random.Range(0, moveSpots.Length);

                waitTime = startWaitTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }

    void ChasePlayer()
    {
        float distance = Vector3.Distance(player.position, transform.position);  //============

        if (distance <= chaseRadius && distance > distToPlayer)
        {
            nav.SetDestination(player.position);
        }
        else if (nav.isActiveAndEnabled && distance <= distToPlayer)
        {
            randomStrafeDir = Random.Range(0, 2);
            randomStrafeStartTime = Random.Range(t_minStrafe, t_maxStrafe);

            if (waitStrafeTime <= 0)
            {
                if (randomStrafeDir == 0)
                {
                    nav.SetDestination(strafeLeft.position);

                }
                else if(randomStrafeDir==1)
                {
                    nav.SetDestination(strafeRight.position);

                }
                waitStrafeTime = randomStrafeStartTime;
            }
            else
            {
                waitStrafeTime -= Time.deltaTime;

            }
        }
    }

    void FacePlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * facePlayerFactor);
    }

    public void ArcherAttack()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        GameObject go = Instantiate(arrowPrefab, arrowSpawn.position, Quaternion.LookRotation(new Vector3(direction.x, direction.y, direction.z)));
        Rigidbody rb = go.GetComponent<Rigidbody>();
        //rb.velocity = transform.forward * shootForce;
        rb.velocity = direction * shootForce;
    }

    public void ArcherAttackSound()
    {
        ThrowArrow.Play();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Sword")
        {
            currentHB -= 50;
            bossHB.BossSetHealth(currentHB);
        }
    }

    void ded()
    {
        if (currentHB <= 0)
        {
           Destroy(gameObject);
        }
    }
}
