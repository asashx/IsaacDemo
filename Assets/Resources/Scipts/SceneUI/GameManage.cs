using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManage : MonoBehaviour
{
    private Canvas canvas; // 引用Canvas 对象
    private GameObject gameoverPanel; // 游戏结束面板
    private GameObject pausePanel;   // 暂停面板
    private GameObject life;
    private GameObject item;
    private GameObject property;
    private bool isPaused = false;  // 游戏是否已暂停
    public bool isGameOver = false; // 游戏是否已结束
    

    void Start()
    {
        // 初始化，获取Canvas 对象
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        // 初始化，获取游戏结束面板和暂停面板
        gameoverPanel = canvas.transform.Find("Gameover").gameObject;
        pausePanel = canvas.transform.Find("Pause").gameObject;
        // 初始化，获取Life、Item、Property 对象
        life = canvas.transform.Find("Life").gameObject;
        item = canvas.transform.Find("Item").gameObject;
        property = canvas.transform.Find("Property").gameObject;
        // 初始化，将游戏结束和暂停面板都禁用
        gameoverPanel.SetActive(false);
        pausePanel.SetActive(false);
        // 初始化，将Life、Item、Property 对象都启用
        life.SetActive(true);
        item.SetActive(true);
        property.SetActive(true);
    }

    void Update()
    {
        // 检测游戏是否已结束
        if (Input.GetKeyDown(KeyCode.Escape) && !isPaused && !isGameOver)
        {
            PauseGame();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isPaused && !isGameOver)
        {
            ResumeGame();
        }
        if (Input.GetKeyDown(KeyCode.Return) && isPaused)
        {
            ExitGame();
        }
        if (Input.GetKeyDown(KeyCode.Escape) && isGameOver)
        {
            ExitGame();
        }
    }

    // 游戏结束函数
    public void GameOver()
    {
        isGameOver = true; // 游戏结束
        // 显示游戏结束面板
        gameoverPanel.SetActive(true);
        Time.timeScale = 0; // 暂停游戏时间流逝
    }

    // 暂停游戏函数
    void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0; // 暂停游戏时间流逝
        pausePanel.SetActive(true);
    }

    // 恢复游戏函数
    void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1; // 恢复正常游戏时间流逝
        pausePanel.SetActive(false);
    }

    // 退出游戏函数
    void ExitGame()
    {
        isPaused = false;
        Time.timeScale = 1; // 恢复正常游戏时间流逝
        pausePanel.SetActive(false);
        gameoverPanel.SetActive(false);
        StartCoroutine(LoadStartSceneAfterDelay(2f));
    }

    // 加载开始场景的协程
    private IEnumerator LoadStartSceneAfterDelay(float delay)
    {
        LoadManager.instance.PlayLoading();
        ObjectPool.Instance.Clear();
        yield return new WaitForSecondsRealtime(delay);
        isGameOver = false;
        SceneManager.LoadScene("StartScene");
    }
}
