using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulsingSpiralEnemy : Enemy {

    public bool isLeader;
    public GameObject iAmFollowing;

    public float idleSpeed = .9f;
    private float idleCircleSize = .1f;

	// Use this for initialization
	void Start () {
        
	}
	
    void Awake()
    {
        //
    }
	// Update is called once per frame
	void FixedUpdate () {
        IdleMovement();
	}

    protected new void IdleMovement()
    {
        if(isLeader)
        {
            var xPos = Mathf.Sin(Time.time * idleSpeed) * idleCircleSize;
            var yPos = Mathf.Cos(Time.time * idleSpeed) * idleCircleSize;
            var zPos = transform.position.z *  -1 *  Time.deltaTime;
            transform.Translate(xPos, yPos, zPos);
        } else
        {
            var heading = iAmFollowing.transform.position - transform.position;
            var direction = heading / heading.magnitude;
            transform.Translate(direction * Time.deltaTime * 4);

        }
    }
}
