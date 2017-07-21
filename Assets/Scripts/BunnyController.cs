﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BunnyController : MonoBehaviour {

    private Rigidbody2D myRigidBody;
    private Animator myAnim;
    private Collider2D myCollider;
    public float bunnyJumpForce = 500f;
    private float bunnyHurtTime = -1;
    public Text scoreText;
    private float startTime;

	// Use this for initialization
	void Start () {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        myCollider = GetComponent<Collider2D>();

        startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {

        if (bunnyHurtTime == -1)
        {
            if (Input.GetButtonUp("Jump"))
            {
                myRigidBody.AddForce(transform.up * bunnyJumpForce);
            }

            myAnim.SetFloat("vVelocity", myRigidBody.velocity.y);

            scoreText.text = (Time.time - startTime).ToString("0.0");
        }
        else
        {
            if (Time.time > bunnyHurtTime + 2) //i.e. 2 seconds after collision
            {
                SceneManager.LoadScene("Scene1");
            }
        }

		
	}

    //collision detection method
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            foreach (PrefabSpawner spawner in FindObjectsOfType<PrefabSpawner>())  //disables newly spawned enemy from moving after collision
            {
                spawner.enabled = false;
            }

            foreach (MoveLeft moveLefter in FindObjectsOfType<MoveLeft>())          //disables enemy from moving after collision
            {
                moveLefter.enabled = false;
            }

            bunnyHurtTime = Time.time;
            myAnim.SetBool("bunnyHurt", true);
            myRigidBody.velocity = Vector2.zero;    //reset object velocity
            myRigidBody.AddForce(transform.up * bunnyJumpForce);    //make bunny shoot up
            myCollider.enabled = false;
        }            
    }
}
