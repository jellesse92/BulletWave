using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEMP_BOUNCE : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 1000f));
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
