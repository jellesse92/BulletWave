using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {



    public bool isFacingRight;
    public float speed = 1f;
    public float attackRadius = 10f;
    public int health;
    public int ammo = 10;
    public int projectileWaveType; // 0 - 2...    || 0 = r || 1 = g  || 2 = b
    public GameObject[] Bullets;

    protected Animator anim;
    protected bool isDead;
    protected bool inAttackRange;
    protected bool isCoolingDown;

    protected float coolDownTime;
    protected GameObject target;
    protected GameObject[] potentialTargets;
    protected GameObject[] bullets;

    void Start()
    {
        potentialTargets = GameObject.FindGameObjectsWithTag("Player");
        inAttackRange = false;
    }
    void Awake()
    {
        for (int i = 0; i < ammo; i++)
        {
            //bullets[i] = 
        }
        isFacingRight = false;
        inAttackRange = false;
        InvokeRepeating("FlipDirection", 3f, 5f);
    }

    protected GameObject FindTarget()
    {
        GameObject closestObject = potentialTargets[0];
        float distance = Vector2.Distance(transform.position, potentialTargets[0].transform.position);
        foreach (GameObject player in potentialTargets)
        {
            closestObject = Vector2.Distance(transform.position, closestObject.transform.position) > Vector2.Distance(transform.position, player.transform.position) ? player : closestObject;
        }
        return closestObject;
    }

    void FixedUpdate()
    {
        if (!inAttackRange)
        {
            IdleMovement();
        }

    }

    protected void IdleMovement()
    {
        gameObject.transform.Translate(Time.deltaTime * (isFacingRight ? 1 : -1) * speed , 0, 0);
    }

    protected void Shoot() {
        var heading = target.transform.position - transform.position;
        var direction = heading / heading.magnitude;



    }

    protected void TakeDamage(int dmg) {
        health -= dmg;
    }
    protected void Approach() { }
    protected void Retreat() { }
    protected void FlipDirection()
    {
        isFacingRight = !isFacingRight;
    }
    
}
