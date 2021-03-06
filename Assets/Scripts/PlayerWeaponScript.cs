﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponScript : MonoBehaviour {

    public bool isFacingRight = false;

    public GameObject bulletDeflector;
    public Transform bulletList;
    public GameObject[] bulletTypes;

    const float PLAYER_SPEED = 5f;

    const int BULLET_GENERATE_AMT = 20;
    const float BULLET_FORCE = 220f;
    const float BULLET_FORCE2 = 290f;
    const float BULLET_FORCEMAX = 440f;

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
    bool checkChargeTime = false;
    float timeCharged = 0f;

    bool reflectorOnCD = false;
    bool shiftOnCD = false;

    public PlayerInput input;

    private void Awake()
    {
        GenerateBullets();  
    }

    // Use this for initialization
    void Start() {
        weaponAudio = GetComponent<AudioSource>();

        layermask = (LayerMask.GetMask("Default", "Enemy"));

        GetComponent<Player>().color--;
        FrequencyShift();
        CancelInvoke("ShiftEndCD");
        shiftOnCD = false;
    }

    private void Update()
    {

    } 

    // Update is called once per frame
    void FixedUpdate() {

        if (GetComponent<Player>().DeathStatus())
        {
            input.ResetKeyPress();
            return;
        }

        Vector3 position;

        position = new Vector3(input.GetKeyPress().horizontalAxisValue * Time.deltaTime * PLAYER_SPEED, 
            input.GetKeyPress().verticalAxisValue * Time.deltaTime * PLAYER_SPEED, 0);
        transform.position += position;


        if (input.GetJoystick() > 0)
            if (Mathf.Abs(input.GetKeyPress().rightHorizontalAxisValue) > .3f || Mathf.Abs(input.GetKeyPress().rightVerticalAxisValue) > .3f)
            {
                float angle = Mathf.Atan2(input.GetKeyPress().rightVerticalAxisValue, input.GetKeyPress().rightHorizontalAxisValue * -1f) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle + 90f, Vector3.forward);
            }

        if (input.GetKeyPress().rightTriggerPressed)
        {
            checkChargeTime = true;
            timeCharged = 0f;
        }

        if (input.GetKeyPress().leftTriggerPressed)
        {
            FrequencyShift();
        }

        if (checkChargeTime)
            timeCharged += Time.deltaTime;      

        if (input.GetKeyPress().rightTriggerReleased)
        {
            ExecuteAttack();
            checkChargeTime = false;
            timeCharged = 0f;
        }

        input.ResetKeyPress();
    }

    void GenerateBullets()
    {
        foreach(GameObject type in bulletTypes)
        {
            GameObject typeList = (GameObject)Instantiate(new GameObject());
            typeList.name = type.name + " List";
            typeList.transform.parent = bulletList;

            for(int i = 0; i < BULLET_GENERATE_AMT; i++)
            {
                GameObject bullet = (GameObject)Instantiate(type);
                bullet.transform.SetParent(typeList.transform);
                bullet.SetActive(false);
            }
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
        //weaponAudio.PlayOneShot(tier1WeaponSound);
        FireBullet(0);

    }

    void ExecuteAttack2()
    {
        //weaponAudio.PlayOneShot(tier2WeaponSound);
        FireBullet(1);

    }

    void ExecuteAttack3()
    {
        //weaponAudio.PlayOneShot(maxWeaponSound);
        FireBullet(2);

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

    void FireBullet(int type)
    {
        GameObject bullet = GetBullet(type);
        bullet.SetActive(true);
        bullet.GetComponent<PlayerSoundBullet>().Initialize(GetComponent<Player>().color);
        bullet.transform.position = transform.position;
        bullet.transform.rotation = Quaternion.Euler(0f, 0f, transform.rotation.eulerAngles.z);


        switch (type)
        {
            case 0: bullet.GetComponent<Rigidbody2D>().AddForce(transform.up * BULLET_FORCE);
                break;
            case 1: bullet.GetComponent<Rigidbody2D>().AddForce(transform.up * BULLET_FORCE2);
                break;
            case 2:
                bullet.GetComponent<Rigidbody2D>().AddForce(transform.up * BULLET_FORCEMAX);
                break;
            default:
                bullet.GetComponent<Rigidbody2D>().AddForce(transform.up * BULLET_FORCE);
                break;
        }

    }

    GameObject GetBullet(int type)
    {
        GameObject bullet;
        foreach(Transform child in bulletList.GetChild(type))
        {
            if (!child.gameObject.activeSelf)
            {
                bullet = child.gameObject;
                return bullet;
            }
        }

        return null;
    }

    //FREQUENCY SHIFT FUNCTIONS

    void FrequencyShift()
    {
        if (shiftOnCD)
            return;

        GetComponent<Player>().color++;
        if (GetComponent<Player>().color >= MAX_COLOR_RANGE)
            GetComponent<Player>().color = 0;

        switch (GetComponent<Player>().color)
        {
            case 0: ApplyShift(0,Color.red); break;
            case 2: ApplyShift(1,Color.blue); break;
            case 1: ApplyShift(2,Color.green); break;
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
