using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float speed = 5; 
    float playerMoveHorizontal;
    float playerMoveVertical;
    Vector3 position; 

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        playerMoveHorizontal = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        playerMoveVertical = Input.GetAxis("Vertical") * Time.deltaTime * speed;

       
      
    }

    void FixedUpdate()
    {
        position = new Vector3(playerMoveHorizontal, playerMoveVertical, 0);
        transform.position += position;

    }
}
