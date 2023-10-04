using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UIManager : MonoBehaviour
{
    public GameObject coinText;
    public GameObject bombText;
    public GameObject keyText;
    // 单例实例
    public static UIManager Instance { get; private set; }

    private void Awake()
    {
        // 确保只有一个UIManager实例存在
        if (Instance == null)
        {
            Instance = this; // 设置Instance为当前实例
            DontDestroyOnLoad(gameObject); // 防止在场景切换时销毁
        }
        else
        {
            Destroy(gameObject); // 如果已经存在其他实例，则销毁当前实例
        }
    }

    public void UpdateCoinText(int coin)
    {
        coinText.GetComponent<TextMeshProUGUI>().text = coin.ToString("00");
    }

    public void UpdateBombText(int bomb)
    {
        bombText.GetComponent<TextMeshProUGUI>().text = bomb.ToString("00");
    }

    public void UpdateKeyText(int key)
    {
        keyText.GetComponent<TextMeshProUGUI>().text = key.ToString("00");
    }
}

