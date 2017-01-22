using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameObject inputManager;
        inputManager = GameObject.FindGameObjectWithTag("Player Join");
        inputManager.GetComponent<PlayerJoin>().StartUpdate();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
