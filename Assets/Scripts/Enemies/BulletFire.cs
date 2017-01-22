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
                b.type = type;
                b.damage = dmg;
                b.speed = speed;
                b.transform.position = pos;
                bulletList[i].SetActive(true);
                break;
            }
          
        }
    }
}
