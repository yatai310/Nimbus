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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

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
        while (!operation.isDone)
        {
            float progress = operation.progress;
            progressBar.value = progress / 0.9f;
            Debug.Log("Loading progress: " + progress);
            if (progress >= 0.9f)
            {
                yield return new WaitForSeconds(1f);
                operation.allowSceneActivation = true;
                NowLoding.SetActive(false);
            }
        }
    }
}
