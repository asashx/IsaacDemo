using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadManager : MonoBehaviour
{
    public static LoadManager instance;

    public GameObject loadAni;
    private Animator ani;
    // private Transform mainCameraTransform;

    // 在 Awake 方法中初始化单例
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // 不销毁这个 GameObject
        }
        else
        {
            Destroy(gameObject); // 如果已经存在其他实例，则销毁这个新的实例
        }
    }

    private void Start()
    {
        // mainCameraTransform = GameObject.Find("Main Camera").transform;
    }

    // 在这里编写播放 load 动画的方法或其他任务
    public void PlayLoading()
    {
        // if (SceneManager.GetActiveScene().name == "GameScene")
        // {
        //     // 设置加载动画的位置为主相机的位置
        //     transform.position = mainCameraTransform.position;
        // }
        StartCoroutine(LoadingCoroutine());
    }

    // 在这里编写播放 load 动画的协程
    IEnumerator LoadingCoroutine()
    {
        ani = GetComponent<Animator>();
        ani.SetBool("isLoading", true);
        yield return new WaitForSeconds(1f);
        loadAni.SetActive(true);
        yield return new WaitForSeconds(1f);
        loadAni.SetActive(false);
        yield return new WaitForSeconds(1f);
        ani.SetBool("isLoading", false);
    }
}
