using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public float moveInterval;            // 移动间隔时间
    public float attackInterval;          // 攻击间隔时间
    public float maxHealth = 10f;        // 最大生命值
    protected float currentHealth;         // 当前生命值
    public float moveSpeed = 2f;         // 移动速度
    public float detectRange = 4f;       // 检测范围
    public GameObject diePrefab;          // 死亡时的粒子效果

    protected Animator animator;
    protected AnimatorStateInfo info;
    protected Rigidbody2D rb;
    protected Transform player;          // 玩家的位置，假设玩家在场景中

    private float moveTimer = 0f;        // 移动计时器
    private float attackTimer = 0f;      // 攻击计时器

    // 在 Start 方法中初始化当前生命值和其他组件
    protected virtual void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;  // 假设玩家使用 "Player" 标签
    }

    // 在 Update 方法中处理周期性的移动和攻击
    protected virtual void Update()
    {
        // 更新移动计时器
        moveTimer += Time.deltaTime;
        // 当移动计时器超过移动间隔时，执行移动行为
        if (moveTimer >= moveInterval)
        {
            PerformMovement();
            moveTimer = 0f;  // 重置计时器
        }

        // 更新攻击计时器
        attackTimer += Time.deltaTime;
        // 当攻击计时器超过攻击间隔时，执行攻击行为
        if (attackTimer >= attackInterval)
        {
            Attack();
            attackTimer = 0f;  // 重置计时器
        }
    }

    // 敌人的行动逻辑，可以在子类中重写
    protected virtual void PerformMovement()
    {
        
    }

    // 敌人的攻击逻辑，可以在子类中重写
    protected virtual void Attack()
    {
        
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            // 获取敌人的 EnemyBehaviour 组件
            EnemyBehaviour enemy = other.GetComponent<EnemyBehaviour>();

            if (enemy != null)
            {
                // 计算敌人的反弹方向
                Vector2 bounceDirection = (other.transform.position - transform.position).normalized;

                // 反弹敌人
                enemy.rb.velocity = bounceDirection * 8f;
            }
        }
        if (other.CompareTag("Player"))
        {
            // 获取玩家的 PlayerLife 组件
            PlayerLife playerLife = other.GetComponent<PlayerLife>();

            if (playerLife != null)
            {
                // 造成伤害，你可以传递伤害值作为参数
                playerLife.TakeDamage(1);

                Rigidbody2D playerRb = other.GetComponent<Rigidbody2D>();

                if (playerRb != null)
                {
                    // 计算玩家的反弹方向
                    Vector2 bounceDirection = (other.transform.position - transform.position).normalized;

                    // 反弹玩家
                    playerRb.velocity = bounceDirection * 8f;
                }
            }
        }
    }

    // 处理敌人受到伤害的方法
    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;

        // 如果生命值小于等于0，调用死亡方法
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // 处理敌人死亡的方法，可以在子类中重写
    protected virtual void Die()
    {
        GameObject die = Instantiate(diePrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
