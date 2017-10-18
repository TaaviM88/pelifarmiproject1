using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoverSimply : MonoBehaviour {
    private Rigidbody2D _enemyEb;
    public float Speed = 1;
    private int _direction = 1;
    public bool CanEnemyTurn = true;
	// Use this for initialization

    void Awake()
    {
        _direction = -1;
        _enemyEb = GetComponent<Rigidbody2D>();
    }


	
	// Update is called once per frame
	void Update () {

        _enemyEb.velocity = new Vector2(_direction* Speed, _enemyEb.velocity.y);
	}

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.otherCollider)
        {
            if(CanEnemyTurn == true)
            {
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
            _direction *= -1;
            }
        }
    }
}
