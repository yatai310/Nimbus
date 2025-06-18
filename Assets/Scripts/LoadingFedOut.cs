using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class LoadingFedOut : MonoBehaviour
{
    private Slider ProgressBar;
    private GameObject NowLoading;
	private Image BackgroundImage;
    private Image ProgressImage;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ProgressBar = GameObject.Find("ProgressSlider").GetComponent<Slider>();
        NowLoading = GameObject.Find("NowLoading");
        BackgroundImage  = GameObject.Find("BackgroundImage").GetComponent<Image>();
        ProgressImage = ProgressBar.GetComponentInChildren<Image>();
        
        if (ProgressBar == null || NowLoading == null || BackgroundImage == null || ProgressImage == null)
        {
            Debug.LogError("One or more required components are missing.");
            return;
        }
       StartCoroutine(FadOut()); 
    }

    IEnumerator FadOut()
    {
        // フェードアウトの処理
        for (float i = 1f; i > 0f; i -= 0.1f)
        {
            BackgroundImage.color = new Color(1f, 1f, 1f, i);
            ProgressImage.color = new Color(88f / 255f,233f / 255f, 30f / 255f, i);

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
