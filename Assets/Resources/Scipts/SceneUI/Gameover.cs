using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gameover : MonoBehaviour
{
    private GameObject gameoverPanel;

    // 在 Start 方法中获取 GameoverPanel 对象的引用
    void Start()
    {
        gameoverPanel = transform.Find("Gameover").gameObject;
    }

    // 在 Update 方法中处理用户输入
    void Update()
    {
        // 检测是否有任意键按下
        // if (Input.GetKeyDown(KeyCode.Space))
        // {
        //     // 先禁用 GameoverPanel，以确保加载动画可以正常显示
        //     SetActiveGameoverPanel(false);

        //     // 播放加载动画
        //     LoadManager.instance.PlayLoading();

        //     // 使用协程等待2秒后加载游戏场景
        //     StartCoroutine(LoadGameSceneAfterDelay(2f));
        // }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameoverPanel.SetActive(false);
            Time.timeScale = 1;
            // 使用协程等待2秒后加载开始场景
            StartCoroutine(LoadStartSceneAfterDelay(2f));
        }
    }

    // // 协程：等待一定时间后加载游戏场景
    // private IEnumerator LoadGameSceneAfterDelay(float delay)
    // {
    //     yield return new WaitForSeconds(delay);
    //     SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    // }

    // 协程：等待一定时间后加载开始场景
    private IEnumerator LoadStartSceneAfterDelay(float delay)
    {
        LoadManager.instance.PlayLoading();
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("StartScene");
    }
}
