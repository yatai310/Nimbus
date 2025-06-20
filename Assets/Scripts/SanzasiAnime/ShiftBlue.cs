using UnityEngine;

public class ShiftBlue : MonoBehaviour
{
    SpriteRenderer sr;
    float blue = 0.65f;

    void Start(){
        sr = GetComponent<SpriteRenderer>();
    }

    void Update(){
        if(ScoreBoard.Instance != null){
            blue = 0.65f - 0.0005f * ScoreBoard.Instance.getScore();
        }
        blue = Mathf.Clamp01(blue);  // 0〜1の範囲に制限

        Color c = sr.color;
        c.b = blue;
        sr.color = c;
    }
}
