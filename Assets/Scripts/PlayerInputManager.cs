using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputManager : MonoBehaviour {

    PlayerJoin inputManager;

	// Use this for initialization
	void Start () {
        inputManager = GameObject.FindGameObjectWithTag("Player Join").GetComponent<PlayerJoin>();

        transform.GetChild(0).GetComponent<PlayerInput>().SetInput(inputManager.GetPlayerInputs(0));
        if (!transform.GetChild(0).gameObject.activeSelf)
            transform.GetChild(0).gameObject.SetActive(true);

        transform.GetChild(1).GetComponent<PlayerInput>().SetInput(inputManager.GetPlayerInputs(1));
        inputManager.StopUpdate();

    }
	
	// Update is called once per frame
	void Update () {
		
	}


}
