using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : Obstacle
{
    protected override void Start()
    {
        isDestructible = false;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // 伤害玩家
            other.GetComponent<PlayerLife>().TakeDamage(1);
        }
    }
}
