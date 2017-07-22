﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabSpawner : MonoBehaviour {

    private float nextSpawn = 0;        //holds next point in time for next spawning
    public Transform prefabToSpawn;     //reference to the prefab to be spawned
    //public float spawnRate = 1;         //time is sec between spawning prefabs
    //public float randomDelay = 1;
    public AnimationCurve spawnCurve;
    public float curveLengthInSeconds = 30f;
    private float startTime;

	// Use this for initialization
	void Start () {
        startTime = Time.time;  //set to current time of the game
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time > nextSpawn)
        {
            Instantiate(prefabToSpawn, transform.position, Quaternion.identity);
            //nextSpawn = Time.time + spawnRate + Random.Range(0, randomDelay);

            float curvePos = (Time.time - startTime) / curveLengthInSeconds;    //how many secs have passed / 30
            //if curvePos goes past the edge of the animation curve.
            if (curvePos > 1f)
            {
                curvePos = 1f;
                startTime = Time.time;
            }

            nextSpawn = Time.time + spawnCurve.Evaluate(curvePos);
        }
	}
}
