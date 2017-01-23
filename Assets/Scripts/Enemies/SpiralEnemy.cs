using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiralEnemy : Enemy {

    public float idleSpeed = .0001f;
    private float idleCircleSize = .05f;

    void Start()
    {
        anim = GetComponentInParent<Animator>();
    }
    // Use this for initialization
    protected override void EnemySpecificStart()
    {
        inAttackRange = false;
        inAggroRadius = false;
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
        if (!inAttackRange && !inAggroRadius)
        {
            idleSpeed += .0001f;
            ApproachIdle();
        } else if (!inAttackRange && inAggroRadius)
        {
            Approach();
        } if (inAttackRange)
        {
            Shoot();
            Retreat();
        }
    }

    protected void ApproachIdle()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, idleSpeed);
    }
    protected new void Approach()
    {
        var heading = target.transform.position - transform.position;
        var direction = heading / heading.magnitude;
        if (!isMovementLock)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed / 3);
            Orbit();
        }
        float angleFromVector = (float)Mathf.Atan2(direction.y, direction.x);
        angleFromVector = angleFromVector < 0 ? 6.3f + angleFromVector : angleFromVector;
        if (anim != null)
            anim.SetFloat("radDirection", angleFromVector);
    }

    protected new void Retreat()
    {
        var heading = target.transform.position - transform.position;
        var direction = heading / heading.magnitude;
        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, -speed/1000);
        float angleFromVector = (float)Mathf.Atan2(direction.y, direction.x);
        angleFromVector = angleFromVector < 0 ? 6.3f + angleFromVector : angleFromVector;
        if (anim != null)
            anim.SetFloat("radDirection", angleFromVector);
        Orbit();
    }

    protected void Orbit()
    {
        Vector3 rotationMask = new Vector3(0, 0, 1);
        if (!isMovementLock)
        {
            transform.RotateAround(target.transform.position, rotationMask, 20 * Time.deltaTime);
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
            BulletList.GetComponentInParent<BulletFire>().Fire(direction, transform.position, Random.Range(2f, 5f), projectileWaveType, bulletType, damage);
            isCoolingDown = true;
            isMovementLock = true;
            Invoke("CoolDownShot", coolDownTime);
            Invoke("CoolDownWalk", movementCoolDownTime);
        }
    }
}

