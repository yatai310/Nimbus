using UnityEngine;
using UnityEngine.SceneManagement;

public class ShiftTitleButtonController : MonoBehaviour
{
    public void ShiftClick()
    {
        //�^�C�g����ʂɑJ��
        SceneManager.LoadScene("Title");
    }
}
