using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed;
    public GameObject panel;
    public Vector2 lastMove;
    public float attackingTime;
    public string startPoint;

    private Animator anim;
    private Rigidbody2D myBody;
    private bool playerMoving;
    private static bool playerExist;
    private bool attacking;
    private float attackingTimeCounter;


    void Start()
    {
        anim = GetComponent<Animator>();
        myBody = GetComponent<Rigidbody2D>();

        if (!playerExist)
        {
            playerExist = true;
            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    void Update()
    {
        if(!attacking)
        {
            if(Input.GetAxisRaw("Horizontal") > 0.5f || Input.GetAxisRaw("Horizontal") < -0.5f)
            {
                myBody.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * moveSpeed, myBody.velocity.y);
                playerMoving = true;
                lastMove = new Vector2(Input.GetAxisRaw("Horizontal"), 0f);
            }

            if (Input.GetAxisRaw("Vertical") > 0.5f || Input.GetAxisRaw("Vertical") < -0.5f)
            {
                myBody.velocity = new Vector2( myBody.velocity.x, Input.GetAxisRaw("Vertical") * moveSpeed);
                playerMoving = true;
                lastMove = new Vector2(0f, Input.GetAxisRaw("Vertical"));
            }

            if (Input.GetAxisRaw("Horizontal") < 0.5f && Input.GetAxisRaw("Horizontal") > -0.5f)
            {
                myBody.velocity = new Vector2(0f, myBody.velocity.y);
            }

            if (Input.GetAxisRaw("Vertical") < 0.5f && Input.GetAxisRaw("Vertical") > -0.5f)
            {
                myBody.velocity = new Vector2( myBody.velocity.x,0f);
            }
        }


        if(attackingTimeCounter>0)
        {
            attackingTimeCounter -= Time.deltaTime;
        }

        if(attackingTimeCounter <= 0 )
        {
            attacking = false;
            anim.SetBool("PlayerAttacking",false);
        }

        anim.SetFloat("MoveX",Input.GetAxisRaw("Horizontal"));
        anim.SetFloat("MoveY", Input.GetAxisRaw("Vertical"));
        anim.SetBool("PlayerMoving", playerMoving);
        anim.SetFloat("LastMoveX",lastMove.x);
        anim.SetFloat("LastMoveY", lastMove.y);

    }
}
