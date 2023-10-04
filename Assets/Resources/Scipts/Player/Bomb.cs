using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject explosionPrefab;
    private Animator animator;
    private AnimatorStateInfo info;
    public float explosionRadius = 1f;
    public float explosionForce = 15f;
    public int explosionDamage = 2;

    // private Collider collider;
    // private GameObject player;
    // private float activateDistance = 0.5f;

    void Start()
    {
        animator = GetComponent<Animator>();
        // collider = GetComponent<Collider>();

        // player = GameObject.FindGameObjectWithTag("Player");
    }

    // 爆炸动画播放完毕后回收
    void Update()
    {
        // if(player != null)
        // {
        //     Vector2 playerPosition = player.transform.position;
        //     Vector2 bombPosition = transform.position;
        //     if(Vector2.Distance(playerPosition, bombPosition) >= activateDistance)
        //     {
        //         collider.enabled = true;
        //     }
        // }
        info = animator.GetCurrentAnimatorStateInfo(0);
        if(info.normalizedTime >= 1.0f)
        {
            Explode();
        }
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
                        playerLife.TakeDamage(explosionDamage);
                    }
                }
                else if (collider.CompareTag("Enemy"))
                {
                    rb.AddForce(explosionDir * explosionForce * 5f, ForceMode2D.Impulse);
                    EnemyBehaviour enemyBehaviour = collider.GetComponent<EnemyBehaviour>();
                    if (enemyBehaviour != null)
                    {
                        enemyBehaviour.TakeDamage(explosionDamage);
                    }
                }
                else if (collider.CompareTag("Item") || collider.CompareTag("Bomb"))
                {
                    rb.AddForce(explosionDir * explosionForce * 0.1f, ForceMode2D.Impulse);
                }
            }
        }

        ObjectPool.Instance.PushObject(gameObject);
        
        GameObject exp = ObjectPool.Instance.GetObject(explosionPrefab);
        exp.transform.position = transform.position;
    }
}
