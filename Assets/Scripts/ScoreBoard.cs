using UnityEngine;
using TMPro;

public class ScoreBoard : MonoBehaviour
{
    public static ScoreBoard Instance;
    private int currentScore = 0;
    public GameObject scoreObject;
    private TextMeshProUGUI scoreText;
    void Start()
    {
        scoreText = scoreObject.GetComponent<TextMeshProUGUI>();
        UpdateScoreUI();
    }
    void Awake()
    {
        if(Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    public void addScore(int score)
    {
        currentScore += score;
        UpdateScoreUI(); // UIの更新も一緒に
    }
    public int getScore()
    {
        return currentScore;
    }
    public void resetScore()
    {
        currentScore = 0;
        UpdateScoreUI(); // UIのリセットも一緒に
    }
    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            //フォントの都合で変更するかも
            scoreText.text = "本日の降水量は \n" + Instance.currentScore.ToString() + "mm です";
        }
    }
}
