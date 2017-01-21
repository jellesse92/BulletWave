﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponScript : MonoBehaviour {

    public GameObject bulletDeflector;
    public GameObject[] bulletTypes;

    const int MAX_COLOR_RANGE = 3;                              //Maximum amounts of colors to shift through
    const float FREQ_SHIFT_CD = 2.0f;                           //Time shifting is on cooldown

    //Reflector constants
    const float REFLECTOR_DURATION = 2f;                        //How long reflector stays active
    const float REFLECTOR_CD = 2.5f;                            //Cooldown between using reflector

    //Length of the tier 1 attack
    const float TIER1_ATTACK_LEN = 10f;
    const float TIER1_OFFSET_1 = 2f;
    const float TIER1_OFFSET_2 = 3f;
    const float TIER1_OFFSET_3 = 5f;
    const float TIER1_OFFSET_FAR = 8f;

    //Length of the tier 2 attack
    const float TIER2_ATTACK_LEN = 19f;
    const float TIER2_OFFSET_1 = 2f;
    const float TIER2_OFFSET_2 = 2f;
    const float TIER2_OFFSET_3 = 3f;
    const float TIER2_OFFSET_FAR = 3f;

    //Length of the tier 3 attack
    const float TIER3_ATTACK_LEN = 30f;

    //Charging tier times
    const float CHARGE_TIER1 = .2f;
    const float CHARGE_TIER2 = 1f;
    const float CHARGE_TIER3 = 2f;

    public AudioClip tier1WeaponSound;
    public AudioClip tier2WeaponSound;
    public AudioClip maxWeaponSound;
    AudioSource weaponAudio;

    LayerMask layermask;                                        //Prevent raycast from hitting unimportant layers
    bool checkChargeRelease = false;
    float timeCharged = 0f;

    bool reflectorOnCD = false;
    bool shiftOnCD = false;

    public int currentColor = 0;

    // Use this for initialization
    void Start() {
        weaponAudio = GetComponent<AudioSource>();

        layermask = (LayerMask.GetMask("Default", "Enemy"));

        currentColor--;
        FrequencyShift();
        CancelInvoke("ShiftEndCD");
        shiftOnCD = false;
    }

    // Update is called once per frame
    void Update() {
        TEMPORARY_CHARGE_STATUS();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            checkChargeRelease = true;
            timeCharged = 0f;
        }

        if(checkChargeRelease)
        {

            timeCharged += Time.deltaTime;
            if (Input.GetKeyUp(KeyCode.Space))
            {
                ExecuteAttack();
                checkChargeRelease = false;
                timeCharged = 0f;
            }
        }

       
        /*
        if (Input.GetKeyDown(KeyCode.Z))
        {
            ExecuteAttack1();

        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            ExecuteAttack2();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            ExecuteAttack3();
        }
        */

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            FrequencyShift();
        }


    }

    //REFLECTOR FUNCTIONS

    void ActivateReflector()
    {
        if (reflectorOnCD)
            return;

        reflectorOnCD = true;
        bulletDeflector.GetComponent<BulletDeflectorScript>().ActivateDeflector();
        Invoke("DeactivateReflector", REFLECTOR_DURATION);
        Invoke("ReflectorEndCD", REFLECTOR_CD);
    }

    void DeactivateReflector()
    {
        bulletDeflector.GetComponent<BulletDeflectorScript>().DeactivateDeflector();
    }

    void ReflectorEndCD()
    {
        reflectorOnCD = false;
    }

    //END REFLECTOR FUNCTIONS

    void ExecuteAttack1()
    {
        weaponAudio.PlayOneShot(tier1WeaponSound);


    }

    void ExecuteAttack2()
    {
        weaponAudio.PlayOneShot(tier2WeaponSound);


    }

    void ExecuteAttack3()
    {
        weaponAudio.PlayOneShot(maxWeaponSound);

    }

    void TEMPORARY_CHARGE_STATUS()
    {
        if (timeCharged > CHARGE_TIER3)
            Debug.Log("MAX CHARGE");
        else if (timeCharged > CHARGE_TIER2)
            Debug.Log("TIER 2 CHARGE");
        else if (timeCharged > CHARGE_TIER1)
            Debug.Log("TIER 1 CHARGE");
        else
            Debug.Log("DEFLECTOR");
    }

    void ExecuteAttack()
    {
        if (timeCharged > CHARGE_TIER3)
            ExecuteAttack3();
        else if (timeCharged > CHARGE_TIER2)
            ExecuteAttack2();
        else if (timeCharged > CHARGE_TIER1)
            ExecuteAttack1();
        else
            ActivateReflector();
    }

    //FREQUENCY SHIFT FUNCTIONS

    void FrequencyShift()
    {
        if (shiftOnCD)
            return;

        currentColor++;
        if (currentColor >= MAX_COLOR_RANGE)
            currentColor = 0;

        switch (currentColor)
        {
            case 0: ApplyShift(0,Color.red); break;
            case 1: ApplyShift(1,Color.blue); break;
            case 2: ApplyShift(2,Color.green); break;
        }
    }

    void ApplyShift(int n,Color c)
    {
        GetComponent<SpriteRenderer>().color = c;
        bulletDeflector.GetComponent<BulletDeflectorScript>().SetDeflectColor(n);
        shiftOnCD = true;
        Invoke("ShiftEndCD", FREQ_SHIFT_CD);
    }

    void ShiftEndCD()
    {
        shiftOnCD = false;
    }


    //END FREQUENCY SHIFTS
}
