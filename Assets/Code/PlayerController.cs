﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour

{

    public float moveSpeed;
    private Animator anim;
    Rigidbody2D myRigidbody;
    bool playerMoving;
    Vector2 lastMove;
    RaycastHit2D hit;

    void Start()
    {
        anim = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Moving();
        Action();
    }

    void Moving()
    {
        playerMoving = false;

        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            playerMoving = true;

            myRigidbody.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * moveSpeed, myRigidbody.velocity.y);
            lastMove = new Vector2(Input.GetAxisRaw("Horizontal"), 0f);
        }
        else
            myRigidbody.velocity = new Vector2(0f, myRigidbody.velocity.y);

        if (Input.GetAxisRaw("Vertical") != 0)
        {
            playerMoving = true;

            myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, Input.GetAxisRaw("Vertical") * moveSpeed);
            lastMove = new Vector2(0f, Input.GetAxisRaw("Vertical"));

        }
        else
            myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, 0f);

        anim.SetFloat("moveX", Input.GetAxisRaw("Horizontal"));
        anim.SetFloat("moveY", Input.GetAxisRaw("Vertical"));
        anim.SetBool("playerMoving", playerMoving);
        anim.SetFloat("lastMoveX", lastMove.x);
        anim.SetFloat("lastMoveY", lastMove.y);
    }


    void Action()
    {
        if (Input.GetKeyDown("e"))
        {
            hit = Physics2D.Raycast(transform.position, lastMove, 1);
        
            PlayerInteraction interaction = hit.collider.gameObject.GetComponent<PlayerInteraction>();
            if (interaction != null)
                interaction.Trigger();

        }
    }
}