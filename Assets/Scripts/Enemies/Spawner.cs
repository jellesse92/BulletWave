﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public GameObject aenemy;
    public int aenemyNum;
    public List<GameObject> aenemyList;
    public bool isCoolingDown;
    public float coolDownTime;

    // Use this for initialization
    void Start () {
        aenemyList = new List<GameObject>();
        for (int i = 0; i < aenemyNum; i++)
        {
            GameObject b = (GameObject)Instantiate(aenemy);
            b.SetActive(false);
            aenemyList.Add(b);
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
        for (int i = 0; i < aenemyNum; i++)
        {
            if (!aenemyList[i].activeInHierarchy)
            {
                ApproachEnemy b = aenemyList[i].GetComponentInChildren<ApproachEnemy>();
                aenemyList[i].transform.position = transform.position;
                b.attackRadius = Random.Range(7f, 13f);
                aenemyList[i].SetActive(true);

                break;
            }
        }
    }
}
