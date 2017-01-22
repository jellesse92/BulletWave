using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundBullet : MonoBehaviour {

    const float INVOKE_DAMAGE_RATE = .5f;

    HashSet<GameObject> enemysInRange = new HashSet<GameObject>();

    public int damage = 5;
    public float lifeTime = 1f;
    int playerColor = 0;

    bool waitingForFirst = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            ApplyDamage(collision.gameObject);
            collision.GetComponent<Enemy>().TakeDamage(damage);
            if (!enemysInRange.Contains(collision.gameObject))
                enemysInRange.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
            if (enemysInRange.Contains(collision.gameObject))
                enemysInRange.Remove(collision.gameObject);
    }

    private void Reset()
    {
        enemysInRange = new HashSet<GameObject>();
    } 

    public void Initialize(int c)
    {
        playerColor = c;
        InvokeRepeating("ApplyDamageAll", 0f, INVOKE_DAMAGE_RATE);
        Invoke("Deactivate", lifeTime);

    }

    void ApplyDamageAll()
    {
        foreach (GameObject target in enemysInRange)
        {
            ApplyDamage(target);
        }
    }

    void ApplyDamage(GameObject target)
    {
        target.GetComponent<Enemy>().TakeDamage(DamageMultiplier(target.GetComponent<Enemy>().projectileWaveType) * damage);
    }

    void Deactivate()
    {
        CancelInvoke("ApplyDamage");
        gameObject.SetActive(false);
    }

    int DamageMultiplier(int color)
    {
        if ((color == 0 && playerColor == 2) || (color == 1 && playerColor == 0) || (color == 2 && playerColor == 1))
            return 3;
        if (color == playerColor)
            return 1;
        return 2;
    }

    
}
