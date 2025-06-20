using UnityEngine;

public class RainController : MonoBehaviour
{
    public static RainController Instance { get; private set; }//Instance にこのクラスのインスタンス（＝RainController がアタッチされたオブジェクト）を保持。

    public GameObject rainObject;
    public AudioSource rainAudio;

    void Awake()
    {
        Debug.Log("RainController Awake 開始");
        if (Instance == null){
            Instance = this;
        }else{
            Destroy(gameObject);
        }
        if (rainObject != null){
            rainObject.SetActive(false);  // 最初は非表示にする
        }
        if (rainObject != null){
            rainAudio.Stop();  // 雨音を止めておく
        }
    }

    public void RainActive()
    {
        if (rainObject != null){
            rainObject.SetActive(true);
        }
        if (rainAudio != null && rainAudio.isPlaying){
            rainAudio.Play();
        }
    }
}

