using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public GameObject bullet;
    public float speed;
    public Vector2 direction;
    public bool deflected;
    public float damage;
    public int type = 1; // 0 = fireball, 1 = bullet
    public int color; // 0 = r, 1 = g, 2 = b

    public float frequency = 20.0f;  // Speed of sine movement
    public float magnitude = 0.5f;   // Size of sine movement
    private Vector2 axis;
    private Vector2 pos;

    // Use this for initialization
    void Start()
    {
        deflected = false;
        pos = transform.position;
        axis = transform.right;  // May or may not be the axis you want
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (type == 0)
        {
            bullet.transform.Translate(direction * Time.deltaTime * speed);
            transform.position = pos + axis * Mathf.Sin(Time.time * frequency) * magnitude;
        }
        if (type == 1)
        {
            bullet.transform.Translate(direction * Time.deltaTime * speed);
        }
    }

    public void Deflect()
    {
        direction = direction * -1;
        deflected = true;
    }

}
