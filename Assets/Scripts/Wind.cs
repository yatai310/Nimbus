using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Wind : MonoBehaviour
{
    // 時間ごとの風データを表すクラス
    [System.Serializable] public class HourlyData
    {
        public float wind_speed;  // 風速（m/s）
        public float wind_deg;    // 風向（度数法で表現、北を0度として時計回り）
        public int dt;            // タイムスタンプ（使用していないがAPIには含まれる）
    }

    // Forecast API の hourly 部分を格納するクラス
    [System.Serializable] public class ForecastData
    {
        public List<HourlyData> hourly; // 1時間ごとの風データ
    }

    public float attractionRadius = 5f;     // 風の影響範囲（半径）
    public LayerMask targetLayer;           // 風の影響を受ける対象レイヤー

    private List<Vector2> windForces = new List<Vector2>(); // 各時間ごとの風力ベクトルを格納
    private int currentWindIndex = 0;       // 現在適用中の風データのインデックス
    private float timer = 0f;               // 風の切り替え用のタイマー
    private float changeInterval = 10f;     // 風を切り替える時間間隔（秒）

    // OpenWeatherMapのAPIキーとURL（APIキーはセキュリティ的に外部に漏れないよう注意）
    private string apiKey = "48dbfd08356661d251c65c60b4f5ee3f";
    private string url = "https://api.openweathermap.org/data/2.5/forecast?lat=51.5074&lon=-0.1278&exclude=minutely,daily,current,alerts&units=metric&appid=";

    private Vector2 currentWind = Vector2.zero; // 現在の風力（方向と強さを含む）

    void Start()
    {
        // ゲーム開始時にAPIから風データを取得するコルーチンを実行
        StartCoroutine(FetchWindData());
    }

    // OpenWeatherMap API から風データを取得して解析する
    IEnumerator FetchWindData()
    {
        UnityWebRequest www = UnityWebRequest.Get(url + apiKey);
        yield return www.SendWebRequest();

        // 通信が成功した場合
        if (www.result == UnityWebRequest.Result.Success)
        {
            string json = www.downloadHandler.text;

            // JSON文字列を ForecastData 型に変換
            ForecastData data = JsonUtility.FromJson<ForecastData>(json);

            windForces.Clear(); // 既存データをクリア
            yield return new WaitForSeconds(5f); // 5秒待つ

            // 各時間帯の風データをベクトルに変換
            foreach (var hour in data.hourly)
            {
                // 風向に180度を加えて「風が来る方向」→「風が吹く方向」へ変換
                float angle = Mathf.Deg2Rad * (hour.wind_deg + 180f);
                Vector2 dir = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));

                // ベクトルに風速を掛けて風力を計算
                Vector2 force = dir * hour.wind_speed;

                // リストに追加
                windForces.Add(force);
            }

            // 最初の風を設定
            if (windForces.Count > 0)
                currentWind = windForces[0];

            Debug.Log("風データ取得完了！");
        }
        else
        {
            // 通信エラー処理
            Debug.LogError("API通信エラー: " + www.error);
        }
    }

    // 一定間隔で風を切り替え、影響範囲内の対象に風の力を加える
    void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;

        if (windForces.Count == 0) return;

        // 一定時間ごとに風を更新
        if (timer >= changeInterval)
        {
            timer = 0f;

            // 次の風に切り替え（ループする）
            currentWindIndex = (currentWindIndex + 1) % windForces.Count;
            currentWind = windForces[currentWindIndex];

            Debug.Log($"風向変更: {currentWind}");
        }

        // 風の影響範囲に入っているオブジェクトを取得
        Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, attractionRadius, targetLayer);
        foreach (Collider2D target in targets)
        {
            Rigidbody2D rb = target.attachedRigidbody;

            // Rigidbody2D があれば風の力を加える
            if (rb != null)
            {
                rb.AddForce(currentWind, ForceMode2D.Force);
            }
        }
    }
}
