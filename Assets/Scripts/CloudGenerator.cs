using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class CloudGenerator : MonoBehaviour
{
    public GameObject[] CloudPrefabs;
    public GameObject Core;
    // Update is called once per frame
    void Update()
    {
        if(Keyboard.current.leftArrowKey.wasPressedThisFrame)//あとで条件変える
        {
            CloudGenerate();
        }
    }
    void CloudGenerate()//後でもう一度アルゴリズムを確認すること！
    {
        int randomIndex = Random.Range(0, CloudPrefabs.Length);
        float X = Random.Range(-15, 16);
        float Y = Mathf.Sqrt(225-X*X);
        
        GameObject newCloud = Instantiate(CloudPrefabs[randomIndex], new Vector3(X, Y, 1f), Quaternion.identity);
    }
}
