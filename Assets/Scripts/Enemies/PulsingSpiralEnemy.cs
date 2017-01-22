using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulsingSpiralEnemy : Enemy {

    public bool isLeader;
    public GameObject iAmFollowing;

    public float idleSpeed = .9f;
    private float idleCircleSize = .15f;
 

	// Use this for initialization
	void Start () {
        
	}
	
    void Awake()
    {
        target = FindTarget();
    }
	// Update is called once per frame
	void FixedUpdate () {
        float distance = Vector2.Distance(target.transform.position, transform.position);
        if (!inAggroRadius)
        {
            IdleMovement();
        } else if (inAggroRadius && !inAttackRange)
        {

        } else if (inAttackRange)
        {

        }
	}

    protected new void IdleMovement()
    {
        if(isLeader)
        {
            Orbit();
        } else
        {
            var heading = iAmFollowing.transform.position - transform.position;
            var direction = heading / heading.magnitude;
            transform.Translate(direction * Time.deltaTime * (speed + 1));

        }
    }


    protected void Orbit()
    {
        if (!isMovementLock)
        {
            transform.RotateAround(target.transform.position, Vector3.forward, 20 * Time.deltaTime);
        }
    }
}
