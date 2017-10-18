using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemyController : MonoBehaviour {
    public float Speed = 1.5f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(new Vector3(-1* Speed * Time.deltaTime,0,0));
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col == true)
        {
            return;
        }
    }
}
