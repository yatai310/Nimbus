using System;
using UnityEngine;

public class StageSelectButtonManager1 : MonoBehaviour
{
    public void OnClick()
    {
        PlayBotton.StageName = this.gameObject.tag;
        Debug.Log("クリックされたステージ: " + PlayBotton.StageName);
    }
}
