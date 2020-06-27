﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2D : MonoBehaviour
{
    public GameObject WeaponPrefab;
    
    private Animator animator;
    private Rigidbody2D rb2d;

    // states
    public static bool isFacingRight;
    public static bool isGrounded;

    [SerializeField]
    Transform groundCheck;

    [SerializeField]
    public float runSpeed;

    [SerializeField]
    public float jumpSpeed;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();

        isFacingRight = true;
    }

    void Update()
    {
        // jump logic
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            // jumpSpeed value is originally 3
            Vector2 newForce = new Vector2(rb2d.velocity.x, jumpSpeed);
            rb2d.AddForce(newForce);
            animator.Play("Player_jump");
        }

        // move player horizontally based on input
        float horizontalTranslate = Input.GetAxis("Horizontal");
        if (horizontalTranslate == 1)  // right button is pressed
        {
            // rb2d.AddForce(new Vector2(runSpeed, rb2d.velocity.y));
            rb2d.velocity = new Vector2(runSpeed, rb2d.velocity.y);
            if (isGrounded)
            {
                animator.Play("Player_run");
            }

            // rotate player and gun based on change in direction
            if (!isFacingRight)
            {
                isFacingRight = true;
                transform.Rotate(0f, 180f, 0f);  // rotating transform instead of flipping spriteRenderer would change the coordinate system of the child elements
            }
        }
        else if (horizontalTranslate == -1)  // left button is pressed
        {
            rb2d.velocity = new Vector2(-runSpeed, rb2d.velocity.y);
            if (isGrounded)
            {
                animator.Play("Player_run");
            }

            // rotate player and gun based on change in direction
            if (isFacingRight)
            {
                isFacingRight = false;
                transform.Rotate(0f, 180f, 0f);
            }
        }
        else
        {
            animator.Play("Player_idle");
        }
    }

    private void FixedUpdate()
    {
        if (Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground")))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }
}
