using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiralEnemy : Enemy {

    public float idleSpeed = .9f;
    private float idleCircleSize = .05f;
    private float aggroRadius = 30f;
   

    // Use this for initialization
    void Start () {
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
	void Update () {
		
	}
}
