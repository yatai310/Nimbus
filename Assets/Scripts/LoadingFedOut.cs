using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class LoadingFedOut : MonoBehaviour
{
    private GameObject NowLoading;
    public Slider ProgressBar;

    private CanvasGroup LoadingCanvasGroup;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        NowLoading = GameObject.Find("NowLoading");
        if (NowLoading != null)
        {
            LoadingCanvasGroup = NowLoading.GetComponent<CanvasGroup>();
            ProgressBar = NowLoading.GetComponentInChildren<Slider>();
        }
        if (ProgressBar == null)
            {
                Debug.Log("ProgressBar Slider is missing.");
                return;
            }
        if (NowLoading == null)
        {
            Debug.Log("NowLoading GameObject is missing.");
            return;
        }
        if (LoadingCanvasGroup == null)
        {
            Debug.Log("LoadingCanvasGroup component is missing.");
            return;
        }
       StartCoroutine(FadOut()); 
    }

    IEnumerator FadOut()
    {
        // フェードアウトの処理
        for (float i = 1f; i > 0f; i -= 0.1f)
        {
            LoadingCanvasGroup.alpha = i;
            yield return new WaitForSeconds(0.01f);
        }
        
        // フェードアウト後にオブジェクトを非表示にする
        NowLoading.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
