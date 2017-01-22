using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
        transform.GetChild(0).GetComponent<PlayerInput>().SetInput
            (GameObject.FindGameObjectWithTag("Player Join").GetComponent<PlayerJoin>().GetPlayerInputs(0));
        transform.GetChild(1).GetComponent<PlayerInput>().SetInput
            (GameObject.FindGameObjectWithTag("Player Join").GetComponent<PlayerJoin>().GetPlayerInputs(1));
    }
	
	// Update is called once per frame
	void Update () {
		
	}


}
