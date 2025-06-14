using UnityEngine;
using System;

public class GameOverMg : MonoBehaviour
{
    public Vector2 center = new Vector2(7f,0f);  // 円の中心（2D座標）
    public float radius = 30f;              // 円の半径

    public event Action OnOutOfBounds;     // 範囲外になったときのイベント

    private Vector2 checkPosition;

    void Update()
    {
        float distance = Vector2.Distance(checkPosition, center);

        if (distance > radius)
        {
            if (OnOutOfBounds != null)
            {
                OnOutOfBounds.Invoke();
            }
        }
    }
}
