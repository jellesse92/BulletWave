﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRange : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
           
            transform.parent.GetComponentInChildren<Enemy>().inAttackRange = true;
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            transform.parent.GetComponentInChildren<Enemy>().inAttackRange = false;
        }
    }
}
