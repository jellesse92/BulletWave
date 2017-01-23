using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ApproachEnemy : Enemy {
    
    // Use this for initialization
    protected override void EnemySpecificStart()
    {
        inAttackRange = false;

    }

    void Start()
    {
        anim = GetComponentInParent<Animator>();
    }
    void Awake()
    {
        bulletType = 0;
        damage = 10;
        inAttackRange = false;
        target = FindTarget();
    }
    // Update is called once per frame
    void FixedUpdate() {

        if (inAttackRange)
        {
            Shoot();
            Retreat();

        }
        else {
            Approach();
        }

        CheckForDeath();
    }
    protected new void Retreat()
    {
        var heading = target.transform.position - transform.position;
        var direction = heading / heading.magnitude;
        if (!isMovementLock)
        {
            transform.parent.transform.Translate(-direction * Time.deltaTime * speed);
        }
        float angleFromVector = (float)Mathf.Atan2(direction.y, direction.x);
        angleFromVector = angleFromVector < 0 ? 6.3f + angleFromVector : angleFromVector;
        if (anim != null)
            anim.SetFloat("radDirection", angleFromVector);
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

    protected new void Approach()
    {
        var heading = target.transform.position - transform.position;
        var direction = heading / heading.magnitude;
        if (!isMovementLock)
        {
            transform.parent.transform.Translate(direction * Time.deltaTime * speed);
        }
        float angleFromVector = (float)Mathf.Atan2(direction.y, direction.x);
        angleFromVector = angleFromVector < 0 ? 6.3f + angleFromVector : angleFromVector;
        if(anim != null)
            anim.SetFloat("radDirection", angleFromVector);
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
