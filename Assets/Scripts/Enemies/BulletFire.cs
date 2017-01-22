using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFire : MonoBehaviour {

    public GameObject bullet;
    public int ammo;
    private List<GameObject> bulletList;

	// Use this for initialization
	void Start () {
        bulletList = new List<GameObject>();
        for(int i = 0; i < ammo; i++)
        {
            GameObject b = (GameObject)Instantiate(bullet);
            b.transform.parent = transform;
            b.SetActive(false);
            bulletList.Add(b);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Fire(Vector2 dir, Vector3 pos, float speed, int color, int type, int dmg)
    {
        for (int i = 0; i < ammo; i++)
        {
            if (!bulletList[i].activeInHierarchy)
            {
                Bullet b = bulletList[i].GetComponent<Bullet>();
                b.direction = dir;
                b.color = color;
                if (b.color == 0)
                {
                    b.GetComponent<SpriteRenderer>().material.SetColor("_Color", Color.red);
                    b.changeColor("red");
                }
                if (b.color == 1)
                {
                    b.GetComponent<SpriteRenderer>().material.SetColor("_Color", Color.green);
                    b.changeColor("green");
                }
                if (b.color == 2)
                {
                    b.GetComponent<SpriteRenderer>().material.SetColor("_Color", Color.blue);
                    b.changeColor("blue");
                }
                b.type = type;
                b.damage = dmg;
                b.speed = speed;
                b.transform.position = pos;
                b.deflected = false;
                bulletList[i].SetActive(true);
                break;
            }
          
        }
    }
}
