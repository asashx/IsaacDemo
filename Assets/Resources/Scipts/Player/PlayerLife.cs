using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLife : MonoBehaviour
{
    public Canvas canvas;

    private GameObject heart1, heart2, heart3;
    private GameObject halfHeart1, halfHeart2, halfHeart3;
    private GameObject emptyHeart1, emptyHeart2, emptyHeart3;

    public int health;
    public int maxHealth = 6;
    private float invincibleTime = 1f;
    private bool isInvincible = false;
    private Transform head;
    private Transform body;

    void Start()
    {
        head = transform.Find("Head");
        body = transform.Find("Body");
        
        // 在 Canvas 下寻找子物体 Heart1、Heart2、Heart3
        heart1 = canvas.transform.Find("Heart1").gameObject;
        heart2 = canvas.transform.Find("Heart2").gameObject;
        heart3 = canvas.transform.Find("Heart3").gameObject;

        // 在 Canvas 下寻找子物体 halfHeart1、halfHeart2、halfHeart3
        halfHeart1 = canvas.transform.Find("HalfHeart1").gameObject;
        halfHeart2 = canvas.transform.Find("HalfHeart2").gameObject;
        halfHeart3 = canvas.transform.Find("HalfHeart3").gameObject;

        // 在 Canvas 下寻找子物体 emptyHeart1、emptyHeart2、emptyHeart3
        emptyHeart1 = canvas.transform.Find("EmptyHeart1").gameObject;
        emptyHeart2 = canvas.transform.Find("EmptyHeart2").gameObject;
        emptyHeart3 = canvas.transform.Find("EmptyHeart3").gameObject;

        health = maxHealth;
        LifeCount();
    }

    void Update()
    {

    }

    public void GetHeal(int heal)
    {
        health += heal;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
        LifeCount();
    }

    public void TakeDamage(int damage)
    {
        if (isInvincible)
        {
            return;
        }
        else if (!isInvincible)
        {
            health -= damage;
            isInvincible = true;
            
            LifeCount();
            
            // 获取头部和身体的Animator组件
            Animator headAnimator = head.GetComponent<Animator>();
            Animator bodyAnimator = body.GetComponent<Animator>();

            // 触发头部和身体的"IsHurt"触发器
            headAnimator.SetTrigger("isHurt");
            bodyAnimator.SetTrigger("isHurt");

            Invoke("ResetInvincible", invincibleTime);
        }  
    }

    void ResetInvincible()
    {
        isInvincible = false;
    }

    void Gameover()
    {

    }

    void LifeCount()
    {
        if (health <= 0)
        {
            Gameover();
        }
        switch (health)
        {
            case 6:
                heart1.SetActive(true);
                heart2.SetActive(true);
                heart3.SetActive(true);
                halfHeart1.SetActive(false);
                halfHeart2.SetActive(false);
                halfHeart3.SetActive(false);
                emptyHeart1.SetActive(false);
                emptyHeart2.SetActive(false);
                emptyHeart3.SetActive(false);
                break;
            case 5:
                heart1.SetActive(true);
                heart2.SetActive(true);
                heart3.SetActive(false);
                halfHeart1.SetActive(false);
                halfHeart2.SetActive(false);
                halfHeart3.SetActive(true);
                emptyHeart1.SetActive(false);
                emptyHeart2.SetActive(false);
                emptyHeart3.SetActive(false);
                break;
            case 4:
                heart1.SetActive(true);
                heart2.SetActive(true);
                heart3.SetActive(false);
                halfHeart1.SetActive(false);
                halfHeart2.SetActive(false);
                halfHeart3.SetActive(false);
                emptyHeart1.SetActive(false);
                emptyHeart2.SetActive(false);
                emptyHeart3.SetActive(true);
                break;
            case 3:
                heart1.SetActive(true);
                heart2.SetActive(false);
                heart3.SetActive(false);
                halfHeart1.SetActive(false);
                halfHeart2.SetActive(true);
                halfHeart3.SetActive(false);
                emptyHeart1.SetActive(false);
                emptyHeart2.SetActive(false);
                emptyHeart3.SetActive(true);
                break;
            case 2:
                heart1.SetActive(true);
                heart2.SetActive(false);
                heart3.SetActive(false);
                halfHeart1.SetActive(false);
                halfHeart2.SetActive(false);
                halfHeart3.SetActive(false);
                emptyHeart1.SetActive(false);
                emptyHeart2.SetActive(true);
                emptyHeart3.SetActive(true);
                break;
            case 1:
                heart1.SetActive(false);
                heart2.SetActive(false);
                heart3.SetActive(false);
                halfHeart1.SetActive(true);
                halfHeart2.SetActive(false);
                halfHeart3.SetActive(false);
                emptyHeart1.SetActive(false);
                emptyHeart2.SetActive(true);
                emptyHeart3.SetActive(true);
                break;
            case 0:
                heart1.SetActive(false);
                heart2.SetActive(false);
                heart3.SetActive(false);
                halfHeart1.SetActive(false);
                halfHeart2.SetActive(false);
                halfHeart3.SetActive(false);
                emptyHeart1.SetActive(true);
                emptyHeart2.SetActive(true);
                emptyHeart3.SetActive(true);
                break;
        }
    }  
}
