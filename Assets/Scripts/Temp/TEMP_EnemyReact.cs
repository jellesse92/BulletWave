using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEMP_EnemyReact : MonoBehaviour {

	// Use this for initialization
	void Start () {
        //GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -1000f));
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ReceiveDamage(int damage)
    {
        if(damage == 5)
            GetComponent<SpriteRenderer>().color = Color.red;
        if(damage == 10)
            GetComponent<SpriteRenderer>().color = Color.green;
        if(damage == 15)
            GetComponent<SpriteRenderer>().color = Color.blue;
        Invoke("DeactiveEffect", .1f);
    }

    public void DeactiveEffect()
    {
        GetComponent<SpriteRenderer>().color = Color.white;
    }
}
