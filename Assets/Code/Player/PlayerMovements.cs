using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerMovements : MonoBehaviour
{
    // Start is called before the first frame update
    float horizontal;
    float vertical;
    public float speed;
    public float height;
    public float friction;
    public float jumpForce;
    public float airbonus;
    public float cooldown;
    bool canjump = true;
    bool grounded;
    Vector3 moveDirection;

    public Transform direction;
    public LayerMask ground;
    Rigidbody rb;

    public TMP_Text text;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void inputs()
    {
        horizontal =   Input.GetAxisRaw("Horizontal");
        vertical =   Input.GetAxisRaw("Vertical");
        if (Input.GetKey("space") && grounded && canjump)
        {
            canjump= false;
            Jump();
            Invoke(nameof(JumpReset), cooldown);
        }        
    }

    private void Move()
    {
        //direction player facing
        moveDirection = direction.forward*vertical + direction.right*horizontal;
        if (grounded)
        {
            rb.AddForce(moveDirection * speed, ForceMode.Force);
        }
        if(!grounded)
        {
            rb.AddForce(moveDirection * speed * airbonus, ForceMode.Force);
        }
    }

    void maxSpeed()
    {
        Vector3 max = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (max.magnitude > speed)
        {
            Vector3 slowPlayer = max.normalized*speed;
            rb.velocity= new Vector3(slowPlayer.x,rb.velocity.y,slowPlayer.z);
        }
    }

    void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(transform.up*jumpForce,ForceMode.Impulse);
    }

    void JumpReset()
    {
        canjump= true;
    }
    // Update is called once per frame
    void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, height*0.5f + 0.05f);
        inputs();
        maxSpeed();
        if (grounded)
            rb.drag = friction;
        else
            rb.drag = 0;
    }
    void FixedUpdate()
    {
        Move();
    }
}
