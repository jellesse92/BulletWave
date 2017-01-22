using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulsingSpiralEnemy : Enemy {

    public bool isLeader;
    public GameObject iAmFollowing;
    public bool StopAllMovement = false;

    public float idleSpeed = .9f;
    private float idleCircleSize = .15f;


    // Use this for initialization
    protected override void EnemySpecificStart()
    {
        inAttackRange = false;
        isMovementLock = false;
	}
	
    void Awake()
    {
        target = FindTarget();
    }
	// Update is called once per frame
	void FixedUpdate () {
        float distance = Vector2.Distance(target.transform.position, transform.position);
        if (isMovementLock && !StopAllMovement)
        {
            Retreat();
        } else if (!inAggroRadius && !inAttackRange && !StopAllMovement)
        {
            if (isLeader)
            {
                Approach();
            } else if(iAmFollowing.activeSelf == false)
            {
                isLeader = true;
                Approach();
            } else
            {
                IdleMovement();
            }
        } else if (inAggroRadius && !inAttackRange && !isMovementLock && !StopAllMovement)
        {
            Orbit();
           
        } else if (inAttackRange && !isMovementLock && !StopAllMovement)
        {
            Shoot();
            Retreat();
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

        Vector3 rotationMask = new Vector3(0, 0, 1);
        if (!isMovementLock)
        {
            transform.RotateAround(target.transform.position, rotationMask,  20 * Time.deltaTime);
        }
    }

    protected new void Approach()
    {
        if (!isMovementLock)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed/ 3);
            Orbit();
        }
    }

    protected new void Retreat()
    {
        var heading = target.transform.position - transform.position;
        var direction = heading / heading.magnitude;
        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, -speed);
    }
    private void CoolDownShot()
    {
        isCoolingDown = false;
    }
    private void CoolDownWalk()
    {
        StopAllMovement = false;
        Invoke("StopWalkCooldown", movementCoolDownTime);
    }

    private void StopAllMoving()
    {
        StopAllMovement = true;
        Invoke("CoolDownWalk", 1f);
    }
    private void StopWalkCooldown()
    {
        isMovementLock = false;
    }
    protected new void Shoot()
    {
        if (!isCoolingDown)
        {
            coolDownTime = Random.Range(.5f, 1.5f);
            var heading = target.transform.position - transform.position;
            var direction = heading / heading.magnitude;
            BulletList.GetComponent<BulletFire>().Fire(direction, transform.position, Random.Range(2f, 5f), projectileWaveType, bulletType, damage);
            isCoolingDown = true;
            isMovementLock = true;
            float stopTime = 5f;
            Invoke("CoolDownShot", coolDownTime);
            StopAllMoving();
        }
    }
}
