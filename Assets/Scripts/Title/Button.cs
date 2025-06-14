using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnClick()
    {
        Debug.Log("クリックされたメーン");
        var spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            var sprite = Resources.Load<Sprite>("Img/window");
            if (sprite == null)
            {
                sprite = Resources.Load<Sprite>("Img/Screan");
            }
            if (sprite != null)
            {
                spriteRenderer.sprite = sprite;
            }
            else
            {
                Debug.LogWarning("指定された画像が見つかりませんでした。");
            }
        }
        else
        {
            Debug.LogWarning("SpriteRendererがアタッチされていません。");
        }
    }
    public void Scenemg()
    {
        Debug.Log("Scenemg");
        // SceneManager.LoadScene("GameScene");
    }
}
