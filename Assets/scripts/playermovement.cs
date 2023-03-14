 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playermovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private float horizontal;

    public float playerspeed = 2;
    public float jumpforce = 2;

    public float raycastLength = 2;

    public bool isGrounded;
    public LayerMask groundLayerMask;
    public Transform respawnpoint;

    private SpriteRenderer spriteRenderer;
    private Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        respawnpoint.position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(horizontal * playerspeed, rb.velocity.y);
        horizontal = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, y: jumpforce);

        }

        if (rb.velocity.x != 0)
        {
            anim.SetBool("isMoving", true);

        }
        else
        {
            anim.SetBool("isMoving", false);
        }

        if (horizontal < 0)
        {
            spriteRenderer.flipX = true;
        }

        else if (horizontal > 0)
        {
            spriteRenderer.flipX = false;
        }

        //to check whether the player is on the ground
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, raycastLength, groundLayerMask);
        Debug.DrawRay(transform.position, Vector3.down * raycastLength, Color.green);


        // to animate jumping   
        anim.SetBool("isGrounded", isGrounded);



    }

    //to collect coins

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Coin")
        {
            Destroy(other.gameObject);
        }

        if (other.tag == "Respawn")
        {
            Respawn();
        }

        void Respawn()
        {
            transform.position = respawnpoint.position;
        }
    }
}    