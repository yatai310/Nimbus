using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class PlayBotton : MonoBehaviour
{
    public static string StageName = "GameScene";
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
    }
    public void Scenemg()
    {
        Debug.Log("Scenemg");
        SceneManager.LoadScene(StageName);
    }
}
