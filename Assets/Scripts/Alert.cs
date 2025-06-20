// using UnityEngine;

// public class Alert : MonoBehaviour
// {
//     public static Alert alert;
//     private static string text;
//     private static int rain;
//     // Update is called once per frame
//     void Start()
//     {
//         rain = 0;
//     }
//     void FixedUpdate()
//     {
//         int score = ScoreBoard.Instance.getScore();
//         if(score==0 && rain==0)
//         {
//             text = "本日は晴天なり";
//             rain = 1;
//         }
//         else if(0<score && score<500 && rain==1)
//         {
//             text = "あいにくの雨"
//             rain = 2;
//         }
//         else if(500<=score && rain == 2)
//         {
//             text = "大雨注意報発令";
//             rain = 3;
//         }
//         else if(800<=score && score<2000 && rain == 3)
//         {
//             text = "大雨警報発令";
//             rain = 4;
//         }
//         else if(2000<=score && score<3000) text = "信じられない豪雨です"
//         else if(3000<=score) text = "大雨特別警報発令！" 
//     }
// }
