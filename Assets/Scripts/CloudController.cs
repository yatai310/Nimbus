using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class CloudController : MonoBehaviour
{
    [SerializeField] private float moveForce = 10f;
    private Rigidbody2D rb;
    private bool controlKey;//生成時すぐに操作して範囲外、ゲームオーバーを防止
    private bool hasTriggered;//多重衝突防止
    public event Action<CloudController> OnCloudGenerateRequested;//通知用

    private void Start()//初期化
    {
        Application.targetFrameRate = 60;
        rb = GetComponent<Rigidbody2D>();
        controlKey = false;//生成時すぐに操作して範囲外、ゲームオーバーを防止
        Invoke("enableControl", 0.25f);//生成時すぐに操作して範囲外、ゲームオーバーを防止
    }

    private void enableControl()//生成時すぐに操作して範囲外、ゲームオーバーを防止
    {
        controlKey = true;
    }

    private void Update()//操作機能
    {
        if(controlKey)
        {   if (Keyboard.current.leftArrowKey.wasPressedThisFrame)
                rb.AddForce(-transform.right * moveForce, ForceMode2D.Impulse);
            if (Keyboard.current.rightArrowKey.wasPressedThisFrame)
                rb.AddForce(transform.right * moveForce, ForceMode2D.Impulse);
            if (Keyboard.current.upArrowKey.wasPressedThisFrame)
                rb.AddForce(transform.up * moveForce, ForceMode2D.Impulse);
            if (Keyboard.current.downArrowKey.wasPressedThisFrame)
                rb.AddForce(-transform.up * moveForce, ForceMode2D.Impulse);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)//オブジェクト衝突時
    {
        if(hasTriggered) return;//多重衝突防止
        hasTriggered = true;

        OnCloudGenerateRequested?.Invoke(this);
    }
}
