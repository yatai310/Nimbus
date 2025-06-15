using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DescribeTextManger : MonoBehaviour
{
    private TextMeshProUGUI text;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       text = GetComponent<TextMeshProUGUI>();  
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "Stage:" + PlayBotton.StageName + "\n";
        if(PlayBotton.StageName == "GameScene")
        {
            text.text += "This is the first stage.";
        }
        else if(PlayBotton.StageName == "AnotherGameStage")
        {
            text.text += "This is an another stage you've never seen.";
        }
        else if(PlayBotton.StageName == "Stage3")
        {
            text.text += "waht?";
        }
        else
        {
            text.text += "no one is here.";
        }
    }
}
