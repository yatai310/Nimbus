using System;
using UnityEngine;

public class StageSelectButtonManager : MonoBehaviour
{
    public void OnClick(GameObject button)
    {
        PlayBotton.StageName = button.tag;
        Debug.Log("クリックされたステージ: " + PlayBotton.StageName);
    }
}
