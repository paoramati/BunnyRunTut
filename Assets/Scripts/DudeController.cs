using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DudeController : MonoBehaviour
{

    private Rigidbody2D myRigidBody;
    private Animator myAnim;
    private Collider2D myCollider;
    public float dudeJumpForce = 500f;
    private float dudeHurtTime = -1;
    public Text scoreText;
    private float startTime;
    private int jumpsLeft = 2;
    public AudioSource jumpSfx;
    public AudioSource deathSfx;
    public AudioSource backgroundMusic;

    // Use this for initialization
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        myCollider = GetComponent<Collider2D>();

        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Title");
        }

        if (dudeHurtTime == -1)
        {
            if ((Input.GetButtonUp("Jump") || Input.GetButtonUp("Fire1")) && jumpsLeft > 0)
            {
                if (myRigidBody.velocity.y < 0)     //to make second jump ignore negative y vector
                {
                    myRigidBody.velocity = Vector2.zero;
                }

                //making second jump less powerful so dude doesn't go off top of screen
                if (jumpsLeft == 1)
                {
                    myRigidBody.AddForce(transform.up * dudeJumpForce * 0.75f); //if doing second jump, jump has less force
                }
                else
                {
                    myRigidBody.AddForce(transform.up * dudeJumpForce);
                }

                jumpsLeft--;
                jumpSfx.Play();
            }

            myAnim.SetFloat("vVelocity", myRigidBody.velocity.y);

            scoreText.text = (Time.time - startTime).ToString("0.0");
        }
        else
        {
            if (Time.time > dudeHurtTime + 3) //i.e. 2 seconds after collision
            {
                //SceneManager.UnloadSceneAsync("Game");
                SceneManager.LoadScene("Title");
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

            dudeHurtTime = Time.time;
            myAnim.SetBool("dudeHurt", true);
            myRigidBody.velocity = Vector2.zero;    //reset object velocity
            myRigidBody.AddForce(transform.up * dudeJumpForce);    //make dude shoot up
            myCollider.enabled = false;
            backgroundMusic.Pause();
            deathSfx.Play();
            
        }
        else if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            jumpsLeft = 2;
        }
    }
}
