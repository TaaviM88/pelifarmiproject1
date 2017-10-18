using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBlock : MonoBehaviour {
    public Transform  Startpoint;
    public float Speed = 3f;
	// Use this for initialization
	void Start () {
        //GoToStartPoint();
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(new Vector3(-1 * Speed * Time.deltaTime, 0, 0));
	}

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Endpoint")
        {
          GoToStartPoint();
          Debug.Log("Osuin perkele");
          Debug.Log(transform.position);
        }
    }
    void Destroy()
    {
        gameObject.SetActive(false);
    }

    void GoToStartPoint()
    {
        Vector3 po = transform.position;
        po.x = Startpoint.position.x;
        //po.y = Startpoint.position.y;
        transform.position = po;
    }

}
