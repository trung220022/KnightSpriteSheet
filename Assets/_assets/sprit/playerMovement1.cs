using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement1 : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] fireballs;
    [SerializeField] private float attackCooldown;
  


    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private float wallJumpCooldown;
    private float horizontalInput; 
    private bool crouch1;
    private bool win1;
    private bool die1;
    private bool hurt1;
    private bool dizzy1;
    private bool strike1;



    private void Awake()
    {
        //Grab references for rigibody and animator from object
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
       

    }

    private void Update()
    {
        float horizontalInput = 0f;
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            horizontalInput = -1f; // Di chuyển sang trái
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            horizontalInput = 1f; // Di chuyển sang phải
        }

        //flip player when moving left.right
        if (horizontalInput > 0.01f)
            transform.localScale = Vector3.one;
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);


        // set animator paramters
        anim.SetBool("walk1", horizontalInput != 0);
        anim.SetBool("grounded", isGrounded());


        //wall jump logic
        if (wallJumpCooldown > 0.2f)
        {
            body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

            if (onWall() && !isGrounded())
            {
                body.gravityScale = 0;
                body.velocity = Vector2.zero;
            }
            else
                body.gravityScale = 5;

            if (Input.GetKey(KeyCode.UpArrow))
                Jump();
        }
        else
            wallJumpCooldown += Time.deltaTime;

        //crouch
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            crouch1 = false;
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            crouch1 = true;
        }
        anim.SetBool("crouch1", crouch1);

        //block
        //if (Input.GetKeyUp(KeyCode.Q))
        //{
        //    block = false;
        //}
        //if (Input.GetKeyDown(KeyCode.Q))
        //{
        //    block = true;
        //}
        //anim.SetBool("block", block);

        
        if (Input.GetKeyUp(KeyCode.M))
        {
            strike1 = false;
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            strike1 = true;
        }
        anim.SetBool("strike1", strike1);

        //win
        if (Input.GetKeyUp(KeyCode.Alpha5))
        {
            win1 = false;
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            win1 = true;
        }
        anim.SetBool("win1", win1);

        //die
        if (Input.GetKeyUp(KeyCode.Alpha6))
        {
            die1 = false;
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            die1 = true;
        }
        anim.SetBool("die1", die1);

        //hurt
        if (Input.GetKeyUp(KeyCode.Alpha7))
        {
            hurt1 = false;
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            hurt1 = true;
        }
        anim.SetBool("hurt1", hurt1);

        //dizzy
        if (Input.GetKeyUp(KeyCode.Alpha8))
        {
            dizzy1 = false;
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            dizzy1 = true;
        }
        anim.SetBool("dizzy1", dizzy1);

        //dash
        if (Input.GetKeyDown(KeyCode.P))
        {
            anim.SetTrigger("flykick1");
        }


        if (Input.GetMouseButtonDown(1))
        {
            Attack1();
        }
           



    }
    private void Attack1()
    {
        anim.SetTrigger("attack1");

        GameObject fireball = fireballs[FindFireball()];
        fireball.transform.position = firePoint.position;
        fireball.GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
    }

    private void Jump()
    {
        if (isGrounded())
        {
            body.velocity = new Vector2(body.velocity.x, jumpPower);
            anim.SetTrigger("jump1");
        }
        else if (onWall() && !isGrounded())
        {
            if (horizontalInput == 0)
            {
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 10, 0);
                transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 3, 6);

            wallJumpCooldown = 0;

        }
    }

    private int FindFireball()
    {
        for (int i = 0; i < fireballs.Length; i++)
        {
            if (!fireballs[i].activeInHierarchy)
                return i;
        }
        return 0;
    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }

    public bool canAttack()
    {
        return horizontalInput == 0 && isGrounded() && !onWall();
    }
}
