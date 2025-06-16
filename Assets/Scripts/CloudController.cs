using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class CloudController : MonoBehaviour
{
    [SerializeField] private float moveForce = 10f;
    private Rigidbody2D rb;
    public event Action<CloudController> OnCloudGenerateRequested;//通知用
    private void Start()//初期化
    {
        Application.targetFrameRate = 60;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()//操作機能
    {
        if (Keyboard.current.leftArrowKey.wasPressedThisFrame)
            rb.AddForce(-transform.right * moveForce, ForceMode2D.Impulse);
        if (Keyboard.current.rightArrowKey.wasPressedThisFrame)
            rb.AddForce(transform.right * moveForce, ForceMode2D.Impulse);
        if (Keyboard.current.upArrowKey.wasPressedThisFrame)
            rb.AddForce(transform.up * moveForce, ForceMode2D.Impulse);
        if (Keyboard.current.downArrowKey.wasPressedThisFrame)
            rb.AddForce(-transform.up * moveForce, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)//オブジェクト衝突時
    {
        OnCloudGenerateRequested?.Invoke(this);
    }
}
