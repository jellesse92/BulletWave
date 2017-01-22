using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 

public class MenuPause : MonoBehaviour {

    //public UnityEvent pause; 
    public GameObject pause;
    bool pressed = false;

    // Use this for initialization
	void Start () {
    }

    // Update is called once per frame
    void FixedUpdate () {
        if (Input.GetKeyDown(KeyCode.Return) && !pressed)
        {
            if (!pause.activeSelf)
            {
                pressed = true;
                pause.SetActive(true);
                Time.timeScale = 0;
            }
        }
        else
            pressed = false;


    }

    public void Resume()
    {
        if (pause.activeSelf && !pressed)
        {
            pause.SetActive(false);
            Time.timeScale = 1;
        }
        else
            pressed = false;

    }
}
