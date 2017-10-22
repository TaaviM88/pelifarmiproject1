using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour 
{
    public float _jumpForce = 4.8f;
    private Rigidbody2D playerRigidbody2D;
    private bool _grounded, _facingRight = true;
    public float Speed = 3f, RunSpeed = 1;
    private float _speed;
    public Transform SpawnPoint;
    private Animator anime;
    private bool _jumping = false;
    private Vector3 latestCheckpoint;
    public AudioClip JumpSound;
    public AudioClip DeathSound;
    private AudioSource source;
    private float volLowRage = 0.5f;
    private float volHighRange = 1.0f;
    private Transform GroundCheck, CeilingCheck;
    const float groundedRadius = 0.1f, celingRadius = 0.01f;
    private CircleCollider2D ccollider;
     [SerializeField] private LayerMask whatIsGround;
     bool _canMove;

	// Use this for initialization
	void Start () 
    {
        GroundCheck = transform.Find("GroundCheck");
        CeilingCheck = transform.Find("CeilingCheck");
        playerRigidbody2D = GetComponent<Rigidbody2D>();
        anime = GetComponent<Animator>();
        _canMove = true;
        //GoToSpawnPoint();
        _speed = playerRigidbody2D.velocity.x;
        updateCheckpoint();
        source = GetComponent<AudioSource>();
        //anim["PlayerDeath"].speed = 0.2f;
        ccollider = GetComponent<CircleCollider2D>();
	}

    void FixedUpdate()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(GroundCheck.position, groundedRadius, whatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {

                _grounded = true;
            }
        }


        MovePlayer(_speed);
        HandleJumpAndFall();
        if (Input.GetAxis("Horizontal") > 0 && _canMove == true)
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

        if (Input.GetAxis("Horizontal") < 0 && _canMove == true)
        {
            //transform.Translate(new Vector3(-1*Speed * Time.deltaTime, 0, 0));
            if (_facingRight == true)
            {
                Flip();
            }
            playerRigidbody2D.velocity = new Vector2(-1 * Speed * RunSpeed, playerRigidbody2D.velocity.y);
            _speed = playerRigidbody2D.velocity.x;
            //playerRigidbody2D.velocity = new Vector2(-1 * Speed, 0);
        }
        if (Input.GetAxis("Horizontal") == 0)
        {
            _speed = 0;
        }
        //Käskytetään pelaajaa hyppäämään
        if (Input.GetAxis("Fire1") > 0 && _grounded == true && _canMove == true)
        {
            if (playerRigidbody2D.velocity.y == 0)
            {
                playerRigidbody2D.AddForce(new Vector2(0f, _jumpForce), ForceMode2D.Impulse);
                _grounded = false;
                _jumping = true;
                anime.SetInteger("State", 3);
                float vol = Random.Range(volLowRage, volHighRange);
                //aktivoi tämä kun haluat hyppyäänen
                source.PlayOneShot(JumpSound);
            }

        }

        if (Input.GetAxis("Fire2") > 0 && _canMove == true) //&& _grounded == true)
        {
            RunSpeed = 2;
        }
        else
        {
            RunSpeed = 1;
        }
        //Debug.Log(_grounded);
        
    }
	// Update is called once per frame
	void Update () 
    {

       /* MovePlayer(_speed);
        HandleJumpAndFall();
        if (Input.GetAxis("Horizontal") > 0 && _canMove == true)
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

        if (Input.GetAxis("Horizontal") < 0 && _canMove == true)
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
        if (Input.GetAxis("Fire1") > 0 && _grounded == true && _canMove == true)
        {
            if (playerRigidbody2D.velocity.y == 0)
            {
                playerRigidbody2D.AddForce(new Vector2(0f, _jumpForce), ForceMode2D.Impulse);
                _grounded = false;
                _jumping = true;
                anime.SetInteger("State", 3);
                float vol = Random.Range(volLowRage, volHighRange);
                //aktivoi tämä kun haluat hyppyäänen
                source.PlayOneShot(JumpSound);
            }
            
        }

        if (Input.GetAxis("Fire2") > 0 && _canMove == true) //&& _grounded == true)
        {
            RunSpeed = 2;
        }
        else
        {
            RunSpeed = 1;
        }
        //Debug.Log(_grounded);*/
        
	}

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "ground") //&& playerRigidbody2D.velocity.y == 0)
        {
            _grounded = true;
            _jumping = false;
        }
        if (col.gameObject.tag == "KillAxel")
        {
            _canMove = false;
            anime.Play("PlayerDeath");
            //anime.SetInteger("State", 3);
            Invoke("GoToCheckpoint", 0.3f);
           
            //GoToCheckpoint();
            //Debug.Log("Spawnasin perkele");
        }

    }
    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "ground" && playerRigidbody2D.velocity.y > 0)
        {
            _grounded = false;
            _jumping = true;
        }
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Checkpoint"))
        {
            updateCheckpoint();
        }
        if (col.gameObject.tag == "KillAxel")
        {
            anime.Play("PlayerDeath");
            source.PlayOneShot(DeathSound);
            //anime.SetInteger("State", 3);
            Invoke("GoToCheckpoint", 0.1f);
            //GoToCheckpoint();
           // Debug.Log("Spawnasin perkele");
        }
        /*if (col.gameObject.tag == "Finish")
        {
            SceneManager.LoadScene(1,LoadSceneMode.Single);
        }*/
    }

    void updateCheckpoint()
    {
        latestCheckpoint = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }

    void GoToCheckpoint()
    {
        //_canMove = true;
        Invoke("EnablePlayerControls",0.3f);
        transform.position = latestCheckpoint;
        GameObject obj = GameObject.FindGameObjectWithTag("MainCamera");
        obj.gameObject.SendMessage("GoToPlayer");
    }

    //Wanha
    /*void  GoToSpawnPoint()
    {
        Vector3 po = transform.position;
        po.x = SpawnPoint.position.x;
        po.y = SpawnPoint.position.y;
        transform.position = po;
    }*/
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
            if(playerRigidbody2D.velocity.y > 0)
            {
                anime.SetInteger("State", 3);
            }
            else
            {
                anime.SetInteger("State", 1);
            }
        }
    }

    void EnablePlayerControls()
    {
        _canMove = true;
    }

}
