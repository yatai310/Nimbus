using UnityEngine;
using UnityEngine.EventSystems;


public class StageScrollManager : MonoBehaviour
{
    public GameObject firstObject; // 最初に選択されるオブジェクト
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EventSystem.current.SetSelectedGameObject(firstObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
