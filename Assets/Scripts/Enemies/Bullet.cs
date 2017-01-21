using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public GameObject bullet;
    public float speed;
    public Vector2 direction;
    public float damage;
    public int type; // 0 = fireball, 1 = bullet
    public int color; // 0 = r, 1 = g, 2 = b

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        bullet.transform.Translate(direction * Time.deltaTime * speed);
	}
}
