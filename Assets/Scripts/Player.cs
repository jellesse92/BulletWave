using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public PlayerSpecialEffects playerSpecialEffects;

    const float SHAKE_MAGNITUDE = .1f;
    const float SHAKE_DURATION = .5f;

    const float REVIVE_TIME = 1f;

    const float INVULN_DURATION = 1.5f;

    const int BASE_LIVES = 3;
    const int BASE_HEALTH = 100;

    //Stats
    public int color = 0;
    int health = BASE_HEALTH;
    int lives = BASE_LIVES;

    bool invulnerable = false;
    bool isDead = false;

	// Use this for initialization
	void Start () {

        Invoke("Test", 4f);
	}

    void Test()
    {
        Debug.Log("running test");
        if (!gameObject.activeSelf)
            return;
        Death();
        //TakeDamage(10);
    }
	
	// Update is called once per frame
	void Update () {
        float x = Input.GetAxis("1_RightJoystickX");
        float y = Input.GetAxis("1_RightJoystickY");



    }

    public void TakeDamage(int damage, GameObject bullet = null)
    {
        if(bullet != null)
        {
            //Remove from deflector script
        }

        if (invulnerable)
            return;

        health -= damage;
        if(health <= 0)
        {
            Death();
            return;
        }
        else
        {
            StartCoroutine("TakeDamageFlash");
            playerSpecialEffects.StartShake(SHAKE_MAGNITUDE, SHAKE_DURATION);
        }
    }
    
    void Revive()
    {
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<Collider2D>().enabled = true;
        isDead = false;
        health = BASE_HEALTH;
        transform.position = transform.parent.position;
    }

    void Death()
    {
        isDead = true;
        lives--;
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;

        if(lives < 0)
        {
            Debug.Log("FINAL DEATH PARTICLE");
        }
        else
        {
            Invoke("Revive", REVIVE_TIME);
        }
    }

    IEnumerator TakeDamageFlash()
    {
        float r = GetComponent<SpriteRenderer>().color.r;
        float b = GetComponent<SpriteRenderer>().color.b;
        float g = GetComponent<SpriteRenderer>().color.g;

        invulnerable = true;

        Invoke("CancelInvuln", INVULN_DURATION);

        AlphaColorShift(.2f);
        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(.15f);
            AlphaColorShift(1f);
            yield return new WaitForSeconds(.15f);
            AlphaColorShift(.3f);
            yield return new WaitForSeconds(.15f);
        }
    }

    void AlphaColorShift(float alpha)
    {
        GetComponent<SpriteRenderer>().color = new Color(GetComponent<SpriteRenderer>().color.r, GetComponent<SpriteRenderer>().color.g,
        GetComponent<SpriteRenderer>().color.b, alpha);
    }

    void CancelInvuln()
    {
        invulnerable = false;
        AlphaColorShift(1f);
    }

    public bool DeathStatus()
    {
        return isDead;
    }
}
