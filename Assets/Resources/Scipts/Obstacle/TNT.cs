using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TNT : Obstacle
{
    public GameObject explosionPrefab;
    public float explosionRadius = 1f;
    public float explosionForce = 15f;
    protected override void Start()
    {
        isAttackable = true;
    }

    protected override void Die()
    {
        Destroy(gameObject);
        Explode();
    }

    void Explode()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

        foreach(Collider2D collider in colliders)
        {
            if (collider.CompareTag("Player") || collider.CompareTag("Enemy") || collider.CompareTag("Item") || collider.CompareTag("Bomb"))
            {
                Vector2 explosionDir = collider.transform.position - transform.position;
                explosionDir.Normalize();

                Rigidbody2D rb = collider.GetComponent<Rigidbody2D>();

                if (collider.CompareTag("Player"))
                {
                    rb.AddForce(explosionDir * explosionForce, ForceMode2D.Impulse);
                    PlayerLife playerLife = collider.GetComponent<PlayerLife>();
                    if (playerLife != null)
                    {
                        playerLife.TakeDamage(2);
                    }
                }
                else if (collider.CompareTag("Enemy"))
                {
                    rb.AddForce(explosionDir * explosionForce * 5f, ForceMode2D.Impulse);
                    EnemyBehaviour enemyBehaviour = collider.GetComponent<EnemyBehaviour>();
                    if (enemyBehaviour != null)
                    {
                        enemyBehaviour.TakeDamage(2);
                    }
                }
                else if (collider.CompareTag("Item") || collider.CompareTag("Bomb"))
                {
                    rb.AddForce(explosionDir * explosionForce * 0.1f, ForceMode2D.Impulse);
                }
            }
            if (collider.CompareTag("Obstacle"))
            {
                Obstacle obstacle = collider.GetComponent<Obstacle>();
                if (obstacle != null)
                {
                    obstacle.TakeDamage(10);
                }
            }
        }
        GameObject exp = ObjectPool.Instance.GetObject(explosionPrefab);
        exp.transform.position = transform.position;
    }
}
