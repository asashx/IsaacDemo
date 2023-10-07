using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    private GameObject life; // 引用Life 对象
    private GameObject item; // 引用Item 对象
    private GameObject property; // 引用Property 对象

    void Start()
    {
        life = GameObject.Find("Life");
        item = GameObject.Find("Item");
        property = GameObject.Find("Property");
        if (life != null)
        {
            life.SetActive(false);
        }
        if (item != null)
        {
            item.SetActive(false);
        }
        if (property != null)
        {
            property.SetActive(false);
        }
    }
    void Update()
    {
        // 检测是否有任意键按下
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
        {
            LoadManager.instance.PlayLoading();
            // 延时两秒
            Invoke("LoadScene", 2f);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    void LoadScene()
    {
        // 在加载新场景之前清空对象池
        ObjectPool.Instance.Clear();
        // 加载场景
        SceneManager.LoadScene("GameScene");
    }
}
