using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabSpawner : MonoBehaviour {

    private float nextSpawn = 0;        //holds next point in time for next spawning
    public Transform prefabToSpawn;     //reference to the prefab to be spawned
    public float spawnRate = 1;         //time is sec between spawning prefabs
    public float randomDelay = 1;
    public AnimationCurve spawnCurve;
    public float curveLengthInSeconds = 30f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time > nextSpawn)
        {
            Instantiate(prefabToSpawn, transform.position, Quaternion.identity);
            nextSpawn = Time.time + spawnRate + Random.Range(0, randomDelay);
        }
	}
}
