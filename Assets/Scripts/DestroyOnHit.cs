using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnHit : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D other)
    {
        //DestroyOnHit(other.gameObject);
        Destroy(other.gameObject);
        //DestroyOnHit;
    }
}
