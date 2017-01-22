using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiralEnemy : Enemy {

    public float idleSpeed = .0001f;
    private float idleCircleSize = .05f;
    private float aggroRadius = 20f;


    // Use this for initialization
    void Start() {
        inAttackRange = false;
    }

    void Awake()
    {
        bulletType = 1;
        movementCoolDownTime = 1f;
        damage = 10;
        inAttackRange = true;
        target = FindTarget();
    }

    void FixedUpdate(){

        float distance = Vector2.Distance(target.transform.position, transform.position);
        if (distance > aggroRadius)
        {
            idleSpeed += .0001f;
            ApproachIdle();
        } else if (distance < aggroRadius && distance > attackRadius)
        {
            ApproachIdle();
            Orbit();
        } if (distance <= attackRadius)
        {
            Shoot();
            if (distance < attackRadius)
            {
                Retreat();
            }
        }
    }

    protected void ApproachIdle()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, idleSpeed);
    }
    protected new void Approach()
    {
        if (!isMovementLock)
        {
            var heading = target.transform.position - transform.position;
            var direction = heading / heading.magnitude;
            transform.Translate(direction * Time.deltaTime * speed);
            Orbit();
        }
    }

    protected new void Retreat()
    {
        if (!isMovementLock)
        {
            var heading = target.transform.position - transform.position;
            var direction = heading / heading.magnitude;
            transform.Translate(-direction * Time.deltaTime * speed);
            Orbit();
        }
    }

    protected void Orbit()
    {
        if (!isMovementLock)
        {
            transform.RotateAround(target.transform.position, Vector3.forward, 20 * Time.deltaTime);
        }
    }

    private void CoolDownShot()
    {
        isCoolingDown = false;
    }
    private void CoolDownWalk()
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
            Invoke("CoolDownShot", coolDownTime);
            Invoke("CoolDownWalk", movementCoolDownTime);
        }
    }
}

