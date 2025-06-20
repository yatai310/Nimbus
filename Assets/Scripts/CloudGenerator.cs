using UnityEngine;
using System;
using System.Collections;

public class CloudGenerator : MonoBehaviour
{
    public GameObject[] CloudPrefabs;
    private bool isMerging = false;//合体中は重複処理を防ぐ
    private bool isGenerating = false;//
    private GameObject currentControlKey;//
    public float BombForce = 10f;
    public float BombRadius = 1f;
    public LayerMask targetLayer;

    private void Start()//最初の雲じゃよ
    {
        CloudGenerate();
    }

    private void CloudGenerate()//雲生成
    {
        if(isGenerating) return;//多重生成防止用
        isGenerating=true;

        int randomIndex = UnityEngine.Random.Range(0, CloudPrefabs.Length-2);
        float X = UnityEngine.Random.Range(-15, 16);
        float Y = Mathf.Sqrt(225 - X * X);
        if(UnityEngine.Random.Range(0, 2)==0) Y *= -1;//これがないと下に生成されなくなる

        GameObject newCloud = Instantiate(CloudPrefabs[randomIndex], new Vector3(X, Y, 1f), Quaternion.identity);
        Cloud c = newCloud.GetComponent<Cloud>();
        c.level = randomIndex;
        c.OnCloudMergeRequested += CloudMergeRequest;//アクションに関数を渡してる
        CloudController cc = newCloud.GetComponent<CloudController>();
        cc.OnCloudGenerateRequested += ControlKeyRequest;//通知用

        currentControlKey = newCloud;
        isGenerating = false;
    }

    private void CloudMergeRequest(Cloud cloud1, Cloud cloud2)//合体したいよ～って通知が来たら起動
    {
        if (isMerging) return; // 既に合体処理中なら無視
        isMerging = true;
        if (cloud1 == null || cloud2 == null || !cloud1.gameObject.activeInHierarchy || !cloud2.gameObject.activeInHierarchy)//既に破壊されている可能性もあるため念のためチェック
        {
            isMerging = false;
            return;
        }

        Vector3 mergePos = (cloud1.transform.position + cloud2.transform.position) / 2f;//二つの雲の間に新しい雲生成
        int newLevel = cloud1.level + 1;
        ScoreBoard.Instance.addScore((int)Mathf.Pow(2, newLevel));//合体したら得点をつける

        Destroy(cloud1.gameObject);//合体したら元のは消さないとね★
        Destroy(cloud2.gameObject);

        // 最大レベルを超えたら単純に消すだけにするよ～～
        if (newLevel >= CloudPrefabs.Length)
        {
            StartCoroutine(ResetMergingFlag());//マテちゃ
            return;
        }

        Collider2D[] targets = Physics2D.OverlapCircleAll(mergePos, BombRadius, targetLayer);//周りを弾き飛ばす
        foreach (Collider2D target in targets)
        {
            Rigidbody2D rb = target.attachedRigidbody;
            if (rb != null)
            {
                Vector2 direction = (mergePos - target.transform.position).normalized;
                rb.AddForce(direction * BombForce, ForceMode2D.Impulse);
            }
        }

        GameObject mergedCloud = Instantiate(CloudPrefabs[newLevel], mergePos, Quaternion.identity);

        // Rigidbody2D rb = mergedCloud.GetComponent<Rigidbody2D>();// 一時的に重力を無効化（オプション）
        // rb.isKinematic = true;

        Cloud newC = mergedCloud.GetComponent<Cloud>();
        newC.level = newLevel;
        newC.OnCloudMergeRequested += CloudMergeRequest;//アクションに関数を渡してる

        Destroy(mergedCloud.GetComponent<CloudController>());
        StartCoroutine(ResetMergingFlag());//マテちゃ
        
        // StartCoroutine(ReenablePhysics(rb));//重力をもどす
    }
    private void ControlKeyRequest(CloudController cloud)
    {
        if(currentControlKey != null) Destroy(cloud);//古い操作鍵を消去
        currentControlKey = null;
        StartCoroutine(DelayedGenerate());
    }
    private IEnumerator DelayedGenerate()
    {
        yield return null;
        CloudGenerate();
    }
    private IEnumerator ResetMergingFlag()
    {
        yield return new WaitForSeconds(0.05f);
        // yield return null;//1フレーム待つ（Destroyが完全に終わるまで）
        isMerging = false;
    }
    // IEnumerator ReenablePhysics(Rigidbody2D rb)
    // {
    //     yield return new WaitForSeconds(0.1f);
    //     rb.isKinematic = false;
    // }
}
