using UnityEngine;

public class ScoreBoard : MonoBehaviour
{
    public static ScoreBoard Instance;
    private int currentScore = 0;
    void Awake()
    {
        if(Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    public void addScore(int score)
    {
        currentScore += score;
    }
    public int getScore()
    {
        return currentScore;
    }
    public void resetScore()
    {
        currentScore = 0;
    }
}
