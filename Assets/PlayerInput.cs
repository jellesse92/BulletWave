using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {

    [System.Serializable]
    public class KeyConfig
    {
        public string horizontalAxisName = "Horizontal_Key";
        public string verticalAxisName = "Vertical_Key";
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
		
	}

    void GetInput()
    {
        float x = (Mathf.Abs(Input.GetAxis(inputConfig.horizontalAxisName)) > 0.06) ? Input.GetAxis(inputConfig.horizontalAxisName) : 0f;
        float y = (Mathf.Abs(Input.GetAxis(inputConfig.verticalAxisName)) > 0.06) ? Input.GetAxis(inputConfig.verticalAxisName) : 0f;

        keysPressed.horizontalAxisValue = x;
        keysPressed.verticalAxisValue = y;

        if(!checkForRightTriggerRelease && Input.GetAxis(inputConfig.rightTriggerName) > .5f)
        {
            keysPressed.rightTriggerPressed = true;
            checkForRightTriggerRelease = true;
        }
        else if (checkForRightTriggerRelease)
            if(Input.GetAxis(inputConfig.rightTriggerName) < .5f)
            {
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
