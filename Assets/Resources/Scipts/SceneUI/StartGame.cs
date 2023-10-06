using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    void Update()
    {
        // 检测是否有任意键按下
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
        {
            LoadManager.instance.PlayLoading();
            // 延时两秒
            Invoke("LoadScene", 2f);
        }
    }

    void LoadScene()
    {
        // 加载场景
        SceneManager.LoadScene("GameScene");
    }
}
