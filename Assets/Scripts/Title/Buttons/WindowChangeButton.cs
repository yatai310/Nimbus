using UnityEngine;

public class WindowChangeButton : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnClick(GameObject button)
    {
        if (button.name == "StageButton")
        {
            WindowManager.whatWindowIs = 0;
            Debug.Log("StageButton clicked, changing window to StageSelect.");
        }
        else if (button.name == "CreditButton")
        {
            WindowManager.whatWindowIs = 1;
        }
        else if (button.name == "SettingsButton")
        {
            WindowManager.whatWindowIs = 2;
        }
        else
        {
            Debug.LogError("Unknown button clicked: " + this.gameObject.name);
        }
    }
}
