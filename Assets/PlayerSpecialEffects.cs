using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpecialEffects : MonoBehaviour {

    GameObject camera;

	// Use this for initialization
	void Start () {
        camera = GameObject.FindGameObjectWithTag("MainCamera");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StartShake(float magnitude = .5f, float duration = .5f)
    {
        if (camera != null)
            camera.GetComponent<ScreenShakeScript>().StartShake(magnitude, duration);
    }
}
