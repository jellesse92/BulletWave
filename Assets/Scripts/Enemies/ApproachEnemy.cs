using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApproachEnemy : Enemy {

    // Use this for initialization
    void Start()
    {
        inAttackRange = false;
    }

    void Awake()
    {
        bulletType = 0;
        damage = 10;
        inAttackRange = true;
        target = FindTarget();
    }
	// Update is called once per frame
	void FixedUpdate () {
        float distance = Vector2.Distance(target.transform.position, transform.position);

        if (distance <= attackRadius)
        {
            Shoot();

        }
        else {
            Approach();
        }

        CheckForDeath();
	}
    protected new void Retreat()
    {
        if (!isMovementLock)
        {
            var heading = target.transform.position - transform.position;
            var direction = heading / heading.magnitude;
            transform.Translate(-direction * Time.deltaTime * speed);
        }
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

    protected new void Approach()
    {
        if (!isMovementLock)
        {
            var heading = target.transform.position - transform.position;
            var direction = heading / heading.magnitude;
            transform.Translate(direction * Time.deltaTime * speed);
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


}
