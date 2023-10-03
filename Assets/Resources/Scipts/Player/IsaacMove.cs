using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class IsaacMove : MonoBehaviour
{
    public Transform head;
    public Transform body;
    Rigidbody2D rb;
    Vector2 moveInput;
    public float Speed;
    Animator animator;
    Animator headAnimation;
    Animator bodyAnimation;
    SpriteRenderer headRenderer;
    SpriteRenderer bodyRenderer;
    private Vector2 playerVelocity;


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        headAnimation = head.GetComponent<Animator>();
        bodyAnimation = body.GetComponent<Animator>();
        headRenderer = head.GetComponent<SpriteRenderer>();
        bodyRenderer = body.GetComponent<SpriteRenderer>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // 移动动画
    private void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        if(moveInput.x < 0)
        {
            headRenderer.flipX = true;
            bodyRenderer.flipX = true;
        }
        else if(moveInput.x >= 0)
        {
            headRenderer.flipX = false;
            bodyRenderer.flipX = false;
        }
        
        if(moveInput.y > 0.05f)
        {
            headAnimation.SetBool("moveUp", true);
        }
        else
        {
            headAnimation.SetBool("moveUp", false);
        }
        if(Mathf.Abs(moveInput.x) >= Mathf.Abs(moveInput.y))
        {
            headAnimation.SetFloat("velocityX", Mathf.Abs(moveInput.x));
            bodyAnimation.SetFloat("velocityX", Mathf.Abs(moveInput.x));
            headAnimation.SetFloat("velocityY", 0);
            bodyAnimation.SetFloat("velocityY", 0);
        }
        else if(Mathf.Abs(moveInput.x) < Mathf.Abs(moveInput.y))
        {
            headAnimation.SetFloat("velocityY", Mathf.Abs(moveInput.y));
            bodyAnimation.SetFloat("velocityY", Mathf.Abs(moveInput.y));
            headAnimation.SetFloat("velocityX", 0);
            bodyAnimation.SetFloat("velocityX", 0);
        }      
        
    }
    
    private void FixedUpdate()
    {
        rb.AddForce(moveInput * Speed);
        playerVelocity = rb.velocity;
    }

    // 获取人物的速度
    public Vector2 GetVelocity()
    {
        return playerVelocity;
    }

}
