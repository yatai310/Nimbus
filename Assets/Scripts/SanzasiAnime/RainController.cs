using UnityEngine;

public class RainController : MonoBehaviour
{
    public static RainController Instance { get; private set; }//Instance にこのクラスのインスタンス（＝RainController がアタッチされたオブジェクト）を保持。

    public GameObject rainObject;
    public AudioSource rainAudio;

    void Awake(){
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
            rainAudio.loop = true;
            rainAudio.Stop();  // 雨音を止めておく
        }
    }

    public void RainActive(){
        if (rainObject != null){
            rainObject.SetActive(true);
        }
        if (rainAudio != null && !rainAudio.isPlaying){
            rainAudio.Play();
        }
    }

    public void RainStop(){
        if (rainObject != null){
            rainObject.SetActive(false);
        }
        if (rainAudio != null && rainAudio.isPlaying){
            rainAudio.Stop();
        }
    }

    void FixedUpdate(){
        int score = ScoreBoard.Instance.getScore();
        var emission = rainObject.GetComponent<ParticleSystem>().emission;//コンポーネントを得る
        var ps = rainObject.GetComponent<ParticleSystem>();
        var main = ps.main;
        //250点まで小雨
        if(score <= 250){
            Debug.Log("小雨");
            main.startLifetime = 1.5f;
            emission.rateOverTime = 50f;
            main.startSpeed = -20f;
        }else if(250 < score && score <= 500){
            Debug.Log("本降り");
            emission.rateOverTime = 250f;
            main.startSpeed = -50f;
            main.startLifetime = 0.6f;
        }else if(500 < score){
            Debug.Log("最大雨");
            emission.rateOverTime = 800f;
            main.startSpeed = -100f;
            main.startLifetime = 0.6f;
        }
    }
}

