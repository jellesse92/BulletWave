using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {


    public GameObject aenemy;
    public int aenemyNum;
    public List<GameObject> aenemyList;
    public bool isCoolingDown;
    public float coolDownTime;

    public Vector3 rotationMask; //which axes to rotate around
    public float rotationSpeed;  //degrees per second
    public GameObject rotateAroundObject;

    public float spawnMin = 2;
    public float spawnMax = 5;

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
        for (int i = 0; i < aenemyNum; i++)
        {
            if (!aenemyList[i].activeInHierarchy)
            {
                ApproachEnemy b = aenemyList[i].GetComponentInChildren<ApproachEnemy>();
                aenemyList[i].transform.position = transform.position;
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
                aenemyList[i].SetActive(true);

                break;
            }
        }
    }
}
