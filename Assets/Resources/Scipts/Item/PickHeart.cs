using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PickHeart : Item
{
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerLife playerLife = other.GetComponent<PlayerLife>();
            if (playerLife != null)
            {
                if (playerLife.health < playerLife.maxHealth)
                {
                    int healAmount = Math.Min(2, playerLife.maxHealth - playerLife.health);
                    playerLife.GetHeal(healAmount);
                    PickedUp();
                }
            }
        }
    }
}
