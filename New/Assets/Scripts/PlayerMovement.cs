using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontalInput;
    private float verticallInput;
    private float jumpTimeCounter;

    public float speed;
    public float jumpForce;
    public float jumpTime;

    private bool facingRight;
    private bool isJumping;
    
    Vector2 movement;
    public Animator animator;

    [SerializeField] bool isOnGround=false;
    [SerializeField] bool jumpUsed;

    public GameObject hookPrefab;

    public Transform hookPrefabPosition;
    private Transform player;

    private Rigidbody2D playerRb;
    

    private void Awake()
    {
        playerRb = GetComponent<Rigidbody2D>();
        
    }

    private void Update()
    {
        //
        JumpCode();
        StartTeleport();
        Debug.Log(verticallInput);
        Debug.Log(horizontalInput);
        //animasyonlar
        animator.SetFloat("Horizontal", horizontalInput);
        animator.SetFloat("Vertical", verticallInput);
        animator.SetFloat("Speed", movement.sqrMagnitude);

        if (Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Vertical") == 1)
        {
            animator.SetFloat("LastHorizontal", Input.GetAxisRaw("Horizontal"));
            animator.SetFloat("LastVertical", Input.GetAxisRaw("Vertical"));
        }
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = Camera.main.transform.position.z + Camera.main.nearClipPlane;
            transform.position = mousePosition;
        }
         







    }
    private void FixedUpdate()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticallInput = Input.GetAxisRaw("Vertical");
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
        movement = playerRb.velocity / 10;
        
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
