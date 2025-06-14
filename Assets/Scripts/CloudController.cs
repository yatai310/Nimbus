using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class CloudController : MonoBehaviour
{
    [SerializeField] private float moveForce = 10f;
    private Rigidbody2D rb;
    private bool hasLanded = false;
    public Action OnLanded;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Application.targetFrameRate = 60;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.leftArrowKey.wasPressedThisFrame)
        {
            rb.AddForce(-transform.right * moveForce, ForceMode2D.Impulse);
        }
        if (Keyboard.current.rightArrowKey.wasPressedThisFrame)
        {
            rb.AddForce(transform.right * moveForce, ForceMode2D.Impulse);
        }
        if (Keyboard.current.upArrowKey.wasPressedThisFrame)
        {
            rb.AddForce(transform.up * moveForce, ForceMode2D.Impulse);
        }
        if (Keyboard.current.downArrowKey.wasPressedThisFrame)
        {
            rb.AddForce(-transform.up * moveForce, ForceMode2D.Impulse);
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(!hasLanded && (collision.gameObject.CompareTag("Core") || collision.gameObject.CompareTag("CloudModel")))
        {
            hasLanded = true;
            rb.bodyType = RigidbodyType2D.Static;
            OnLanded?.Invoke();
        }
    }
}
