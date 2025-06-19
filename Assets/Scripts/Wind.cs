using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Wind : MonoBehaviour
{
    public float forceRadius = 15f;//風の影響範囲（半径）
    public float attractionForce = 10f;//coreから移植、引力
    public LayerMask targetLayer;//風の影響を受ける対象レイヤー
    public float magnification;

    private List<Vector2> windForces = new List<Vector2>();//各時間ごとの風力ベクトルを格納
    private int currentWindIndex;//現在適用中の風データのインデックス
    private float timer;//風の切り替え用のタイマー

    private string apiKey = "48dbfd08356661d251c65c60b4f5ee3f";
    private string url = "https://pro.openweathermap.org/data/2.5/forecast/hourly?lat=35&lon=136&units=metric&cnt=24&appid=";//緯度経度指定で場所変更

    private Vector2 currentWind = Vector2.zero;//現在の風力（方向と強さを含む）

    [System.Serializable] public class WindData//時間ごとの風データを表すクラス
    {
        public float speed;//風速（m/s）
        public float deg;//風向（度数法で表現、北を0度として時計回り）
        public float gust;//突風（使用していないがAPIには含まれる）
    }
    [System.Serializable] public class HourlyData
    {
        public WindData wind;
        public string dt;
    }
    [System.Serializable] public class ForecastData
    {
        public List<HourlyData> list;
    }
    void Start()
    {
        timer = 0f;
        currentWindIndex = 0;
        StartCoroutine(FetchWindData());//ゲーム開始時にAPIから風データを取得するコルーチン、もしかしたらここで()内にURLいれないと無理なの？
    }

    IEnumerator FetchWindData()//風データを取得して解析, WHile文使うかもこれだと遅かったらそのままで終わりじゃない？
    {
        UnityWebRequest request = UnityWebRequest.Get(url + apiKey);//APIを叩く
        yield return request.SendWebRequest();//URLに接続してデータがくるまで待つ

        if(request.result == UnityWebRequest.Result.Success)//通信が成功した場合
        {
            string windData = request.downloadHandler.text;

            ForecastData data = JsonUtility.FromJson<ForecastData>(FixJson(windData));//windData文字列を ForecastData 型に変換

            windForces.Clear();//既存データをクリア
            // yield return new WaitForSeconds(5f);//5秒待つ、これいる？

            foreach (var hour in data.list)//各時間帯の風データをベクトルに変換
            {
                float angle = Mathf.Deg2Rad * (hour.wind.deg + 180f);//風向に180度を加えて「風が来る方向」→「風が吹く方向」へ変換
                Vector2 dir = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
                Vector2 windForce = dir * hour.wind.speed;//ベクトルに風速を掛けて風力を計算
                windForces.Add(windForce);//リストに追加
            }
            currentWind = windForces[0];
            Debug.Log("風データ取得完了！");
        }
        else
        {
            // 通信エラー処理
            Debug.LogError("API通信エラー: " + request.error);
        }
    }

    void FixedUpdate()//一定間隔で風を切り替え、影響範囲内の対象に風の力を加える
    {
        timer += Time.fixedDeltaTime;//時間計算、0.02秒ごと追加
        if(windForces.Count==0) return;
        if(timer >= 10f)//10秒ごとに風を更新
        {
            timer = 0f;
            currentWindIndex += 1;
            currentWind = magnification*windForces[currentWindIndex];//風のつよさ制限したいからなんか入れるかもここら辺に
            Debug.Log($"風向変更: {currentWind}");
        }

        Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, forceRadius, targetLayer);//風の影響範囲に入っているオブジェクトを取得
        foreach (Collider2D target in targets)
        {
            Rigidbody2D rb = target.attachedRigidbody;
            if (rb != null)//Rigidbody2D があれば風の力を加える
            {
                Vector2 direction = (transform.position - target.transform.position).normalized;
                rb.AddForce(direction * attractionForce + currentWind);//ForceMode2D.Forceいるのかな？
            }
        }
    }
    private string FixJson(string json)//json保存で型式の都合でこうせざるをえない
    {
        int listIndex = json.IndexOf("\"list\"");
        if (listIndex >= 0)
        {
            int arrayStart = json.IndexOf('[', listIndex);
            int arrayEnd = json.LastIndexOf(']');
            string arrayJson = json.Substring(arrayStart, arrayEnd - arrayStart + 1);
            return "{\"list\":" + arrayJson + "}";
        }
        else
        {
            Debug.LogError("JSONに 'list' が見つかりませんでした");
            return "{}";
        }
    }
}
