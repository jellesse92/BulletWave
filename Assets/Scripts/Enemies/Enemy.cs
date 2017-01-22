using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {



    public bool isFacingRight;
    public float speed = 1f;
    public float attackRadius = 10f;
    public float movementCoolDownTime;
    public int damage;
    public int health;
    public int ammo = 10;
    public int projectileWaveType; // 0 - 2...    || 0 = r || 1 = g  || 2 = b
    public int bulletType;
    public GameObject BulletList;

    protected Animator anim;
    protected bool isDead;
    protected bool inAttackRange;
    protected bool isMovementLock;
    protected bool isCoolingDown;

    protected float coolDownTime;
    protected GameObject target;
    protected GameObject[] potentialTargets;

    void Start()
    {
         inAttackRange = false;
    }
    void Awake()
    {

        isFacingRight = false;
        inAttackRange = false;
        InvokeRepeating("FlipDirection", 3f, 5f);
    }

    protected GameObject FindTarget()
    {
        potentialTargets = GameObject.FindGameObjectsWithTag("Player");
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

    }

    public void TakeDamage(int dmg) {
        health -= dmg;
    }
    
    protected void Approach() { }
    protected void Retreat() { }
    protected void FlipDirection()
    {
        isFacingRight = !isFacingRight;
    }
    
    protected void CheckForDeath()
    {
        //Death Animation?
        if (health <= 0)
        {
            isDead = true;
            gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter2d(Collider2D col)
    {
        if (col.tag == "Bullet")
        {
            var b = col.GetComponent<Bullet>();
            if (b.deflected)
            {
                TakeDamage(b.damage);
            }
        }
    }
}
