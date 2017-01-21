using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponScript : MonoBehaviour {

    

	// Use this for initialization
	void Start () {
        Invoke("AttackTier1", 1f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void AttackTier1()
    {
        Debug.Log("ATtack tier 1");

    }
}
