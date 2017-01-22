using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner2 : MonoBehaviour {

    public GameObject senemy;
    public int senemyNum;
    public List<GameObject> senemyList;
    public bool isCoolingDown;
    public float coolDownTime;

    // Use this for initialization
    void Start()
    {
        senemyList = new List<GameObject>();
        for (int i = 0; i < senemyNum; i++)
        {
            GameObject b = (GameObject)Instantiate(senemy);
            b.SetActive(false);
            senemyList.Add(b);
        }
        isCoolingDown = false;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isCoolingDown)
        {
            coolDownTime = Random.Range(.5f, 1.5f);
            Spawn();
            isCoolingDown = true;
            Invoke("CoolDownSpawn", coolDownTime);
        }
    }

    private void CoolDownSpawn()
    {
        isCoolingDown = false;
    }

    public void Spawn()
    {
        for (int i = 0; i < senemyNum; i++)
        {
            if (!senemyList[i].activeInHierarchy)
            {
                SpiralEnemy b = senemyList[i].GetComponentInChildren<SpiralEnemy>();
                senemyList[i].transform.position = transform.position;
                b.attackRadius = Random.Range(7f, 13f);
                b.projectileWaveType = (int)Random.Range(0.0f, 3f);
                if (b.projectileWaveType == 0)
                {
                    b.GetComponent<SpriteRenderer>().material.SetColor("_Color", Color.red);
                }
                if (b.projectileWaveType == 1)
                {
                    b.GetComponent<SpriteRenderer>().material.SetColor("_Color", Color.green);
                }
                if (b.projectileWaveType == 2)
                {
                    b.GetComponent<SpriteRenderer>().material.SetColor("_Color", Color.blue);
                }
                senemyList[i].SetActive(true);

                break;
            }
        }
    }
}
