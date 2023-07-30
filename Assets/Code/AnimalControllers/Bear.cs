using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Bear : MonoBehaviour
{
    Rigidbody rb;
    public Transform player;
    Animator animate;
    public float speed;
    public float rotateSpeed;
    public float chaseDistance;
    public float attackRange;



    bool wander = false;
    bool chasing = false;
    bool walking = false;
    bool leftRotation;
    bool rightRotation;
    Vector3 playerDirection;


    public TMP_Text text;
    // Start is called before the first frame update
    void Start()
    {
        rb= GetComponent<Rigidbody>();        
        animate= GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
            float distance = Vector3.Distance(player.position, transform.position);
            text.text = distance.ToString();

        if (Vector3.Distance(player.position,transform.position) < chaseDistance)
        {
            chasing = true;
            walking = false;
            leftRotation = false;
            rightRotation = false;
            chase();
        }
        else
        {
            chasing= false;
            StartCoroutine(Wander());
            rotation();
            walk();
        }
        animations();
    }

    void rotation()
    {
        if (leftRotation)
        {
            transform.Rotate(transform.up * Time.deltaTime * -rotateSpeed);
            //animate.SetBool("Idle", leftRotation);
        }
        if (rightRotation)
        {
            transform.Rotate(transform.up * Time.deltaTime * rotateSpeed);
            //animate.SetBool("Idle", rightRotation);
        }
    }

    void walk()
    {
        if (walking)
        {
            rb.AddForce(transform.forward * speed);
            Vector3 max = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
            //animate.SetBool("WalkForward", walking);
            if (max.magnitude > speed)
            {
                Vector3 slowPlayer = max.normalized * speed;
                rb.velocity = new Vector3(slowPlayer.x, rb.velocity.y, slowPlayer.z);
            }
        }
        if (!walking)
        {
            //animate.SetBool("WalkForward", walking);
        }
    }

    void animations()
    {
        animate.SetBool("Idle", leftRotation);
        animate.SetBool("Idle", rightRotation);
        animate.SetBool("WalkForward", walking);
        animate.SetBool("Run Forward", chasing);

    }
    void chase()
    {
        if (chasing)
        {
            playerDirection = player.position- transform.position;
            playerDirection.y = 0f;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(playerDirection),1.2f*Time.deltaTime);
            transform.Translate(0f,0f,0.1f);
            Vector3 max = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
            //animate.SetBool("Run Forward", chasing);
            if (max.magnitude > speed)
            {
                Vector3 slowPlayer = max.normalized * speed;
                rb.velocity = new Vector3(slowPlayer.x, rb.velocity.y, slowPlayer.z);
            }

            if(Vector3.Distance(player.position, transform.position) < attackRange)
            {
                attack();
            }
        }
        if (!chasing)
        {
            //animate.SetBool("Run Forward", chasing);
        }
    }

    void attack()
    {
        animate.SetTrigger("Attack1");
        animate.SetTrigger("Attack2");
        animate.SetTrigger("Attack8");

    }
    IEnumerator Wander()
    {
        int direction = Random.Range(1, 2);
        int delay = Random.Range(1, 3);
        int roatationSpeed = Random.Range(1, 3);
        int walkDelay = Random.Range(1, 3);
        int walkTime = Random.Range(1,3);
        wander = true;

        yield return new WaitForSeconds(walkDelay);
        walking = true;
        yield return new WaitForSeconds(walkTime);
        walking = false;
        yield return new WaitForSeconds(delay);
        if (direction == 1)
        {
            leftRotation = true;
            yield return new WaitForSeconds(delay);
            leftRotation = false;
        }
        if (direction == 2)
        {
            rightRotation = true;
            yield return new WaitForSeconds(delay);
            rightRotation = false;
        }
        wander = false;
    }

}
