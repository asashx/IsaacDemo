using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class IsaacAttack : MonoBehaviour
{
    public Transform head;
    public Transform body;
    Rigidbody2D rb;
    Animator headAnimation;
    SpriteRenderer headRenderer;
    SpriteRenderer bodyRenderer;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        headAnimation = head.GetComponent<Animator>();
        headRenderer = head.GetComponent<SpriteRenderer>();
        bodyRenderer = body.GetComponent<SpriteRenderer>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // 攻击动画
    private void OnAttack()
    {
        if(Input.GetKey(KeyCode.UpArrow))
        {
            headAnimation.SetBool("attackUp", true);
        }
        else if(Input.GetKey(KeyCode.DownArrow))
        {
            headAnimation.SetBool("attackDown", true);
        }
        else if(Input.GetKey(KeyCode.LeftArrow))
        {
            headAnimation.SetBool("attackLeft", true);
            headRenderer.flipX = true;
        }
        else if(Input.GetKey(KeyCode.RightArrow))
        {
            headAnimation.SetBool("attackRight", true);
            headRenderer.flipX = false;
        }
        else
        {
            headRenderer.flipX = bodyRenderer.flipX;
            headAnimation.SetBool("attackUp", false);
            headAnimation.SetBool("attackDown", false);
            headAnimation.SetBool("attackLeft", false);
            headAnimation.SetBool("attackRight", false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        OnAttack();
    }
}
