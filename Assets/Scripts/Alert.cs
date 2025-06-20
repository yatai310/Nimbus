using UnityEngine;

public class Alert : MonoBehaviour
{
    public static Alert alert;
    private static string text;
    private static int rain;

    void Start()
    {
        rain = 1;
    }
    void FixedUpdate()
    {
        int score = ScoreBoard.Instance.getScore();
        if(0<score && score<500 && rain==1)
        {
            text = "あいにくの雨";
            rain = 2;
        }
        else if(500<=score && score<6&& rain==2)
        {
            text = "大雨注意報発令";
            rain = 3;
        }
        else if(800<=score && score<2000 && rain==3)
        {
            text = "大雨警報発令!";
            rain = 4;
        }
        else if(2000<=score && score<3000 && rain==4)
        {
            text = "信じられない豪雨です!!";
            rain = 5;
        }
        else if(3000<=score && rain==5)
        {
            text = "大雨特別警報発令！！直ちに避難してください！！";
            rain = 6;
        }
    }
    
}
