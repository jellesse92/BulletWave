using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEMP_EnemyReact : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ReceiveDamage(int damage)
    {
        GetComponent<SpriteRenderer>().color = Color.red;
    }
}
