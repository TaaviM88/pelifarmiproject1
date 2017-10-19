using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    public GameObject Prefab;
    private GameObject _spawn;
    private bool _playeronRange;
    public bool canRespawn = true;
    private float _nextSpawn = 0.0f;
    public float spawnTime = 1f;
    public float activationTime = 5;
    //public int maxSpawnCount = 4;
    private int spawnCount = 0;
    public bool destroyEnemy;
	// Use this for initialization
	void Start () {
        gameObject.SetActive(true);
        //Invoke("Activation",activationTime);
	}
	
	// Update is called once per frame
	void Update () { 
        if (_playeronRange == true)
        {

            if (Time.time > _nextSpawn) //(spawnCount <= maxSpawnCount && Time.time > _nextSpawn)
            {
                _nextSpawn = Time.time + spawnCount;
                _spawn = Instantiate(Prefab, transform.position, Quaternion.identity) as GameObject;
                spawnCount++;
            }
        }
	}

    void Respawn()
    {
        if (_spawn == null && canRespawn)
        {
            _spawn = Instantiate(Prefab, transform.position, Quaternion.identity) as GameObject;
        }
    }
    void Activation()
    {
        gameObject.SetActive(true);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            //Activation();
            _playeronRange = true;
           
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            //Activation();
            _playeronRange = false;
        }
    }
}
