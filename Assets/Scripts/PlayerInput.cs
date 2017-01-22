using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {

    [System.Serializable]
    public class KeyConfig
    {
        public string horizontalAxisName = "Horizontal";
        public string verticalAxisName = "Vertical";
        public string rightTriggerName = "1_RightTrigger";
    }

    [System.Serializable]
    public class KeyPress
    {
        public float horizontalAxisValue;
        public float verticalAxisValue;
        public bool rightTriggerPressed;
        public bool rightTriggerReleased;
    }

    KeyPress keysPressed = new KeyPress();
    KeyConfig inputConfig = new KeyConfig();
    int joystickNum = 1;

    bool checkForRightTriggerRelease = false;


    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        GetInput();
    }

    void GetInput()
    {
        float x = (Mathf.Abs(Input.GetAxis(inputConfig.horizontalAxisName)) > 0.06) ? Input.GetAxis(inputConfig.horizontalAxisName) : 0f;
        float y = (Mathf.Abs(Input.GetAxis(inputConfig.verticalAxisName)) > 0.06) ? Input.GetAxis(inputConfig.verticalAxisName) : 0f;

        keysPressed.horizontalAxisValue = x;
        keysPressed.verticalAxisValue = y;

        if(!checkForRightTriggerRelease && (joystickNum == -1 && Input.GetKeyDown(KeyCode.Space)) || (joystickNum > 0 && !checkForRightTriggerRelease && Input.GetAxis(inputConfig.rightTriggerName) > .5f))
        {
            keysPressed.rightTriggerPressed = true;
            checkForRightTriggerRelease = true;
        }
        else if (checkForRightTriggerRelease)
            if((joystickNum == -1 && Input.GetKeyUp(KeyCode.Space)) || (joystickNum > 0 && Input.GetAxis(inputConfig.rightTriggerName) < .5f))
            {
                Debug.Log("testing");
                Debug.Log(joystickNum == -1 && Input.GetKeyUp(KeyCode.Space));
                checkForRightTriggerRelease = false;
                keysPressed.rightTriggerReleased = true;
            }

    }

    public void ResetKeyPress()
    {
        keysPressed.rightTriggerPressed = false;
        keysPressed.rightTriggerReleased = false;
    }

    public KeyPress GetKeyPress()
    {
        return keysPressed;
    }

    public void SetInput(int index)
    {
        joystickNum = index;
    }

}
