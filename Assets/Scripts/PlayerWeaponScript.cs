using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponScript : MonoBehaviour {

    public GameObject bulletDeflector;

    const float REFLECTOR_DURATION = 2f;                        //How long reflector stays active
    const float REFLECTOR_CD = 2.5f;                            //Cooldown between using reflector

    const float TIER1_ATTACK_LEN = 10f;                          //Length of the tier 1 attack

    LayerMask layermask;                                        //Prevent raycast from hitting unimportant layers

    bool reflectorOnCD = false;

	// Use this for initialization
	void Start () {
        layermask = (LayerMask.GetMask("Default", "Enemy"));
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            //ActivateReflector();
            ExecuteAttack1();
        }

        //TIER 1!!!
        Color color = Color.green;

        Debug.DrawRay(transform.position, transform.up * TIER1_ATTACK_LEN, color);
        Debug.DrawRay(transform.position, (transform.up + new Vector3(1f,0f,0f)) * TIER1_ATTACK_LEN, color);


        Debug.DrawRay(transform.position, (transform.up + new Vector3(-1f, 0f, 0f)) * TIER1_ATTACK_LEN, color);
        Debug.DrawRay(transform.position, (transform.up + new Vector3(.5f, 0f, 0f)) * TIER1_ATTACK_LEN, color);
        Debug.DrawRay(transform.position, (transform.up + new Vector3(-.5f, 0f, 0f)) * TIER1_ATTACK_LEN, color);


    }

    //REFLECTOR FUNCTIONS

    void ActivateReflector()
    {
        if (reflectorOnCD)
            return;

        reflectorOnCD = true;
        bulletDeflector.GetComponent<Collider2D>().enabled = true;
        Invoke("DeactivateReflector", REFLECTOR_DURATION);
        Invoke("ReflectorEndCD", REFLECTOR_CD);
    }

    void DeactivateReflector()
    {
        bulletDeflector.GetComponent<Collider2D>().enabled = false;
    }

    void ReflectorEndCD()
    {
        reflectorOnCD = false;
    }

    //END REFLECTOR FUNCTIONS

    void ExecuteAttack1()
    {
        RaycastHit2D[][] rays = new RaycastHit2D[5][];
        rays[0] = Physics2D.RaycastAll(transform.position, transform.up, TIER1_ATTACK_LEN, layermask);
        rays[1] = Physics2D.RaycastAll(transform.position, (new Vector2(10f, 10f)), 100f, layermask);

        foreach (RaycastHit2D targets in rays[0])
            if (targets)
                if (targets.collider.tag == "Enemy")
                    targets.transform.GetComponent<TEMP_EnemyReact>().ReceiveDamage(5);

        foreach (RaycastHit2D targets in rays[1])
            if (targets)
                if (targets.collider.tag == "Enemy")
                    targets.transform.GetComponent<TEMP_EnemyReact>().ReceiveDamage(5);
    }
}
