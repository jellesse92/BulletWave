using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    private const int waveTypes = [0, 1, 2];

    public bool isFacingRight;
    public float speed = 1f;
    public float attackRadius = 10f;
    public int health;
    public int projectileWaveType;

    protected Animator anim;
    protected bool isDead;
    protected bool inAttackRange;

    protected float coolDownTime;
    protected GameObject target;
    protected GameObject[] potentialTargets;

    void Start()
    {
        potentialTargets = GameObject.FindGameObjectsWithTag("Player");
    }
    void Awake()
    {
        isFacingRight = false;
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

    protected void FlipDirection()
    {
        isFacingRight = !isFacingRight;
    }
    
}
