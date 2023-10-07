using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProperty : MonoBehaviour
{
    public int coinNum = 0;
    public int bombNum = 1;
    public int keyNum = 0;

    // private float speed = 1f;
    // private float attack = 3.5f;
    // private float attackSpeed = 2.73f;
    // private float attackRange = 6.5f;
    void Start()
    {
        coinNum = 0;
        bombNum = 1;
        keyNum = 0;
        
        UIManager.Instance.UpdateCoinText(coinNum);
        UIManager.Instance.UpdateBombText(bombNum);
        UIManager.Instance.UpdateKeyText(keyNum);
    }

    public void AddCoin(int num)
    {
        coinNum += num;
        UIManager.Instance.UpdateCoinText(coinNum);
    }

    public void AddBomb(int num)
    {
        bombNum += num;
        UIManager.Instance.UpdateBombText(bombNum);
    }

    public void AddKey(int num)
    {
        keyNum += num;
        UIManager.Instance.UpdateKeyText(keyNum);
    }
}
