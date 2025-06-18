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
	public Image bgImage;
    public Image ProgressImage;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Awake()
    {
        // DontDestroyOnLoad(this.gameObject);
        DontDestroyOnLoad(NowLoading);
    }
    void Start()
    {
        // 背景画像の取得
        bgImage = GameObject.Find("BackgroundImage").GetComponent<Image>();
        // 画像の色を透明に設定
        bgImage.color = new Color(0f, 0f, 0f, 0f);
        // ローディングオブジェクトを非表示にする
        NowLoading.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

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
            bgImage.color = new Color(1f, 1f, 1f, i);
            ProgressImage.color = new Color(88f / 255f,233f / 255f, 30f / 255f, i);
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
