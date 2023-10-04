using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public bool isDestructible = false;  // 是否可以被炸毁
    public Sprite[] healthSprites;       // 生命值变化对应的贴图数组
    protected int currentHealth;         // 当前生命值

    protected virtual void Start()
    {
        currentHealth = healthSprites.Length; // 初始化生命值为贴图数组的长度
    }

    public virtual void TakeDamage(int damage)
    {
        if (isDestructible)
        {
            currentHealth -= damage;

            // 如果生命值小于等于0，销毁障碍物
            if (currentHealth <= 0)
            {
                Destroy(gameObject);
            }
            else
            {
                // 更新贴图
                UpdateSprite();
            }
        }
    }

    protected virtual void UpdateSprite()
    {
        // 根据当前生命值更新贴图
        if (currentHealth > 0 && currentHealth <= healthSprites.Length)
        {
            GetComponent<SpriteRenderer>().sprite = healthSprites[currentHealth - 1];
        }
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

}
