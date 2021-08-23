using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontalInput;
    public float speed;
    public float jumpForce;

    private bool facingRight;
    [SerializeField] bool isOnGround=false;
    [SerializeField] bool jumpUsed;

    private Rigidbody2D playerRb;

    private void Awake()
    {
        playerRb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        JumpCode();
    }
    private void FixedUpdate()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        CharacterMovement();
        Flipper();  
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        }
       
    }
    void CharacterMovement()
    {
        playerRb.velocity = new Vector2(horizontalInput * speed, playerRb.velocity.y);
        
    }

    void Flipper()  
    {
        if (facingRight == false && horizontalInput > 0)
        {
            Flip();
        }
        else if (facingRight == true && horizontalInput < 0)
        {
            Flip();
        }
    }
    void Flip()
    {
        facingRight = !facingRight;
        Vector2 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
    void JumpCode()
    {
        if (Input.GetKeyDown(KeyCode.W) && isOnGround)
        {
            playerRb.velocity = Vector2.up * jumpForce;
            isOnGround = false;
        }
    }
   
    
}
