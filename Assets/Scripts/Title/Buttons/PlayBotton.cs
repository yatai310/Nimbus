using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class PlayBotton : MonoBehaviour
{
    public static string StageName = "GameScene";
    public Slider ProgressBar;
    public GameObject NowLoading;
    public CanvasGroup LoadingCanvasGroup;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Awake()
    {
        DontDestroyOnLoad(NowLoading);
    }
    void Start()
    {
        NowLoading.SetActive(false);
    }
    public void Scenemg()
    {
        Debug.Log("Scenemg");
        StartCoroutine(LoadSceneAsync());
    }
    IEnumerator LoadSceneAsync()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(StageName);
        operation.allowSceneActivation = false;
        NowLoading.SetActive(true);
         // 背景画像のフェードイン
        for(float i = 0f; i < 1f; i += 0.1f)
        {
            LoadingCanvasGroup.alpha = i + 0.1f;
            yield return new WaitForSeconds(0.025f);
        }
        yield return new WaitForSeconds(0.05f);
        while (!operation.isDone)
        {
            float progress = operation.progress;
            ProgressBar.value = progress / 0.9f;
            Debug.Log("Loading progress: " + progress);
            if (progress >= 0.9f)
            {
                yield return new WaitForSeconds(1f);
                operation.allowSceneActivation = true;
            }
        }
    }
}
