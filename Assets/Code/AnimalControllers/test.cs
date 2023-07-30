using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class test : MonoBehaviour
{

    [SerializeField] GameObject player;
    NavMeshAgent agent;
    [SerializeField]
    BoxCollider boxCollider;
    [SerializeField]
    LayerMask groundLayer, playerMask;
    Vector3 ranPoints;
    Animator animate;
    bool wandering;
    [SerializeField] float walkDistance;
    [SerializeField] float agroRange, attackRange;
    bool chaseRange, attacking, attackReset;
    public bool dead = false;



    //states
    enum state
    {
        Idle,
        Walk,
        Chase,
        Stun, 
        Attack
    }

    void Start()
    {
        agent= GetComponent<NavMeshAgent>();
        animate= GetComponent<Animator>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (!dead)
        {
            chaseRange = Physics.CheckSphere(transform.position, agroRange, playerMask);
            attacking = Physics.CheckSphere(transform.position, attackRange, playerMask);


            if (!chaseRange && !attacking)
            {
                Wander();
            }
            if (chaseRange && !attacking)
            {
                Chase();
            }
            if (chaseRange && attacking)
            {
                Attack();
            }
        }
    }

    void Wander()
    {
        if(!wandering)
        {
            animate.SetBool("Idle", true);
            animate.SetBool("WalkForward", false);
            newLocation();
        }
        if(wandering) 
        {
            animate.SetBool("Idle", false);
            animate.SetBool("WalkForward", true);
            agent.SetDestination(ranPoints);
        }
        if(Vector3.Distance(transform.position,ranPoints)<10)
        {
            animate.SetBool("Idle", true);
            wandering = false;
        }
    }

    void Chase()
    {
        animate.SetBool("Idle", false);
        animate.SetBool("WalkForward", false);
        animate.SetBool("Run Forward", true);
        agent.SetDestination(player.transform.position);
    }

    void Attack()
    {
        if(!animate.GetCurrentAnimatorStateInfo(0).IsName("Attack1") && attackReset)
        {
            attackReset= false;
            animate.SetTrigger("Attack1");
            agent.SetDestination(transform.position);
        }
        boxCollider.enabled = false;
        Invoke("canAttack", 3);
    }

    void newLocation()
    {
        float x = Random.Range(-walkDistance, walkDistance);
        float z = Random.Range(-walkDistance, walkDistance);

        ranPoints= new Vector3(transform.position.x + x, transform.position.y,transform.position.z +z);
        if(Physics.Raycast(ranPoints,Vector3.down,groundLayer))
        {
            wandering= true;
        }
    }
    public void hit()
    {
        Debug.Log("Hit");
        attacking = false;
        wandering= false;
        if (!animate.GetCurrentAnimatorStateInfo(0).IsName("Get Hit Front"))
        {
            animate.SetTrigger("Get Hit Front");
        }
    }

    public void die()
    {
        wandering= false;
        attacking= false;
        dead= true;
        if(!animate.GetCurrentAnimatorStateInfo(0).IsName("Death"))
        {
            animate.SetTrigger("Death");
        }
        Invoke("Destroy",2.5f);
        GameObject player = GameObject.Find("Player");
        player.GetComponent<playerController>().addScore();
        GameObject.Find("Predators").GetComponent<PredatorController>().enemyKilled();
        Destroy(gameObject);
    }
    void canAttack()
    {
        boxCollider.enabled = true;
        attackReset = true;
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }
}
