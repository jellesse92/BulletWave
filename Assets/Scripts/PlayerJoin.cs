using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJoin : MonoBehaviour {

    public static PlayerJoin Instance
    {
        get;
        set;
    }


    int playersJoined = 0;
    int[] controlAssignment = { -1, -1 };

    private void Start()
    {
        DontDestroyOnLoad(transform.gameObject);
        Instance = this;

        //Destroys copy of this on scene
        if (FindObjectsOfType(GetType()).Length > 1)
            Destroy(gameObject);
    } 

    private void Update()
    {
        CheckForJoin();
    }

    void CheckForJoin()
    {
        for(int i = 1; i < 12; i++)
        {
            if (Input.GetAxis(i.ToString() + "_RightTrigger") > .5f)
                JoinJoystick(i);
        }
    }

    void JoinJoystick(int index)
    {
        if (playersJoined >= 2)
            return;
        controlAssignment[playersJoined] = index;
        playersJoined++;
        Debug.Log("Player Entered:" + playersJoined);
    }

    public int GetPlayerInputs(int player)
    {
        if (player >= 2)
            Debug.Log("too many player join");

        return controlAssignment[player];
    }

    
}
