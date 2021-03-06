﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner2 : MonoBehaviour {

    public GameObject senemy;
    public int senemyNum;
    public List<GameObject> senemyList;
    public bool isCoolingDown;
    public float coolDownTime;

    public Vector3 rotationMask; //which axes to rotate around
    public float rotationSpeed;  //degrees per second
    public GameObject rotateAroundObject;

    public float spawnMin = 2;
    public float spawnMax = 5;

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

        rotationMask = new Vector3(0, 0, 1);
        rotationSpeed = 5;
        rotateAroundObject = GameObject.FindGameObjectWithTag("MainCamera");
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if (rotateAroundObject)
        {//If true in the inspector orbit <rotateAroundObject>:
            transform.RotateAround(rotateAroundObject.transform.position,
            rotationMask, rotationSpeed * Time.deltaTime);
        }
        else
        {//not set -> rotate around own axis/axes:
            transform.Rotate(new Vector3(
            rotationMask.x * rotationSpeed * Time.deltaTime,
            rotationMask.y * rotationSpeed * Time.deltaTime,
            rotationMask.z * rotationSpeed * Time.deltaTime));
        }

        if (!isCoolingDown)
        {
            coolDownTime = Random.Range(spawnMin, spawnMax);
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
                senemyList[i].GetComponentInChildren<CircleCollider2D>().radius = Random.Range(3.5f, 7f);
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
