﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {

    [System.Serializable]
    public class KeyConfig
    {
        public string horizontalAxisName = "Horizontal";
        public string verticalAxisName = "Vertical";
        public string rightTriggerName = "1_RightTrigger";
        public string rightHorizontalAxisName = "1_RightJoystickX";
        public string rightVerticalAxisName = "1_RightJoystickY";
        public string leftTriggerName = "1_LeftTrigger";
    }

    [System.Serializable]
    public class KeyPress
    {
        public float horizontalAxisValue;
        public float verticalAxisValue;
        public bool rightTriggerPressed;
        public bool rightTriggerReleased;
        public float rightHorizontalAxisValue = 0f;
        public float rightVerticalAxisValue = 0f;
        public bool leftTriggerPressed;
    }

    KeyPress keysPressed = new KeyPress();
    KeyConfig inputConfig = new KeyConfig();
    int joystickNum = -1;

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

        x = (Mathf.Abs(Input.GetAxis(inputConfig.rightHorizontalAxisName)) > 0.3) ? Input.GetAxis(inputConfig.rightHorizontalAxisName) : 0f;
        y = (Mathf.Abs(Input.GetAxis(inputConfig.rightVerticalAxisName)) > 0.3) ? Input.GetAxis(inputConfig.rightVerticalAxisName) : 0f;

        keysPressed.rightHorizontalAxisValue = x;
        keysPressed.rightVerticalAxisValue = y;

        if (!checkForRightTriggerRelease && (joystickNum == -1 && Input.GetKeyDown(KeyCode.Space)) || (joystickNum > 0 && !checkForRightTriggerRelease && Input.GetAxis(inputConfig.rightTriggerName) > .5f))
        {
            keysPressed.rightTriggerPressed = true;
            checkForRightTriggerRelease = true;
        }
        else if (checkForRightTriggerRelease)
            if((joystickNum == -1 && Input.GetKeyUp(KeyCode.Space)) || (joystickNum > 0 && Input.GetAxis(inputConfig.rightTriggerName) < .5f))
            {
                checkForRightTriggerRelease = false;
                keysPressed.rightTriggerReleased = true;
            }

        if((joystickNum == -1 && Input.GetKeyDown(KeyCode.LeftShift)) || (joystickNum > 0 && Input.GetAxis(inputConfig.leftTriggerName) > .5f))
        {
            keysPressed.leftTriggerPressed = true;
        }

    }

    public void ResetKeyPress()
    {
        keysPressed.rightTriggerPressed = false;
        keysPressed.rightTriggerReleased = false;
        keysPressed.leftTriggerPressed = false;
    }

    public KeyPress GetKeyPress()
    {
        return keysPressed;
    }

    public void SetInput(int index)
    {
        if (index <= 0)
        {
            
            gameObject.SetActive(false);
            transform.GetChild(0).GetComponent<PlayerWeaponScript>().CancelInvoke();
            transform.GetChild(0).GetComponent<PlayerWeaponScript>().StopAllCoroutines();
            return;
        }

        string nStr = index.ToString();
        joystickNum = index;
        inputConfig.rightTriggerName = nStr + "_RightTrigger";
        inputConfig.horizontalAxisName = nStr + "_LeftJoystickX";
        inputConfig.verticalAxisName = nStr + "_LeftJoystickY";
        inputConfig.rightHorizontalAxisName = nStr + "_RightJoystickX";
        inputConfig.rightVerticalAxisName = nStr + "_RightJoystickY";
        inputConfig.leftTriggerName = nStr + "_LeftTrigger";
    }

    public int GetJoystick()
    {
        return joystickNum;
    }

}
