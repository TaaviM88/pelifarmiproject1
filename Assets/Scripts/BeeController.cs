using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeController : MonoBehaviour {
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
            transform.position -= new Vector3(_speed * Time.deltaTime, Curve.Evaluate(Time.realtimeSinceStartup), 0);
        }
        Invoke("Disable", 25f);
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
