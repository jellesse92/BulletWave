using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJoin : MonoBehaviour {

    int playersJoined = 0;
    int[] controlAssignment = { -1, -1 };

    private void Update()
    {
        CheckForJoin();
    }

    void CheckForJoin()
    {
        for(int i = 1; i < 12; i++)
        {
            if (Input.GetAxis(i.ToString() + "_RightTrigger") <-.5f)
                JoinJoystick(i);
        }
    }

    void JoinJoystick(int index)
    {
        Debug.Log("Joystick " + index.ToString());
        if (playersJoined >= 2)
            return;

        controlAssignment[playersJoined] = index;
        playersJoined++;

    }
    
}
