using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {

    public static MusicManager Instance
    {
        get;
        set;
    }

    // Use this for initialization
    void Start () {
        DontDestroyOnLoad(transform.gameObject);
        Instance = this;

        //Destroys copy of this on scene
        if (FindObjectsOfType(GetType()).Length > 1)
            Destroy(gameObject);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
