using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float playerHeight = 0;
    public const float maxVelocity = 2.5f;
    //public const float maxVelocity = 8f; // for testing
    public const float moveSpeed = 15f;
    //public const float moveSpeed = 30f; // for testing
    public const float dragModifier = 0.8f;
    public GameObject arrowPrefab;
    public Animator animator;
    public bool hidden = false;
    public int hiddenInObjects = 0; //counter of objects that we have hidden in
    public bool spotted = false;
    private float xScale;

    private Rigidbody2D rb;
    private Vector2 movement;

    public AudioClip hayClip;
    public AudioClip bushClip;
    public AudioSource swishSource;
    public AudioSource stepSource;

    private void Awake()
    {
        //playerHeight = transform.localScale.y / 2;
        xScale = transform.localScale.x;
        ////print(xScale);
        rb = GetComponent<Rigidbody2D>();
        //animator = GetComponent<Animator>();
        animator = transform.GetChild(0).GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!spotted && GameEvents.gameStarted)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
            animator.SetFloat("speed", movement.magnitude);

            if (movement.magnitude > 0)
            {
                if (!swishSource.loop && hidden)
                {
                    if (!swishSource.isPlaying)
                        swishSource.Play();
                    swishSource.loop = true;
                }
                if (!stepSource.isPlaying)
                {
                    stepSource.Play();
                }
                movement.x = movement.x / movement.magnitude;
                movement.y = movement.y / movement.magnitude;

                if (movement.x > 0 && transform.localScale.x < 0)
                {
                    FlipSprite(false);
                }
                else if (movement.x < 0 && transform.localScale.x > 0)
                {
                    FlipSprite(true);
                }
            } else
            {
                if (swishSource.loop)
                {
                    //swishSource.Pause();
                    swishSource.loop = false;
                }
                if (stepSource.isPlaying)
                {
                    stepSource.Stop();
                    stepSource.time = 0;
                }
            }
            if (Input.GetButtonDown("Fire1"))
            {
                //ShootArrow();
                //Debug.Log("Pressed primary button.");
            }
        } else
        {
            movement = new Vector2(0, 0);
            animator.SetFloat("speed", 0);
        }


    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    print("player collision");
    //}

    private void FlipSprite(bool toLeft)
    {
        Vector3 lTemp = transform.localScale;
        float xScaleToSet = xScale;
        if (toLeft)
        {
            xScaleToSet = -xScale;
        }
        lTemp.x = xScaleToSet;
        transform.localScale = lTemp;
    }

    private void FixedUpdate()
    {
        HandlePlayerMovement();
    }

    private void ShootArrow()
    {
        //print(Input.mousePosition.x + " " + transform.position.x);
        print(Camera.main.ScreenToWorldPoint(Input.mousePosition).x + " " + transform.position.x);
        Vector3 arrowPos = new Vector3(transform.position.x, transform.position.y - playerHeight, 0);
        GameObject arrowObj = Instantiate(arrowPrefab, arrowPos, Quaternion.identity);
        Arrow arrow = arrowObj.GetComponent<Arrow>();
        Vector2 directionVector = new Vector2(Input.mousePosition.x - transform.position.x, Input.mousePosition.y - (transform.position.y - playerHeight)).normalized;
        arrow.Setup(directionVector);
    }

    public void TestPrint()
    {
        print("helloworld");
    }

    private void HandlePlayerMovement()
    {
        rb.AddForce(movement * moveSpeed * 100 * Time.deltaTime);
        // if not moving on horizontal axis
        if (movement.x == 0)
        {
            if (Mathf.Abs(rb.velocity.x) > 0.05f)
            {
                rb.velocity = new Vector2(rb.velocity.x * dragModifier, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
        }
        else
        {
            if (rb.velocity.x > maxVelocity)
            {
                rb.velocity = new Vector2(maxVelocity, rb.velocity.y);
            }
            else if (rb.velocity.x < -1 * maxVelocity)
            {
                rb.velocity = new Vector2(-1 * maxVelocity, rb.velocity.y);
            }
        }

        // if not moving on vertical axis
        if (movement.y == 0)
        {
            if (Mathf.Abs(rb.velocity.y) > 0.05f)
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * dragModifier);
            }
            else
            {
                rb.velocity = new Vector2(rb.velocity.x, 0);
            }
        }
        else
        {
            if (rb.velocity.y > maxVelocity)
            {
                rb.velocity = new Vector2(rb.velocity.x, maxVelocity);
            }
            else if (rb.velocity.y < -1 * maxVelocity)
            {
                rb.velocity = new Vector2(rb.velocity.x, -1 * maxVelocity);
            }
        }
    }

    public void HideInObject()
    {
        hiddenInObjects++;
        transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        hidden = true;
        stepSource.volume = 0;
    }

    public void UnhideFromObject()
    {
        hiddenInObjects--;
        if (hiddenInObjects == 0)
        {
            transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            hidden = false;
            swishSource.Pause();
            stepSource.volume = 0.4f;
        }

    }
}
