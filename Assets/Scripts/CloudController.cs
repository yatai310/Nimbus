using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class CloudController : MonoBehaviour
{
    [SerializeField] private float moveForce = 10f;
    Vector2 inputDir;
    public GameObject core;
    private Rigidbody2D rb;
    private bool controlKey;//生成時すぐに操作して範囲外、ゲームオーバーを防止
    private bool hasTriggered;//多重衝突防止
    public event Action<CloudController> OnCloudGenerateRequested;//通知用

    private void Start()//初期化
    {
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 1;//垂直同期オン
        rb = GetComponent<Rigidbody2D>();
        controlKey = false;//生成時すぐに操作して範囲外、ゲームオーバーを防止
        Invoke("enableControl", 0.25f);//生成時すぐに操作して範囲外、ゲームオーバーを防止
    }

    private void enableControl()//生成時すぐに操作して範囲外、ゲームオーバーを防止
    {
        controlKey = true;
    }

    void Update()//操作機能
    {
        inputDir = Vector2.zero;
        if (Keyboard.current.leftArrowKey.isPressed) inputDir.x -= 1;
        if (Keyboard.current.rightArrowKey.isPressed) inputDir.x += 1;
        if (Keyboard.current.upArrowKey.isPressed) inputDir.y += 1;
        if (Keyboard.current.downArrowKey.isPressed) inputDir.y -= 1;
    }
    void FixedUpdate()
    {
        Vector2 direction = (core.transform.position - this.transform.position).normalized;
        if (controlKey)
            rb.AddForce(inputDir.normalized * moveForce, ForceMode2D.Impulse);
    }
    
    private void OnCollisionEnter2D(Collision2D collision)//オブジェクト衝突時
    {
        if(hasTriggered) return;//多重衝突防止
        hasTriggered = true;

        OnCloudGenerateRequested?.Invoke(this);
    }
}
