using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FadeInScript : MonoBehaviour
{
	[SerializeField] private float fadeInTime;
	private Image image;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
		image = transform.Find("Panel").GetComponent<Image>();
		//　コルーチンで使用する待ち時間を計測
		fadeInTime = 1f * fadeInTime / 10f;
		StartCoroutine("FadeIn");
    }

    IEnumerator FadeIn()
    {

        //　Colorのアルファを0.1ずつ下げていく
        for (float i = 1f; i >= 0; i -= 0.1f)
        {
            image.color = new Color(0f, 0f, 0f, i);
            //　指定秒数待つ
            yield return new WaitForSeconds(fadeInTime);
        }
    }

    // Update is called once per frame
        void Update()
    {
        
    }
}
