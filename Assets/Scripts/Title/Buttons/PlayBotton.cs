using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class PlayBotton : MonoBehaviour
{
    public static string StageName = "GameScene";
    public Slider progressBar;
    public GameObject NowLoding;
	public Image bgImage;
    public Image progressImage;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // 背景画像の取得
        bgImage = GameObject.Find("BackgroundImage").GetComponent<Image>();
        // 画像の色を透明に設定
        bgImage.color = new Color(0f, 0f, 0f, 0f);
        // ローディングオブジェクトを非表示にする
        NowLoding.SetActive(false);
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
        NowLoding.SetActive(true);
         // 背景画像のフェードイン
        for(float i = 0f; i < 1f; i += 0.1f)
        {
            bgImage.color = new Color(0f, 0f, 0f, i);
            progressImage.color = new Color(88f / 255f,233f / 255f, 30f / 255f, i);
            yield return new WaitForSeconds(0.025f);
        }
        while (!operation.isDone)
        {
            float progress = operation.progress;
            progressBar.value = progress / 0.9f;
            Debug.Log("Loading progress: " + progress);
            if (progress >= 0.9f)
            {
                yield return new WaitForSeconds(1f);
                // 背景画像のフェードアウト
                for (float i = 1f; i >= 0f; i -= 0.1f)
                {
                    bgImage.color = new Color(0f, 0f, 0f, i);
                    progressImage.color = new Color(88f / 255f, 233f / 255f, 30f / 255f, i);
                    yield return new WaitForSeconds(0.025f);
                }
                NowLoding.SetActive(false);
                yield return null;
                operation.allowSceneActivation = true;
            }
        }
    }
}
