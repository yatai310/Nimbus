using UnityEngine;

public class WindowManager : MonoBehaviour
{
    public static int whatWindowIs = 0;
    public GameObject[] stageWindow;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < stageWindow.Length; i++)
        {
            if (i == whatWindowIs)
            {
                stageWindow[i].SetActive(true);
            }
            else
            {
                stageWindow[i].SetActive(false);
            }
        }
    }
}
