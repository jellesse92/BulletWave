using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApproachEnemy : Enemy {

	// Use this for initialization
	void Awake()
    {
        inAttackRange = true;
        target = FindTarget();
    }
	// Update is called once per frame
	void FixedUpdate () {
        float distance = Vector2.Distance(target.transform.position, transform.position);

        if (distance <= attackRadius)
        {
            Shoot();
            if (distance < attackRadius)
            {
                Retreat();
            }
        }
        else {
            Approach();
        }
	}


}
