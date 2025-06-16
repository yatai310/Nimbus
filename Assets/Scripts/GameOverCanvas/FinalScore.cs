using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FinalScore : MonoBehaviour
{
    public TextMeshProUGUI resultScoreText;

    public void ShowScore()
    {
        resultScoreText.text = "スコア: " + ScoreBoard.Instance.getScore();
    }
}