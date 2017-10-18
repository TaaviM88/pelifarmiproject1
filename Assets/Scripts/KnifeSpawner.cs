using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeSpawner : MonoBehaviour {
    public GameObject FallingObsticle;
    private GameObject Fall;
    public bool canRespawn = true;
    public float timer = 3f;
    float _temp;
	// Use this for initialization
	void Start () {
		Fall = Instantiate(FallingObsticle,transform.position,Quaternion.Euler(new Vector3(0,0,-180)))as GameObject;
        _temp = timer;
	}
	
	// Update is called once per frame
	void Update () {
        if(timer <= 0)
        {
            Respawn();
        }
        if (canRespawn == true)
        {
            ResetTimer();
        }

        if (Fall == null && Time.realtimeSinceStartup % timer <= Time.deltaTime)
        {
            Fall = Instantiate(FallingObsticle, transform.position, Quaternion.Euler(new Vector3(0, 0, -180))) as GameObject;
        }
		
	}

    void Respawn()
    {
        canRespawn = true;
    }

    void ResetTimer()
    {
        timer = _temp;
    }
}
