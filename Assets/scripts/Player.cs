using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    /*
    *    PUBLIC VARIABLES
    *   explain why these are public
    */
    public float speed;
    public float jumpForce;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;
    public int extraJumps;


    /*
    *    PRIVATE VARIABLES
    *   explain why these are private
    */
    private float moveInput;
    private Rigidbody2D rb;
    private bool facingRight = true;
    private int resetJumps;
    private bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        //because it's private, this is one way we can give it a value
        //because we don't specify what gameobject, it refers to whatever object
        //      the script is attached to
        rb = GetComponent<Rigidbody2D>();
        resetJumps = extraJumps;
    }

    // Update is called once per frame (by default, functions are private)
    void Update()
    {
        moveInput = Input.GetAxis("Horizontal");
        //Debug.Log(moveInput); //you can uncomment this line to see how moveInput's values change

        //rb.velocity.y just means whatever your current y velocity is, use it
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        //flipping logic 
        //yes there are lots of shortcuts you can use to shorten this, but the goal is to really explain the concepts
        // ie don't try to flex your knowldge on your students
        if(facingRight == false && moveInput > 0){
            Flip();
        }
        else if(facingRight == true && moveInput < 0){
            Flip();
        }

        //jumping logic (using isGrounded from update)
        if(isGrounded){ //make sure to explain why we can do this (can also be written as, isGrounded == true)
            extraJumps = resetJumps;
        }
        if(Input.GetKeyDown(KeyCode.UpArrow) && extraJumps > 0){
            //vector2.up is just a (0,1) vector
            rb.velocity = Vector2.up * jumpForce;

            //show different ways of writing this line of code
            extraJumps--;
        }
        else if(Input.GetKeyDown(KeyCode.UpArrow) && extraJumps == 0 && isGrounded){
            //vector2.up is just a (0,1) vector
            rb.velocity = Vector2.up * jumpForce;
        }
    }
    void FixedUpdate() {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius,whatIsGround);
        
    }
    void Flip(){
        facingRight = !facingRight; //switches bool variable back and forth
        Vector3 scaler = transform.localScale;  //creates temp variable equal to players scale values
        scaler.x *= -1; //sets the temps x value to be either positive or negative
        transform.localScale = scaler; //sets players scale to the temp variable
    }
}
