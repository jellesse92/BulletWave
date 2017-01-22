using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDeflectorScript : MonoBehaviour {

    int bulletType;
    int bulletSpeed;
    int bulletDamage;
    int bulletColor;

    int playerColor = 0;

    HashSet<GameObject> bulletsInRange = new HashSet<GameObject>();
    bool active = false;

    private void Reset()
    {
        bulletsInRange = new HashSet<GameObject>();
    } 

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Bullet")
            if (active)
                ApplyDeflection(collision.gameObject);
            else if (!bulletsInRange.Contains(collision.gameObject))
                bulletsInRange.Add(collision.gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
            if (bulletsInRange.Contains(collision.gameObject))
                bulletsInRange.Remove(collision.gameObject);
    }

    public void RemoveBulletFromHash(GameObject bullet)
    {
        if (bulletsInRange.Contains(bullet))
            bulletsInRange.Remove(bullet);
        bulletsInRange = new HashSet<GameObject>();
    }

    public void ActivateDeflector()
    {
        active = true;
        foreach (GameObject bullet in bulletsInRange)
            ApplyDeflection(bullet);
        
    }

    public void DeactivateDeflector()
    {
        active = false;
    }

    public void SetDeflectColor(int color)
    {
        playerColor = color;
    }

    void ApplyDeflection(GameObject bullet)
    {
        Debug.Log("DEFLECT! THIS: " + bullet.name);
    }

}
