using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tears : MonoBehaviour
{
    public float damage;
    public float speed;
    public float range = 6.5f;
    private float existTime = 0f;
    public GameObject explosionPrefab;
    private Rigidbody2D rb;
    private Vector2 playerVelocity;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // 眼泪发射方向
    public void SetSpeed(Vector2 direction)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            // 获取人物的速度
            IsaacMove isaacMove = player.GetComponent<IsaacMove>();
            if (isaacMove != null)
            {
                playerVelocity = isaacMove.GetVelocity();
            }
        }
        rb.velocity = direction * speed + playerVelocity * 0.3f;
    }

    // 激活时设置存活时间和图层
    private void OnEnable()
    {
        existTime = 0f;
        GetComponent<SpriteRenderer>().sortingLayerName = "Player";
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
        else if (other.CompareTag("Bomb"))
        {
            Die();
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.AddForce((other.transform.position - transform.position) * 400f);
            }
        }
        else if (other.CompareTag("Enemy"))
        {
            Die();
            EnemyBehaviour enemyBehaviour = other.GetComponent<EnemyBehaviour>();
            if (enemyBehaviour != null)
            {
                enemyBehaviour.TakeDamage(damage);
            }
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.AddForce((other.transform.position - transform.position) * 1000f);
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
