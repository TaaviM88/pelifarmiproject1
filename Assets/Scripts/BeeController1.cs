using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeController1 : MonoBehaviour {
    private float _speed = 2f;
    public AnimationCurve Curve;
    private Vector3 enemyMovent;
    private int _direction = -1;
	// Use this for initialization
	void Awake () {
        gameObject.SetActive(true);
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.timeScale > 0)
        {
            transform.position = new Vector3(transform.position.x,Curve.Evaluate(Time.realtimeSinceStartup), transform.position.z);
            //transform.position.y = Curve.Evaluate(Time.realtimeSinceStartup);
           /* if transform.position.y < 0)
            {
                transform.position -=
            }*/
        }
        //Invoke("Disable", 25f);
	}

    void OnEnable()
    {

    }

    void Disable()
    {
        //gameObject.SetActive(false);
        Destroy(gameObject);
       
    }

    void OnDisable()
    {
        CancelInvoke();
    }
}
