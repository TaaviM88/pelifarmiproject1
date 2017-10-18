using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour 
{
    private float _jumpForce = 5f;
    private Rigidbody2D playerRigidbody2D;
    private bool _grounded, _facingRight = true;
    public float Speed = 3f, RunSpeed = 1;
    private float _speed;
    public Transform SpawnPoint;
    private Animator anime;
    private bool _jumping = false;
    private Vector3 latestCheckpoint;
    public AudioClip JumpSound;
    private AudioSource source;
    private float volLowRage = 0.5f;
    private float volHighRange = 1.0f;

	// Use this for initialization
	void Start () 
    {
        playerRigidbody2D = GetComponent<Rigidbody2D>();
        anime = GetComponent<Animator>();
        GoToSpawnPoint();
        _speed = playerRigidbody2D.velocity.x;
        updateCheckpoint();
        source = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () 
    {

        MovePlayer(_speed);
        HandleJumpAndFall();
        if (Input.GetAxis("Horizontal") > 0)
        {
            //transform.Translate(new Vector3(Speed * Time.deltaTime, 0, 0));
            if (_facingRight == false)
            {
                Flip();
            }
            playerRigidbody2D.velocity = new Vector2(Speed * RunSpeed, playerRigidbody2D.velocity.y);
            _speed = playerRigidbody2D.velocity.x;
            //playerRigidbody2D.velocity = new Vector2(Speed, 0); 
        }

        if (Input.GetAxis("Horizontal") < 0)
        {
            //transform.Translate(new Vector3(-1*Speed * Time.deltaTime, 0, 0));
            if (_facingRight == true)
            {
                Flip();
            }
                  playerRigidbody2D.velocity = new Vector2(-1*Speed * RunSpeed, playerRigidbody2D.velocity.y);
                  _speed = playerRigidbody2D.velocity.x;
            //playerRigidbody2D.velocity = new Vector2(-1 * Speed, 0);
        }
        if (Input.GetAxis("Horizontal") == 0)
        {
            _speed = 0;
        }
        //Käskytetään pelaajaa hyppäämään
        if (Input.GetAxis("Fire1") > 0 && _grounded == true)
        {
            playerRigidbody2D.AddForce(new Vector2(0f, _jumpForce), ForceMode2D.Impulse);
            _grounded = false;
            _jumping = true;
            anime.SetInteger("State", 3);
            float vol = Random.Range(volLowRage, volHighRange);
          
            //aktivoi tämä kun haluat hyppyäänen
            //source.PlayOneShot(JumpSound);
        }

        if (Input.GetAxis("Fire2") > 0) //&& _grounded == true)
        {
            RunSpeed = 2;
        }
        else
        {
            RunSpeed = 1;
        }
        Debug.Log(_grounded);
        
	}

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "ground")
        {
            _grounded = true;
            _jumping = false;
        }
        if (col.gameObject.tag == "KillAxel")
        {
            anime.Play("PlayerDeath");
            GoToCheckpoint();
            //Invoke("GoToSpawnPoint", anime.playbackTime);
            //GoToSpawnPoint();
            Debug.Log("Spawnasin perkele");
        }

    }
    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "ground" && playerRigidbody2D.velocity.y >0)
        {
            _grounded = false;
        }
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Checkpoint"))
        {
            updateCheckpoint();
        }
    }

    void updateCheckpoint()
    {
        latestCheckpoint = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }

    void GoToCheckpoint()
    {
        transform.position = latestCheckpoint;
        GameObject obj = GameObject.FindGameObjectWithTag("MainCamera");
        obj.gameObject.SendMessage("GoToPlayer");
    }

    //Wanha
    void  GoToSpawnPoint()
    {
        Vector3 po = transform.position;
        po.x = SpawnPoint.position.x;
        po.y = SpawnPoint.position.y;
        transform.position = po;
    }
    private void Flip()
    {
        _facingRight = !_facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void MovePlayer(float playerSpeed)
    {
        if(playerSpeed < 0 && !_jumping || playerSpeed > 0 && !_jumping)
        {
            anime.SetInteger("State",2);
        }
        if(playerSpeed == 0 && !_jumping)
        {
            anime.SetInteger("State",0);
        }
        //playerRigidbody2D.velocity = new Vector2(Speed, playerRigidbody2D.velocity.y);
    }

    void HandleJumpAndFall()
    {
        if (_jumping)
        {
            if(playerRigidbody2D.velocity.y >0)
            {
                anime.SetInteger("State", 3);
            }
            else
            {
                anime.SetInteger("State", 1);
            }
        }
    }

}
