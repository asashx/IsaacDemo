using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodTears : MonoBehaviour
{
    public float speed;
    public float range = 6.5f;

    private float existTime = 0f;
    public GameObject explosionPrefab;
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // 眼泪发射方向
    public void SetSpeed(Vector2 direction)
    {
        rb.velocity = direction * speed;
    }

    // 激活时设置存活时间和图层
    private void OnEnable()
    {
        existTime = 0f;
    }

    void Update()
    {
        existTime += Time.deltaTime;
        if (existTime >= range / speed)
        {
            Die();
        }
    }

    // 碰撞检测
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Wall"))
        {
            Die();
        }
        else if (other.CompareTag("Obstacle"))
        {
            Die();
        }
        else if (other.CompareTag("Player"))
        {
            Die();
            PlayerLife playerLife = other.GetComponent<PlayerLife>();
            if (playerLife != null)
            {
                playerLife.TakeDamage(1);
            }
            Rigidbody2D playerRb = other.GetComponent<Rigidbody2D>();

            if (playerRb != null)
            {
                // 计算玩家的反弹方向
                Vector2 bounceDirection = (other.transform.position - transform.position).normalized;

                // 反弹玩家
                playerRb.velocity = bounceDirection * 5f;
            }
        }
    }

    // 眼泪销毁
    public void Die()
    {
        GameObject exp = ObjectPool.Instance.GetObject(explosionPrefab);
        exp.transform.position = transform.position;

        ObjectPool.Instance.PushObject(gameObject);
    }
}
