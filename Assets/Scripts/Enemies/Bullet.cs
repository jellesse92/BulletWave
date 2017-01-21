using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float speed;
    public Vector2 direction;
    public bool deflected;
    public int damage;
    public int type = 1; // 0 = fireball, 1 = bullet
    public int color; // 0 = r, 1 = g, 2 = b



    // Use this for initialization
    void Start()
    {
        deflected = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (type == 0)
        {
            transform.Translate(new Vector2 ((direction.x * Time.deltaTime * speed * Mathf.Sin(Time.time)), (direction.y * Time.deltaTime * speed * Mathf.Cos(Time.time))));

        }
        if (type == 1)
        {
            transform.Translate(direction * Time.deltaTime * speed);
        }

        if (!gameObject.GetComponent<SpriteRenderer>().isVisible)
        {
            Invoke("Deactivate", 2f);
        }
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }
    public void Deflect()
    {
        direction = direction * -1;
        deflected = true;
    }

}
