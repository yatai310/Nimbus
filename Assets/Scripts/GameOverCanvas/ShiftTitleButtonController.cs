using UnityEngine;
using UnityEngine.SceneManagement;

public class ShiftTitleButtonController : MonoBehaviour
{
    public void ShiftClick()
    {
        //時間を進める。
        Time.timeScale = 1f;
        //Tilte画面に移動
        SceneManager.LoadScene("Title");
    }
}
