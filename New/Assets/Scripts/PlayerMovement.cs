using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontalInput;
    private float jumpTimeCounter;

    public float speed;
    public float jumpForce;
    public float jumpTime;

    private bool facingRight;
    private bool isJumping;

    [SerializeField] bool isOnGround=false;
    [SerializeField] bool jumpUsed;

    public GameObject hookPrefab;

    public Transform hookPrefabPosition;
    

    private Rigidbody2D playerRb;

    private void Awake()
    {
        playerRb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        JumpCode();
        StartTeleport();
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
        void Flip()
        {
            facingRight = !facingRight;
            Vector2 Scaler = transform.localScale;
            Scaler.x *= -1;
            transform.localScale = Scaler;
        }

        if (facingRight == false && horizontalInput > 0)
        {
            Flip();
        }
        else if (facingRight == true && horizontalInput < 0)
        {
            Flip();
        }

    }
   
    void JumpCode()
    {
        if (Input.GetKeyDown(KeyCode.W) && isOnGround)
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            playerRb.velocity = Vector2.up * jumpForce;
            isOnGround = false;
        }

        if (Input.GetKey(KeyCode.W) && isJumping == true)
        {
            if (jumpTimeCounter > 0)
            {
                playerRb.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;  
            }
            else
            {
                isJumping = false;
                isOnGround = false;
            }
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            isJumping = false;
        }
        
    }
   void StartTeleport()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            Instantiate(hookPrefab, hookPrefabPosition.position, hookPrefab.transform.rotation);
        }
    }
    
}
