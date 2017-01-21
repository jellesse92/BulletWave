using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponScript : MonoBehaviour {

    public GameObject bulletDeflector;

    const float REFLECTOR_DURATION = 5f;
    

	// Use this for initialization
	void Start () {
        Invoke("ActivateReflector", 1f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void ActivateReflector()
    {
        Invoke("DeactivateReflector", REFLECTOR_DURATION);

    }

    void DeactivateReflector()
    {

    }
}
