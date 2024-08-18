using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpheight;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    private Rigidbody2D body;
    private Animator anim;
    private heath playerHeath;

    private BoxCollider2D boxcollider;
    private float walljumpCooldown;
    private float horizontalInput;

    private bool moveLeft;
    private bool moveRight;


    [Header("sound")]
    [SerializeField] private AudioClip jumpsound;
    private void Awake()
    {
        Application.targetFrameRate = 60;
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxcollider = GetComponent<BoxCollider2D>();
        playerHeath = GetComponent<heath>();
    }

    private void Update()
    {
        if (moveLeft)
        {
            horizontalInput = -1f;
        }
        else if (moveRight)
        {
            horizontalInput = 1f;
        }
        else
        {
            horizontalInput = 0f;
        }

        if (horizontalInput > 0.01f)
            transform.localScale = Vector3.one;
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);

        //Set animator parameters
        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("grounded", isGround());


        if (Input.GetKey(KeyCode.Space))
            TryJump();

        //Wall jump logic
        //if (walljumpCooldown > 0.2f)
        //{
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

        //if (onwall() && !isGround())
        //{
        // body.gravityScale = 0;
        //body.velocity = Vector2.zero;
        // }
        //else
        //body.gravityScale = 7;


        //}
        //else
        //walljumpCooldown += Time.deltaTime;
    }


    public void TryJump()
    {
        if (!playerHeath.dead)
        {
            Jump();
        }
    }

    public void MoveLeft()
    {
        moveLeft = true;
    }

    public void MoveRight()
    {
        moveRight = true;
    }

    public void StopMoving()
    {
        moveLeft = false;
        moveRight = false;
    }


    public void Jump()
    {
        if (isGround())
        {
            body.velocity = new Vector2(body.velocity.x, jumpheight);
            anim.SetTrigger("jump");
        }
        //else if (onwall() && !isGround())
        //{
        //if (horizontalInput == 0)
        //{
        //body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 10, 0);
        //transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        //}
        //else
        //body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 3, 6);

        //walljumpCooldown = 0;
        //}
    }

    private bool isGround()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxcollider.bounds.center, boxcollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    //private bool onwall()
    //{
    //RaycastHit2D raycastHit = Physics2D.BoxCast(boxcollider.bounds.center, boxcollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
    // return raycastHit.collider != null;
    //}

    public bool canAttack()
    {
        return horizontalInput == 0 && isGround();//&& !onwall();
    }
}

    
