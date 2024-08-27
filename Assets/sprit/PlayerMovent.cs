using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class playerMovement : MonoBehaviour
{

    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;


    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private float wallJumpCooldown;
    private float horizontalInput;
    private bool crouch;
    private bool block;
    private bool strike;
    private bool win;
    private bool die;
    private bool hurt;
    private bool dizzy;
    private void Awake()
    {
        //Grab references for rigibody and animator from object
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        //flip player when moving left.right
        if (horizontalInput > 0.01f)
            transform.localScale = Vector3.one;
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);

        // set animator paramters
        anim.SetBool("walk", horizontalInput != 0);
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

            if (Input.GetKey(KeyCode.Space))
                Jump();
            if (Input.GetKey(KeyCode.W))
                Jump();
        }
        else
            wallJumpCooldown += Time.deltaTime;

        //crouch
        if (Input.GetKeyUp(KeyCode.S))
        {
            crouch = false;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            crouch = true;
        }
        anim.SetBool("crouch", crouch);

        //block
        if (Input.GetKeyUp(KeyCode.Q))
        {
            block = false;
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            block = true;
        }
        anim.SetBool("block", block);

        //strike
        //if (Input.GetMouseButtonUp(1))
        //{
        //    strike = false;
        //}

        //if (Input.GetMouseButtonDown(1))
        //{
        //    strike = true;
        //}
        //anim.SetBool("strike", strike);

        if (Input.GetKeyDown(KeyCode.V))
        {
            anim.SetTrigger("strike1");
        }


        //win
        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            win = false;
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            win = true;
        }
        anim.SetBool("win", win);

        //die
        if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            die = false;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            die = true;
        }
        anim.SetBool("die", die);

        //hurt
        if (Input.GetKeyUp(KeyCode.Alpha3))
        {
            hurt = false;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            hurt = true;
        }
        anim.SetBool("hurt", hurt);

        //dizzy
        if (Input.GetKeyUp(KeyCode.Alpha4))
        {
            dizzy = false;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            dizzy = true;
        }
        anim.SetBool("dizzy", dizzy);

        //dash
        if (Input.GetKeyDown(KeyCode.L))
        {
            anim.SetTrigger("dash");
        }

        //cast
        if (Input.GetKeyDown(KeyCode.F))
        {
            anim.SetTrigger("cast");
        }

        //jumpattack
        if (Input.GetMouseButtonDown(1))
        {
            anim.SetTrigger("jumpattack");
        }
    }


    private void Jump()
    {
        if (isGrounded())
        {
            body.velocity = new Vector2(body.velocity.x, jumpPower);
            anim.SetTrigger("jump");
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