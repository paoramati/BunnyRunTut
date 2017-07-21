using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BunnyController : MonoBehaviour {

    private Rigidbody2D myRigidBody;
    private Animator myAnimator;

    public float bunnyJumpForce = 500f;

	// Use this for initialization
	void Start () {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonUp("Jump"))
        {
            myRigidBody.AddForce(transform.up * bunnyJumpForce);
        }

        myAnimator.SetFloat("vVelocity", myRigidBody.velocity.y);
	}

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Enemy")) 
            SceneManager.LoadScene("Scene1");        
    }
}
