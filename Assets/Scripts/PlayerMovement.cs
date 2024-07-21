using System;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    Vector2 moveInput;
    private Rigidbody2D rb;
    Animator animator;
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update() {
        Run();
        FlipSprite();
    }

    void OnMove(InputValue inputValue){
        moveInput = inputValue.Get<Vector2>();
        // if (moveInput.x < 0) {
        //     transform.localScale = new Vector3(-1,transform.localScale.y,transform.localScale.z);
        // } else if(moveInput.x > 0){
        //     transform.localScale = new Vector3(1,transform.localScale.y,transform.localScale.z);
        // }
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
            transform.localScale = new Vector2(Math.Sign(rb.velocity.x),1f);
        }
    }
}
