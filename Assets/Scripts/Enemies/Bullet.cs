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
    public int counter = 0;
    public float startTime;

    GameObject redParticle;
    GameObject blueParticle;
    GameObject greenParticle;
    GameObject explosion;

    // Use this for initialization
    void Awake()
    {
        getParticle();
    }
    void Start()
    {
        deflected = false;
        startTime = Time.time;
    }

    void getParticle()
    {
        foreach (Transform child in transform)
        {
            if (child.name == "RedParticle")
                redParticle = child.gameObject;
            else if (child.name == "BlueParticle")
                blueParticle = child.gameObject;
            else if (child.name == "GreenParticle")
                greenParticle = child.gameObject;
            else if (child.name == "Explosion")
                explosion = child.gameObject;
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (type == 0)
        {
            // this code snippet makes butterflies. not sure why.
            //transform.Rotate(new Vector2 (Mathf.Sin((Time.time - startTime + 1.3f) * 1) * 10, 0));
            transform.Translate(new Vector2 (0, Mathf.Sin(Time.time*speed)*0.05f));
            transform.Translate(direction * Time.deltaTime * speed);
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

    public void changeColor(string color)
    {
        //getParticle();
        if (color == "red" || color == "Red")
        {
            redParticle.SetActive(true);
            blueParticle.SetActive(false);
            greenParticle.SetActive(false);
        }
        else if (color == "blue" || color == "Blue")
        {
            redParticle.SetActive(false);
            blueParticle.SetActive(true);
            greenParticle.SetActive(false);
        }
        else if (color == "green" || color == "Green")
        {
            redParticle.SetActive(false);
            blueParticle.SetActive(false);
            greenParticle.SetActive(true);
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

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (!deflected && col.tag == "Player")
        {
            if (redParticle.activeSelf)
            {
                redParticle.SetActive(false);
                foreach (Transform child in explosion.transform)
                {
                    if (child.name == "Red")
                    {
                        child.position = col.transform.position;
                        child.GetComponent<ParticleSystem>().Play();
                    }
                }
            }
            if (blueParticle.activeSelf)
            {
                blueParticle.SetActive(false);
                foreach (Transform child in explosion.transform)
                {
                    if (child.name == "Blue")
                    {
                        child.position = col.transform.position;
                        child.GetComponent<ParticleSystem>().Play();
                    }
                }
            }
            if (greenParticle.activeSelf)
            {
                greenParticle.SetActive(false);
                foreach (Transform child in explosion.transform)
                {
                    if (child.name == "Green")
                    {
                        child.position = col.transform.position;
                        child.GetComponent<ParticleSystem>().Play();
                    }
                }
            }
            col.GetComponent<Player>().TakeDamage(damage, this.gameObject);
        }
    }
}
