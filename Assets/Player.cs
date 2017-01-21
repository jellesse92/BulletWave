using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    const float INVULN_DURATION = 1.5f;

    const int BASE_LIVES = 3;
    const int BASE_HEALTH = 100;

    //Stats
    int health = BASE_HEALTH;
    int lives = BASE_LIVES;

    bool invulnerable = false;

	// Use this for initialization
	void Start () {
        TakeDamage(10);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void TakeDamage(int damage)
    {
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
        }

    }

    

    void Revive()
    {
        health = BASE_HEALTH;
    }

    void Death()
    {

    }

    IEnumerator TakeDamageFlash()
    {
        float r = GetComponent<SpriteRenderer>().color.r;
        float b = GetComponent<SpriteRenderer>().color.b;
        float g = GetComponent<SpriteRenderer>().color.g;

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
}
