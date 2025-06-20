using UnityEngine;
using System;

public class Cloud : MonoBehaviour
{
    public int level;
    public event Action<Cloud, Cloud> OnCloudMergeRequested;//通知用

    private void OnCollisionEnter2D(Collision2D collision)//オブジェクト衝突時
    {
        if (collision.gameObject.CompareTag("CloudModel"))//衝突したオブジェクトがCloudオブジェクトなら
        {
            Cloud otherC = collision.gameObject.GetComponent<Cloud>();
            if (otherC.level == this.level && otherC != null)//衝突したCloudオブジェクトと同レベルなら
            {
                if(this.GetInstanceID() < otherC.GetInstanceID()) OnCloudMergeRequested?.Invoke(this, otherC);//CloudGeneratorに通知
            }
        }
    }
}
