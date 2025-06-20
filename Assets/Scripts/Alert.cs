using UnityEngine;
using TMPro;

public class Alert : MonoBehaviour
{
    private static int rain;
    public float speed = 100f; // テキストの移動速度（px/sec）
    public float resetPositionX = -500f; // この位置まで移動したらリセット
    public float startPositionX = 500f;  // 元の開始位置

    private RectTransform rectTransform;
    private TextMeshProUGUI alertText;
    public GameObject alertObject;

    void Start()
    {
        alertText = alertObject.GetComponent<TextMeshProUGUI>();
        rain = 1;
        rectTransform = GetComponent<RectTransform>();//初期位置に設定
        rectTransform.anchoredPosition = new Vector2(startPositionX, rectTransform.anchoredPosition.y);
    }
    void Update()
    {
        rectTransform.anchoredPosition += Vector2.right * speed * Time.deltaTime;//右に移動
        if (rectTransform.anchoredPosition.x > resetPositionX)//一定の位置まで移動したら初期位置に戻す
        {
            rectTransform.anchoredPosition = new Vector2(startPositionX, rectTransform.anchoredPosition.y);
        }
    }
    void FixedUpdate()
    {
        int score = ScoreBoard.Instance.getScore();
        if(0<score && score<500 && rain==1)
        {
            alertText.text = "あいにくの雨";
            rain = 2;
        }
        else if(500<=score && score<800 && rain==2)
        {
            alertText.text = "大雨注意報発令";
            rain = 3;
        }
        else if(800<=score && score<2000 && rain==3)
        {
            alertText.text = "大雨警報発令!";
            rain = 4;
        }
        else if(2000<=score && score<3000 && rain==4)
        {
            alertText.text = "信じられない豪雨です!!";
            rain = 5;
        }
        else if(3000<=score && rain==5)
        {
            alertText.text = "大雨特別警報発令！！直ちに避難してください！！";
            rain = 6;
        }
    }
    
}
