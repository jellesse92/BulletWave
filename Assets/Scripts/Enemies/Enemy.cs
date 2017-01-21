using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {


    public bool isFacingRight;
    public float speed = 1f;
    public int health;

    protected Animator anim;
    protected bool isDead;
    protected bool inAttackRange;


    protected float coolDownTime;
    protected int projectileWaveType;
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
    void FixedUpdate()
    {
        IdleMovement();
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
