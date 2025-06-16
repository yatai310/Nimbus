using UnityEngine;
using System;
//sanzasi編集
public class GameOverMg : MonoBehaviour
{
    public GameObject popupPanel;
    public GameObject field;

    public Vector2 center;//GameOver判定の円の中心座標
    public float radius = 30f;//円の半径

    private bool gameOverFlag = false;

    void Start(){
        if (popupPanel != null){
            popupPanel.SetActive(false); // 最初はポップアップ非表示
        }

         if (field != null)
        {
            center = field.transform.position;
            radius = field.transform.localScale.x * 0.5f; // 直径の半分
        }
    }

    void Update()
    {
        if(gameOverFlag) return;//一度だけポップアップを表示

        //「Cloud」タグを持つオブジェクトをすべて取得
        GameObject[] clouds = GameObject.FindGameObjectsWithTag("CloudModel");

       //ゲームオーバーを判定
        foreach (GameObject cloud in clouds){
            Vector2 position = cloud.transform.position;
            float distance = Vector2.Distance(position, center);
            if (distance > radius){
                popupPanel.SetActive(true);
                gameOverFlag = true;
                break;
            }
        }
    }
}
