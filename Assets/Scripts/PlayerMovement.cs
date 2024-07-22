using System;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpSpeed = 8f;
    [SerializeField] private float climbSpeed = 5f;
    [SerializeField] private Vector2 deathKick = new Vector2(10f,10f);
    private float startGravityScale;
    private Rigidbody2D rb;
    Vector2 moveInput;

    Animator animator;
    
    CapsuleCollider2D bodyCollider2D;
    BoxCollider2D feetCollider;

    bool isAlive = true;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        bodyCollider2D = GetComponent<CapsuleCollider2D>();
        feetCollider = GetComponent<BoxCollider2D>();
        startGravityScale = rb.gravityScale;
    }

    void Update() {
        if (isAlive){  
            Run();
            FlipSprite();
            ClimbLadder();
            Die();
        }
    }

    void OnMove(InputValue inputValue){
        if (isAlive){
            moveInput = inputValue.Get<Vector2>();
        }
        // if (moveInput.x < 0) {
        //     transform.localScale = new Vector3(-1,transform.localScale.y,transform.localScale.z);
        // } else if(moveInput.x > 0){
        //     transform.localScale = new Vector3(1,transform.localScale.y,transform.localScale.z);
        // }
    }

    void OnJump(InputValue inputValue){
        if (isAlive){
            if (inputValue.isPressed && feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))){
                rb.velocity += new Vector2(0f,jumpSpeed);
            }
        }
    }

    void Run(){
        Vector2 playerVelocity = new Vector2(moveInput.x * moveSpeed,rb.velocity.y);
        rb.velocity = playerVelocity ;

        bool playerHasHorizontalSpeed = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
        animator.SetBool("isRunning",playerHasHorizontalSpeed);
    }

    private void FlipSprite() {
        bool playerHasHorizontalSpeed = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
        if (playerHasHorizontalSpeed){
            transform.localScale = new Vector2(Mathf.Sign(rb.velocity.x),1f);
        }
    }

    private void ClimbLadder(){
        if (feetCollider.IsTouchingLayers(LayerMask.GetMask("Ladder"))) {
            Vector2 climbVelocity = new Vector2(rb.velocity.x,moveInput.y * climbSpeed);
            rb.velocity = climbVelocity ;
            bool playerHasVerticalSpeed = Mathf.Abs(rb.velocity.y) > Mathf.Epsilon;
            animator.SetBool("isClimbing",playerHasVerticalSpeed);   
            rb.gravityScale = 0f;
        } else {
            rb.gravityScale = startGravityScale;
            animator.SetBool("isClimbing",false);   
        }
    }

    void Die(){
        if (bodyCollider2D.IsTouchingLayers(LayerMask.GetMask("Enemies"))) {
            isAlive = false;
            animator.SetTrigger("Dying");
            rb.velocity = deathKick;
        }
    }
}
