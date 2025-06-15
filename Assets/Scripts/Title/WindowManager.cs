using UnityEngine;
using UnityEngine.UI;

public class WindowManager : MonoBehaviour
{
    public static int whatWindowIs = 0;
    public GameObject[] stageWindow;
    public GameObject[] buttons;
    private Button[] buttonComponents;
    public Sprite activeSprite;
    public Sprite normalSprite;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        buttonComponents = new Button[buttons.Length];
        for (int i = 0; i < buttons.Length; i++)
        {
            buttonComponents[i] = buttons[i].GetComponent<Button>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < stageWindow.Length; i++)
        {
            if (i == whatWindowIs)
            {
                stageWindow[i].SetActive(true);
                buttonComponents[i].GetComponent<Image>().sprite = normalSprite;
            }
            else
            {
                stageWindow[i].SetActive(false);
                buttonComponents[i].GetComponent<Image>().sprite = activeSprite;
            }
        }
    }
}
