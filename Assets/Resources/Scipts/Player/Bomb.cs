using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject explosionPrefab;
    private Animator animator;
    private AnimatorStateInfo info;
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
            Die();
        }
    }

    void Die()
    {
        ObjectPool.Instance.PushObject(gameObject);
        
        GameObject exp = ObjectPool.Instance.GetObject(explosionPrefab);
        exp.transform.position = transform.position;
    }
}
