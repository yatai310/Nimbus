using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Voleme : MonoBehaviour
{
    public Slider slider; // スライダーの参照
    static public float volume = 1.0f; // 音量の初期値
    public AudioSource audioSource;
    public TextMeshProUGUI volumeText;
    void Start()
    {
        slider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    void OnSliderValueChanged(float value)
    {
        volume = value; // スライダーの値を音量に設定
        if (audioSource != null)
        {
            audioSource.volume = volume; // AudioSourceの音量を更新
        }
        UpdateVolumeText();
    }

    void UpdateVolumeText()
    {
        if (volumeText != null)
        {
            volumeText.text = "音量 " + (int)(volume * 100); // 音量をパーセンテージ表示
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
